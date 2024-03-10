namespace Noisier;

// TODO add waveform switch?
public class Note {
    public List<Pitch> Pitches { get; set; } = [];
    public required Fraction Duration { get; set; }
    public required Fraction Position { get; set; }
    public List<IEffect> Effects { get; set; } = [];

    public double GetBaseAmplitude(double timePoint, double fragmentPlayed) {
        var baseAmplitude = Pitches.Sum(pitch => Math.Sin(timePoint * pitch.Frequency * 2 * Math.PI));

        return Effects.Aggregate(baseAmplitude, (baseAmplitude, effect) => effect.Apply(baseAmplitude, fragmentPlayed));
    }
}
