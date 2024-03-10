using NSubstitute;
using Xunit;

namespace Noisier.Tests;

public class NoteTests {
    [Theory]
    [InlineData(Pitch.A, 3, 220.0)]
    [InlineData(Pitch.C, 4, 261.63)]
    [InlineData(Pitch.CSharp, 4, 277.18)]
    [InlineData(Pitch.D, 4, 293.66)]
    [InlineData(Pitch.DSharp, 4, 311.13)]
    [InlineData(Pitch.E, 4, 329.63)]
    [InlineData(Pitch.F, 4, 349.23)]
    [InlineData(Pitch.FSharp, 4, 369.99)]
    [InlineData(Pitch.G, 4, 392.0)]
    [InlineData(Pitch.GSharp, 4, 415.3)]
    [InlineData(Pitch.A, 4, 440.0)]
    [InlineData(Pitch.ASharp, 4, 466.16)]
    [InlineData(Pitch.B, 4, 493.88)]
    [InlineData(Pitch.A, 5, 880.0)]
    public void Frequency(Pitch pitch, int octave, double expectedFrequency) {
        var subject = new Note(pitch, octave, new Fraction(1, 1), new Fraction(0, 1));

        Assert.Equal(expectedFrequency, subject.Frequency, 2);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0.25 / 440.0, 1)]
    [InlineData(0.5 / 440.0, 0)]
    [InlineData(0.75 / 440.0, -1)]
    [InlineData(1 / 440.0, 0)]
    public void GetBaseAmplitude(double timePoint, double expectedAmplitude) {
        var subject = new Note(Pitch.A, 4, new Fraction(1, 1), new Fraction(0, 1));

        Assert.Equal(expectedAmplitude, subject.GetBaseAmplitude(timePoint, 0), 2);
    }

    [Fact]
    public void GetBaseAmplitude_Applies_Effects() {
        var noteEffects = new List<INoteEffect>() {
            Substitute.For<INoteEffect>(),
            Substitute.For<INoteEffect>()
        };
        var subject = new Note(Pitch.A, 4, new Fraction(1, 1), new Fraction(0, 1)) {
            Effects = noteEffects
        };

        noteEffects[0].Apply(Arg.Any<double>(), Arg.Any<double>()).Returns(callInfo => callInfo.ArgAt<double>(0) + 0.5);
        noteEffects[1].Apply(Arg.Any<double>(), Arg.Any<double>()).Returns(callInfo => callInfo.ArgAt<double>(0) / 2);

        Assert.Equal(0.75, subject.GetBaseAmplitude(0.25 / 440.0, 0.4), 2);
    }
}
