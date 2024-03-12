namespace Noisier;

// TODO add waveform switch?
public class Note {
    public List<Pitch> Pitches { get; set; } = [];
    public required Fraction Duration { get; set; }
    public required Fraction Position { get; set; }
    public List<IEffect> Effects { get; set; } = [];

    public double GetAmplitude(double timePoint, double fragmentPlayed) {
        var amplitude = Pitches.Sum(pitch => GetPitchAmplitude(pitch, timePoint, fragmentPlayed));

        return Effects.Aggregate(amplitude, (amplitude, effect) => effect.Apply(amplitude, fragmentPlayed));
    }

    public virtual double GetPitchAmplitude(Pitch pitch, double timePoint, double fragmentPlayed)
        => Math.Sin(timePoint * pitch.Frequency * 2 * Math.PI);
}
