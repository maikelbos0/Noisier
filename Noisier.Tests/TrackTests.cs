using Xunit;

namespace Noisier.Tests;

public class TrackTests {
    [Theory]
    [InlineData(0, 1)]
    [InlineData(99, 1)]
    [InlineData(100, 0)]
    [InlineData(149, 0)]
    [InlineData(150, 2)]
    [InlineData(199, 2)]
    [InlineData(200, 3)]
    [InlineData(249, 3)]
    [InlineData(250, 1)]
    [InlineData(299, 1)]
    [InlineData(300, 0)]
    public void GetAmplitude(uint position, double expectedAmplitude) {
        var subject = new Track() {
            WaveformCalculator = (double timePoint, double frequency) => 1,
            VolumeCalculator = () => 1,
            Notes = {
                new() { Pitches = { new(PitchClass.C, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(0, 1) },
                new() { Pitches = { new(PitchClass.C, 4), new(PitchClass.E, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(3, 2) },
                new() { Pitches = { new(PitchClass.G, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(2, 1) },
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 100, 60));
    }
}
