using NSubstitute;
using System.Text;
using Xunit;

namespace Noisier.Tests;

public class WaveCreatorTests {
    [Theory]
    [InlineData(30, 88200)]
    [InlineData(60, 44100)]
    [InlineData(120, 22050)]
    public void NoteDuration(uint beatsPerMinute, double expectedDuration) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute
        };

        Assert.Equal(subject.BeatDuration, expectedDuration);
    }

    [Theory]
    [InlineData(240, 1, 1, 0, 1, 44100)]
    [InlineData(240, 1, 5, 0, 1, 8820)]
    [InlineData(240, 2, 1, 0, 1, 88200)]
    [InlineData(240, 1, 1, 7, 1, 352800)]
    public void ChunkSize(uint beatsPerMinute, uint durationNumerator, uint durationDenominator, uint positionNumerator, uint positionDenominator, uint expectedChunkSize) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute,
            Notes = {
                new Note(Pitch.C, 4, new Fraction(durationNumerator, durationDenominator), new Fraction(positionNumerator, positionDenominator))
            }
        };

        Assert.Equal(expectedChunkSize, subject.ChunkSize);
    }

    [Fact]
    public void WriteHeader() {
        var binaryWriter = Substitute.For<BinaryWriter>();
        var subject = new WaveCreator();

        subject.WriteHeader(binaryWriter);

        Received.InOrder(() => {
            binaryWriter.Received().Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("RIFF"))));
            binaryWriter.Write((uint)44);
            binaryWriter.Received().Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("WAVE"))));
        });
    }

    [Fact]
    public void WriteFormat() {
        var binaryWriter = Substitute.For<BinaryWriter>();
        var subject = new WaveCreator();

        subject.WriteFormat(binaryWriter);
        Received.InOrder(() => {
            binaryWriter.Received().Write(Arg.Is<byte[]>(value => value.SequenceEqual(Encoding.ASCII.GetBytes("fmt "))));
            binaryWriter.Write((uint)16);
            binaryWriter.Write((ushort)1);
            binaryWriter.Write((ushort)2);
            binaryWriter.Write((uint)44100);
            binaryWriter.Write((uint)176400);
            binaryWriter.Write((ushort)4);
            binaryWriter.Write((ushort)16);
        });
    }

    [Fact]
    public void GetSize() {
        var subject = new WaveCreator() {
            BeatsPerMinute = 60,
            Notes = {
                new Note(Pitch.C, 4, new Fraction(1, 1), new Fraction(23, 1))
            }
        };

        Assert.Equal((uint)4233644, subject.GetSize());
    }
}
