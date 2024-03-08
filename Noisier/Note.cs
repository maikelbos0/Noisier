namespace Noisier;

public record Note(Pitch Pitch, int Octave) {
    public const double A4 = 440;
    public const int PitchesPerOctave = 12;

    public double Frequency => A4 * Math.Pow(2, ((Octave - 4) * PitchesPerOctave + (int)Pitch - (int)Pitch.A) / (double)PitchesPerOctave);
}
