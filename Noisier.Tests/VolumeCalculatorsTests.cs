using Xunit;

namespace Noisier.Tests;

public class VolumeCalculatorsTests {
    [Theory]
    [InlineData(400, 0, 10000)]
    [InlineData(400, 100, 10000)]
    [InlineData(400, 399, 10000)]
    public void Constant(double noteDuration, double relativePosition, double expectedVolume) {
        Assert.Equal(expectedVolume, VolumeCalculators.Constant()(noteDuration, relativePosition), 2);
    }

    [Theory]
    [InlineData(400, 0, 10000)]
    [InlineData(400, 100, 7500)]
    [InlineData(400, 399, 25)]
    public void LinearDecrease(double noteDuration, double relativePosition, double expectedVolume) {
        Assert.Equal(expectedVolume, VolumeCalculators.LinearDecrease()(noteDuration, relativePosition), 2);
    }
}
