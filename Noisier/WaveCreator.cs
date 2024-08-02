using System.Text;

namespace Noisier;

public class WaveCreator {
    private static readonly byte[] fileTypeId = Encoding.ASCII.GetBytes("RIFF");
    private static readonly byte[] mediaTypeId = Encoding.ASCII.GetBytes("WAVE");
    private static readonly byte[] format = Encoding.ASCII.GetBytes("fmt ");
    private const int formatChunkSize = 16;
    private const short formatTag = 1;
    private const short channels = 2;
    private const int frequency = 44100;
    private const int bytesPerSecond = frequency * blockAlign;
    private const short blockAlign = channels * ((bitsPerSample + 7) / 8);
    private const short bitsPerSample = 16;
    private static readonly byte[] chunkId = Encoding.ASCII.GetBytes("data");

    public int BeatsPerMinute { get; set; } = 100;
    public List<Track> Tracks { get; set; } = [];
    public int BeatDuration => 60 * frequency / BeatsPerMinute;
    public int TotalDuration => (int)Tracks.SelectMany(track => track.Positions.SelectMany(position => track.Notes.Select(note => BeatDuration * (position.Value + note.Position.Value + note.Duration.Value)))).DefaultIfEmpty(0).Max();
    public int ChunkSize => TotalDuration * blockAlign;

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

        for (int i = 0; i < TotalDuration; i++) {
            var amplitude = Tracks.Sum(track => track.GetAmplitude(i, frequency, BeatDuration));

            binaryWriter.Write((short)Math.Clamp(amplitude, short.MinValue, short.MaxValue));
        }
    }

    public int GetSize() {
        return (
            fileTypeId.Length
            + sizeof(int) // Size
            + mediaTypeId.Length
            + format.Length
            + sizeof(int) // formatChunkSize
            + sizeof(short) // formatTag
            + sizeof(short) // channels
            + sizeof(int) // frequency
            + sizeof(int) // bytesPerSecond
            + sizeof(short) // blockAlign
            + sizeof(short) // bitsPerSample
            + sizeof(int) // chunkId
            + sizeof(int) // chunkSize
            + ChunkSize
        );
    }
}
