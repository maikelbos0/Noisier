namespace Noisier;

public record Fraction(uint Numerator, uint Denominator) {
    public double Value => Numerator / (double)Denominator;
};
