namespace Noisier;

public class NoteLinearAmplitudeDecrease : INoteEffect {
    public double Apply(double baseAmplitude, double fragmentPlayed) => baseAmplitude * (1 - fragmentPlayed);
}
