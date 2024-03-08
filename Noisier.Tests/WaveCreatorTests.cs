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
}
