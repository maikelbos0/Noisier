namespace Noisier;

public static class WaveformCalculators {
    public static WaveformCalculator Sine() => (timePoint, frequency) 
        => Math.Sin(timePoint * frequency * 2 * Math.PI);

    public static WaveformCalculator Piano() => (timePoint, frequency) 
        => Math.Pow(Math.Sin(timePoint * frequency * 2 * Math.PI), 3) 
            + Math.Sin((timePoint * frequency * 2 + 2 / 3.0) * Math.PI);

    public static WaveformCalculator Horn() => (timePoint, frequency)
        => Math.Pow(Math.Sin((timePoint * frequency * 2 + 2 / 3.0) * Math.PI), 3)
            + 0.5 * Math.Pow(Math.Sin((timePoint * frequency * 2 - 7 / 8.0) * Math.PI), 2);
}
