using Xunit;

namespace Noisier.Tests;

public class PitchTests {
    [Theory]
    [InlineData(PitchClass.A, 3, 220.0)]
    [InlineData(PitchClass.C, 4, 261.63)]
    [InlineData(PitchClass.CSharp, 4, 277.18)]
    [InlineData(PitchClass.D, 4, 293.66)]
    [InlineData(PitchClass.DSharp, 4, 311.13)]
    [InlineData(PitchClass.E, 4, 329.63)]
    [InlineData(PitchClass.F, 4, 349.23)]
    [InlineData(PitchClass.FSharp, 4, 369.99)]
    [InlineData(PitchClass.G, 4, 392.0)]
    [InlineData(PitchClass.GSharp, 4, 415.3)]
    [InlineData(PitchClass.A, 4, 440.0)]
    [InlineData(PitchClass.ASharp, 4, 466.16)]
    [InlineData(PitchClass.B, 4, 493.88)]
    [InlineData(PitchClass.A, 5, 880.0)]
    public void Frequency(PitchClass pitchClass, int octave, double expectedFrequency) {
        var subject = new Pitch(pitchClass, octave);

        Assert.Equal(expectedFrequency, subject.Frequency, 2);
    }
}
