namespace Noisier;

public class LinearAmplitudeDecrease : IEffect {
    public double Apply(double baseAmplitude, double fragmentPlayed) => baseAmplitude * (1 - fragmentPlayed);
}
