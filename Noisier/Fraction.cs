namespace Noisier;

public record Fraction(int Numerator, int Denominator) {
    public double Value { get; } = Numerator / (double)Denominator;

    public static Fraction operator +(Fraction a, Fraction b) {
        var denominator = Enumerable.Range(1, a.Denominator * b.Denominator)
            .First(candidate => candidate % a.Denominator == 0 && candidate % b.Denominator == 0);

        return new(a.Numerator * denominator / b.Denominator + b.Numerator * denominator / a.Denominator, denominator);
    }
};
