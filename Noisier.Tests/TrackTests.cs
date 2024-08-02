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
    public void GetAmplitude(int position, double expectedAmplitude) {
        var subject = new Track() {
            Positions = [new(0, 1)],
            WaveformCalculator = (_, _) => 1,
            VolumeCalculator = (_, _) => 1,
            Notes = {
                new(new(0, 1), new(1, 1), new Pitch(PitchClass.C, 4)),
                new(new(3, 2), new(1, 1), new(PitchClass.E, 4), new(PitchClass.E, 4) ),
                new(new(2, 1), new(1, 1), new Pitch(PitchClass.G, 4))
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }

    [Theory]
    [InlineData(49, 0)]
    [InlineData(50, 1)]
    [InlineData(99, 1)]
    [InlineData(100, 2)]
    [InlineData(149, 2)]
    [InlineData(150, 1)]
    [InlineData(199, 1)]
    [InlineData(200, 0)]
    public void GetAmplitude_Positions(int position, double expectedAmplitude) {
        var subject = new Track() {
            Positions = [new(1, 1), new(1, 2)],
            WaveformCalculator = (_, _) => 1,
            VolumeCalculator = (_, _) => 1,
            Notes = {
                new(new(0, 1), new(1, 1), new Pitch(PitchClass.A, 4))
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }

    [Theory]
    [InlineData(10, 22)]
    [InlineData(20, 44)]
    [InlineData(100, 220)]
    [InlineData(200, 440)]
    public void GetAmplitude_WaveformCalculator(int position, double expectedAmplitude) {
        var subject = new Track() {
            Positions = [new(0, 1)],
            WaveformCalculator = (timePoint, frequency) => timePoint * frequency,
            VolumeCalculator = (_, _) => 1,
            Notes = {
                new(new(0, 1), new(4, 1), new Pitch(PitchClass.A, 4))
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }

    [Theory]
    [InlineData(0, 400)]
    [InlineData(100, 300)]
    [InlineData(399, 1)]
    public void GetAmplitude_VolumeCalculator(int position, double expectedAmplitude) {
        var subject = new Track() {
            Positions = [new(0, 1)],
            WaveformCalculator = (_, _) => 1,
            VolumeCalculator = (noteDuration, relativePosition) => noteDuration - relativePosition,
            Notes = {
                new(new(0, 1), new(4, 1), new Pitch(PitchClass.C, 4))
            }
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(position, 200, 100));
    }
}
