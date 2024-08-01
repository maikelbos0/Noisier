namespace Noisier;

public static class Scales {
    public static IList<PitchClass> CMajor => [
        PitchClass.C,
        PitchClass.D,
        PitchClass.E,
        PitchClass.F,
        PitchClass.G,
        PitchClass.A,
        PitchClass.B
    ];

    public static IList<PitchClass> CMinor => [
        PitchClass.C,
        PitchClass.D,
        PitchClass.EFlat,
        PitchClass.F,
        PitchClass.G,
        PitchClass.AFlat,
        PitchClass.BFlat
    ];
}
