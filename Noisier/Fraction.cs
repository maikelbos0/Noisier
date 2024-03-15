namespace Noisier;

public record Fraction(uint Numerator, uint Denominator) {
    public double Value { get; } = Numerator / (double)Denominator;
};
