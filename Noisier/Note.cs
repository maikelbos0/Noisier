namespace Noisier;

public record Note(Fraction Position, Fraction Duration, params Pitch[] Pitches);
