namespace Noisier;

public class LinearAmplitudeDecrease : IEffect {
    public double Apply(double amplitude, double fragmentPlayed) => amplitude * (1 - fragmentPlayed);
}
