using System.Text;

namespace Noisier;

public class WaveCreator {
    private static byte[] fileTypeId = Encoding.ASCII.GetBytes("RIFF");
    private static byte[] mediaTypeId = Encoding.ASCII.GetBytes("WAVE");
    private static byte[] format = Encoding.ASCII.GetBytes("fmt ");
    private const uint formatChunkSize = 16;
    private const ushort formatTag = 1;
    private const ushort channels = 2;
    private const uint frequency = 44100;
    private const uint bytesPerSecond = frequency * blockAlign;
    private const ushort blockAlign = channels * ((bitsPerSample + 7) / 8);
    private const ushort bitsPerSample = 16;
    private static byte[] chunkId = Encoding.ASCII.GetBytes("data");
    private const double amplitude = 10000;

    public uint BeatsPerMinute { get; set; } = 100;
    public List<Note> Notes { get; set; } = new();
    public uint BeatDuration => 60 * frequency / BeatsPerMinute;
    public uint TotalDuration => (uint)Notes.Select(note => BeatDuration * (note.Position.Value + note.Duration.Value)).DefaultIfEmpty(0).Max();
    public uint ChunkSize => TotalDuration * blockAlign;

    public void Create(string filePath) {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        using var binaryWriter = new BinaryWriter(fileStream);

        WriteHeader(binaryWriter);
        WriteFormat(binaryWriter);

        binaryWriter.Write(chunkId);
        binaryWriter.Write(ChunkSize);

        var notes = new Queue<Note>(Notes.OrderBy(n => n.Position.Value));
        var activeNotes = new List<Note>();

        for (uint i = 0; i < TotalDuration; i++) {
            while (notes.Any() && i == (uint)(notes.Peek().Position.Value * BeatDuration)) {
                activeNotes.Add(notes.Dequeue());
            }

            foreach (var activeNote in activeNotes.ToList()) {
                if (i == (uint)((activeNote.Position.Value + activeNote.Duration.Value) * BeatDuration)) {
                    activeNotes.Remove(activeNote);
                }
            }

            var timePoint = i / (double)frequency;
            var baseAmplitude = activeNotes.Sum(activeNote => {
                var fragmentPlayed = (i - activeNote.Position.Value * BeatDuration) / (activeNote.Duration.Value * BeatDuration);

                return activeNote.GetBaseAmplitude(timePoint, fragmentPlayed);
            });

            binaryWriter.Write((short)Math.Clamp(amplitude * baseAmplitude, short.MinValue, short.MaxValue));
        }
    }

    public void WriteHeader(BinaryWriter binaryWriter) {
        binaryWriter.Write(fileTypeId);
        binaryWriter.Write(GetSize());
        binaryWriter.Write(mediaTypeId);
    }

    public void WriteFormat(BinaryWriter binaryWriter) {
        binaryWriter.Write(format);
        binaryWriter.Write(formatChunkSize);
        binaryWriter.Write(formatTag);
        binaryWriter.Write(channels);
        binaryWriter.Write(frequency);
        binaryWriter.Write(bytesPerSecond);
        binaryWriter.Write(blockAlign);
        binaryWriter.Write(bitsPerSample);
    }

    public uint GetSize() {
        return (uint)(
            fileTypeId.Length
            + sizeof(uint) // Size
            + mediaTypeId.Length
            + format.Length
            + sizeof(uint) // formatChunkSize
            + sizeof(ushort) // formatTag
            + sizeof(ushort) // channels
            + sizeof(uint) // frequency
            + sizeof(uint) // bytesPerSecond
            + sizeof(ushort) // blockAlign
            + sizeof(ushort) // bitsPerSample
            + sizeof(uint) // chunkId
            + sizeof(uint) // chunkSize
            + ChunkSize
        );
    }
}
