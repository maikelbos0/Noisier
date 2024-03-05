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
    private const ushort blockAlign = channels * bitsPerSample / 8;
    private const ushort bitsPerSample = 16;
    private static byte[] chunkId = Encoding.ASCII.GetBytes("data");

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

        int samples = 88200 * 4;

        binaryWriter.Write(chunkId);
        binaryWriter.Write(samples * blockAlign);
        double aNatural = 220.0;
        double ampl = 10000;
        double perfect = 1.5;
        double concert = 1.498307077;
        double freq = aNatural * concert;

        //for (int i = 0; i < samples; i++) {
        //    double t = (double)i / (double)frequency;
        //    short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
        //    binaryWriter.Write(s);
        //}

        //for (int i = 0; i < samples / 4; i++) {
        //    double t = (double)i / (double)frequency;
        //    short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
        //    binaryWriter.Write(s);
        //}
        //freq = aNatural * concert;
        //for (int i = 0; i < samples / 4; i++) {
        //    double t = (double)i / (double)frequency;
        //    short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
        //    binaryWriter.Write(s);
        //}
        for (int i = 0; i < samples / 2; i++) {
            double t = (double)i / (double)frequency;
            short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * perfect * 2.0 * Math.PI)));
            binaryWriter.Write(s);
        }
        for (int i = 0; i < samples / 2; i++) {
            double t = (double)i / (double)frequency;
            short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * concert * 2.0 * Math.PI)));
            binaryWriter.Write(s);
        }
    }

    private int GetSize() {
        return fileTypeId.Length
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
            + 88200 * 4 * blockAlign
            ;
    }
}
