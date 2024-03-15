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
}
