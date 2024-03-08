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

        Assert.Equal(subject.NoteDuration, expectedDuration);
    }

    [Theory]
    [InlineData(240, 1, 44100)]
    [InlineData(60, 1, 176400)]
    [InlineData(240, 4, 176400)]
    public void ChunkSize(uint beatsPerMinute, int noteCount, uint expectedChunkSize) {
        var subject = new WaveCreator() {
            BeatsPerMinute = beatsPerMinute,
            Notes = Enumerable.Repeat(new Note(Pitch.C, 4), noteCount).ToList()
        };

        Assert.Equal(expectedChunkSize, subject.ChunkSize);
    }
}
