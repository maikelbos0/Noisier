﻿namespace Noisier;


//track public List<Fraction> Positions { get; set; } = new();
public record Pitch(PitchClass PitchClass, int Octave) {
    private const double a4Frequency = 440;
    private const int pitchesPerOctave = 12;

    public double Frequency => a4Frequency * Math.Pow(2, ((Octave - 4) * pitchesPerOctave + (int)PitchClass - (int)PitchClass.A) / (double)pitchesPerOctave);
}
