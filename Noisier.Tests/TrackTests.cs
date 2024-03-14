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
            WaveformCalculator = (_, _) => 1,
            VolumeCalculator = (_, _) => 1,
            Notes = {
                new() { Pitches = { new(PitchClass.C, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(0, 1) },
                new() { Pitches = { new(PitchClass.C, 4), new(PitchClass.E, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(3, 2) },
                new() { Pitches = { new(PitchClass.G, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(2, 1) },
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }

    [Theory]
    [InlineData(10, 22)]
    [InlineData(20, 44)]
    [InlineData(100, 220)]
    [InlineData(200, 440)]
    public void GetAmplitude_WaveformCalculator(uint position, double expectedAmplitude) {
        var subject = new Track() {
            WaveformCalculator = (timePoint, frequency) => timePoint * frequency,
            VolumeCalculator = (_, _) => 1,
            Notes = {
                new() { Pitches = { new(PitchClass.A, 4) }, Duration = new Fraction(4, 1), Position = new Fraction(0, 1) }
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }

    [Theory]
    [InlineData(0, 400)]
    [InlineData(100, 300)]
    [InlineData(399, 1)]
    public void GetAmplitude_VolumeCalculator(uint position, double expectedAmplitude) {
        var subject = new Track() {
            WaveformCalculator = (_, _) => 1,
            VolumeCalculator = (noteDuration, noteProgress) => noteDuration - noteProgress,
            Notes = {
                new() { Pitches = { new(PitchClass.C, 1) }, Duration = new Fraction(4, 1), Position = new Fraction(0, 1) }
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }
}
