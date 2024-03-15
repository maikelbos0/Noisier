namespace Noisier;

public static class WaveformCalculators {
    public static WaveformCalculator Sine() => (timePoint, frequency) => Math.Sin(timePoint * frequency * 2 * Math.PI);

    public static WaveformCalculator Piano() => (timePoint, frequency) => Math.Pow(Math.Sin(timePoint * frequency * 2 * Math.PI), 3) + Math.Sin((timePoint * frequency * 2 + 2 / 3.0) * Math.PI);
}
