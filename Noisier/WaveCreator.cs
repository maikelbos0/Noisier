using System.Text;

namespace Noisier;

public class WaveCreator {
    private static readonly byte[] fileTypeId = Encoding.ASCII.GetBytes("RIFF");
    private static readonly byte[] mediaTypeId = Encoding.ASCII.GetBytes("WAVE");
    private static readonly byte[] format = Encoding.ASCII.GetBytes("fmt ");
    private const uint formatChunkSize = 16;
    private const ushort formatTag = 1;
    private const ushort channels = 2;
    private const uint frequency = 44100;
    private const uint bytesPerSecond = frequency * blockAlign;
    private const ushort blockAlign = channels * ((bitsPerSample + 7) / 8);
    private const ushort bitsPerSample = 16;
    private static readonly byte[] chunkId = Encoding.ASCII.GetBytes("data");

    public uint BeatsPerMinute { get; set; } = 100;
    public List<Track> Tracks { get; set; } = [];
    public uint BeatDuration => 60 * frequency / BeatsPerMinute;
    public uint TotalDuration => (uint)Tracks.SelectMany(track => track.Positions.SelectMany(position => track.Notes.Select(note => BeatDuration * (position.Value + note.Position.Value + note.Duration.Value)))).DefaultIfEmpty(0).Max();
    public uint ChunkSize => TotalDuration * blockAlign;

    public void Create(string filePath) {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        using var binaryWriter = new BinaryWriter(fileStream);

        WriteHeader(binaryWriter);
        WriteFormat(binaryWriter);
        WriteContent(binaryWriter);
    }

    public void WriteHeader(BinaryWriter binaryWriter) {
        binaryWriter.Write(fileTypeId);
        binaryWriter.Write(GetSize());
        binaryWriter.Write(mediaTypeId);
    }

    public static void WriteFormat(BinaryWriter binaryWriter) {
        binaryWriter.Write(format);
        binaryWriter.Write(formatChunkSize);
        binaryWriter.Write(formatTag);
        binaryWriter.Write(channels);
        binaryWriter.Write(frequency);
        binaryWriter.Write(bytesPerSecond);
        binaryWriter.Write(blockAlign);
        binaryWriter.Write(bitsPerSample);
    }

    public void WriteContent(BinaryWriter binaryWriter) {
        binaryWriter.Write(chunkId);
        binaryWriter.Write(ChunkSize);

        for (uint i = 0; i < TotalDuration; i++) {
            var amplitude = Tracks.Sum(track => track.GetAmplitude(i, frequency, BeatDuration));

            binaryWriter.Write((short)Math.Clamp(amplitude, short.MinValue, short.MaxValue));
        }
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
