namespace Noisier;

public static class WaveformCalculators {
    public static WaveformCalculator Sine() => (timePoint, frequency) => Math.Sin(timePoint * frequency * 2 * Math.PI);
}
