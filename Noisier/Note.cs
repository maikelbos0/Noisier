namespace Noisier;

public class Note {
    public List<Pitch> Pitches { get; set; } = [];
    public required Fraction Duration { get; set; }
    public required Fraction Position { get; set; }
}
