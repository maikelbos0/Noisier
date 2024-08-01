namespace Noisier;

public record Fraction(uint Numerator, uint Denominator) {
    public double Value { get; } = Numerator / (double)Denominator;

    public static Fraction operator +(Fraction a, Fraction b) {
        var denominator = (uint)Enumerable.Range(1, (int)(a.Denominator * b.Denominator))
            .First(candidate => candidate % a.Denominator == 0 && candidate % b.Denominator == 0);

        return new(a.Numerator * denominator / b.Denominator + b.Numerator * denominator / a.Denominator, denominator);
    }
};
