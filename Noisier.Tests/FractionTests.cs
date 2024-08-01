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

    [Theory]
    [InlineData(0, 1, 0, 1, 0, 1)]
    [InlineData(0, 1, 1, 1, 1, 1)]
    [InlineData(1, 1, 0, 1, 1, 1)]
    [InlineData(1, 1, 1, 1, 2, 1)]
    [InlineData(1, 4, 1, 4, 2, 4)]
    [InlineData(1, 4, 1, 3, 7, 12)]
    [InlineData(1, 3, 1, 4, 7, 12)]
    public void Plus(uint numeratorA, uint denominatorA, uint numeratorB, uint denominatorB, uint expectedNumerator, uint expectedDenominator) {
        var subjectA = new Fraction(numeratorA, denominatorA);
        var subjectB = new Fraction(numeratorB, denominatorB);

        var result = subjectA + subjectB;

        Assert.Equal(expectedNumerator, result.Numerator);
        Assert.Equal(expectedDenominator, result.Denominator);
    }
}
