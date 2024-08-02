namespace Noisier;

public static class VolumeCalculators {
    public const int DefaultVolume = 10000;

    public static VolumeCalculator Constant(int volume = DefaultVolume) => (_, _) => volume;
    public static VolumeCalculator LinearDecrease(int volume = DefaultVolume) => (noteDuration, relativePosition) => volume * (1 - relativePosition / noteDuration);
    public static VolumeCalculator Sine(int volume = DefaultVolume) => (noteDuration, relativePosition) => volume * Math.Sin(relativePosition / noteDuration * Math.PI);
}
