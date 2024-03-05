namespace Noisier;

public record Frequency(Note Note, int Octave) {
    public const double A4 = 440;
    public const int NotesPerOctave = 12;

    public double Value => A4 * Math.Pow(2, ((Octave - 4) * NotesPerOctave + (int)Note - (int)Note.A) / (double)NotesPerOctave);
}
