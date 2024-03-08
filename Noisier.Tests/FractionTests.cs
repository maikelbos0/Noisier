using Xunit;

namespace Noisier.Tests;

public class FractionTests {
    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 2, 0.5)]
    [InlineData(2, 1, 2)]
    [InlineData(2, 3, 0.67)]
    public void Value(uint numerator, uint denominator, double expectedValue) {
        var subject = new Fraction(numerator, denominator);

        Assert.Equal(expectedValue, subject.Value, 2);
    }
}
