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
    public uint NoteDuration => 60 * frequency / BeatsPerMinute;
    public uint ChunkSize => NoteDuration * (uint)Notes.Count * blockAlign;

    public void Create(string filePath) {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        using var binaryWriter = new BinaryWriter(fileStream);

        binaryWriter.Write(fileTypeId);
        binaryWriter.Write(GetSize());
        binaryWriter.Write(mediaTypeId);
        binaryWriter.Write(format);
        binaryWriter.Write(formatChunkSize);
        binaryWriter.Write(formatTag);
        binaryWriter.Write(channels);
        binaryWriter.Write(frequency);
        binaryWriter.Write(bytesPerSecond);
        binaryWriter.Write(blockAlign);
        binaryWriter.Write(bitsPerSample);

        binaryWriter.Write(chunkId);
        binaryWriter.Write(ChunkSize);

        foreach (var note in Notes) {
            for (int i = 0; i < NoteDuration; i++) {
                binaryWriter.Write((short)(amplitude * note.GetBaseAmplitude(i / (double)frequency)));
            }
        }

        // kept for reference - to play multiple notes, add them up
        //for (int i = 0; i < samples / 2; i++) {
        //    double t = (double)i / (double)frequency;
        //    short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * concert * 2.0 * Math.PI)));
        //    binaryWriter.Write(s);
        //}
    }

    //test?
    private uint GetSize() {
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
