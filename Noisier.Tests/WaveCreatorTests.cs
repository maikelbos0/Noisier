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
    [InlineData(240, 1, 1, 44100)]
    [InlineData(240, 1, 2, 22050)]
    [InlineData(240, 2, 1, 88200)]
    public void ChunkSize_Fraction(uint beatsPerMinute, uint numerator, uint denominator, uint expectedChunkSize) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute,
            Notes = {
                new Note(Pitch.C, 4, new Fraction(numerator, denominator))
            }
        };

        Assert.Equal(expectedChunkSize, subject.ChunkSize);
    }

    [Theory]
    [InlineData(240, 1, 44100)]
    [InlineData(60, 1, 176400)]
    [InlineData(240, 4, 176400)]
    public void ChunkSize_MultipleNotes(uint beatsPerMinute, int noteCount, uint expectedChunkSize) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute,
            Notes = Enumerable.Repeat(new Note(Pitch.C, 4, new Fraction(1, 1)), noteCount).ToList()
        };

        Assert.Equal(expectedChunkSize, subject.ChunkSize);
    }

    [Fact]
    public void GetSize() {
        var subject = new WaveCreator() {
            BeatsPerMinute = 60,
            Notes = Enumerable.Repeat(new Note(Pitch.C, 4, new Fraction(1, 1)), 24).ToList()
        };

        Assert.Equal((uint)4233644, subject.GetSize());
    }
}
