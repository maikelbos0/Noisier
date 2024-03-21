using Xunit;

namespace Noisier.Tests;

public class WaveformCalculatorsTests {
    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(0.005, 10, 0.31)]
    [InlineData(0.02, 10, 0.95)]
    [InlineData(0.025, 10, 1)]
    [InlineData(0.05, 10, 0)]
    [InlineData(0.075, 10, -1)]
    [InlineData(0.1, 10, 0)]
    public void Sine(double timePoint, double frequency, double expectedAmplitude) {
        Assert.Equal(expectedAmplitude, WaveformCalculators.Sine()(timePoint, frequency), 2);
    }

    [Theory]
    [InlineData(0, 10, 0.87)]
    [InlineData(0.005, 10, 0.7)]
    [InlineData(0.02, 10, 0.65)]
    [InlineData(0.025, 10, 0.5)]
    [InlineData(0.05, 10, -0.87)]
    [InlineData(0.075, 10, -0.5)]
    [InlineData(0.1, 10, 0.87)]
    public void Piano(double timePoint, double frequency, double expectedAmplitude) {
        Assert.Equal(expectedAmplitude, WaveformCalculators.Piano()(timePoint, frequency), 2);
    }

    [Theory]
    [InlineData(0, 10, 0.72)]
    [InlineData(0.005, 10, 0.51)]
    [InlineData(0.02, 10, 0.49)]
    [InlineData(0.025, 10, 0.3)]
    [InlineData(0.05, 10, -0.58)]
    [InlineData(0.075, 10, 0.55)]
    [InlineData(0.1, 10, 0.72)]
    public void Horn(double timePoint, double frequency, double expectedAmplitude) {
        Assert.Equal(expectedAmplitude, WaveformCalculators.Horn()(timePoint, frequency), 2);
    }
}
