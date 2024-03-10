namespace Noisier;

// TODO add chord effect
// TODO add waveform switch?
public record Note(Pitch Pitch, int Octave, Fraction Duration, Fraction Position) {
    private const double a4Frequency = 440;
    private const int pitchesPerOctave = 12;

    public List<IEffect> Effects { get; set; } = new();
    public double Frequency => a4Frequency * Math.Pow(2, ((Octave - 4) * pitchesPerOctave + (int)Pitch - (int)Pitch.A) / (double)pitchesPerOctave);

    public double GetBaseAmplitude(double timePoint, double fragmentPlayed) 
        => Effects.Aggregate(Math.Sin(timePoint * Frequency * 2 * Math.PI), (baseAmplitude, effect) => effect.Apply(baseAmplitude, fragmentPlayed));
}
