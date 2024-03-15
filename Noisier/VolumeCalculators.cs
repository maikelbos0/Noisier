namespace Noisier;

public static class VolumeCalculators {
    public const uint DefaultVolume = 10000;

    public static VolumeCalculator Constant(uint volume = DefaultVolume) => (_, _) => volume;
    public static VolumeCalculator LinearDecrease(uint volume = DefaultVolume) => (noteDuration, relativePosition) => volume * (1 - relativePosition / noteDuration);
}
