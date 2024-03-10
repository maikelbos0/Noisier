using Xunit;

namespace Noisier.Tests;

public class NoteLinearAmplitudeDecreaseTests {
    [Theory]
    [InlineData(0.5, 0.4, 0.3)]
    [InlineData(1, 0.3, 0.7)]
    public void Apply(double baseAmplitude, double fragmentPlayed, double expectedResult) {
        var subject = new NoteLinearAmplitudeDecrease();

        Assert.Equal(expectedResult, subject.Apply(baseAmplitude, fragmentPlayed));
    }
}
