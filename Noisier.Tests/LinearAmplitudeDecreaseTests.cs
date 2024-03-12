using Xunit;

namespace Noisier.Tests;

public class LinearAmplitudeDecreaseTests {
    [Theory]
    [InlineData(0.5, 0.4, 0.3)]
    [InlineData(1, 0.3, 0.7)]
    public void Apply(double amplitude, double fragmentPlayed, double expectedResult) {
        var subject = new LinearAmplitudeDecrease();

        Assert.Equal(expectedResult, subject.Apply(amplitude, fragmentPlayed));
    }
}
