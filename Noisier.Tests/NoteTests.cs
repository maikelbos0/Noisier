using NSubstitute;
using Xunit;

namespace Noisier.Tests;

public class NoteTests {

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0.25 / 440.0, 1)]
    [InlineData(0.5 / 440.0, 0)]
    [InlineData(0.75 / 440.0, -1)]
    [InlineData(1 / 440.0, 0)]
    public void GetAmplitude(double timePoint, double expectedAmplitude) {
        var subject = new Note() {
            Pitches = { new(PitchClass.A, 4) },
            Duration = new Fraction(1, 1),
            Position = new Fraction(0, 1)
        };

        Assert.Equal(expectedAmplitude, subject.GetAmplitude(timePoint, 0), 2);
    }

    // TODO test for multiple pitches, test pitch amplitude

    [Fact]
    public void GetAmplitude_Applies_Effects() {
        var subject = new Note() {
            Pitches = { new(PitchClass.A, 4) },
            Duration = new Fraction(1, 1),
            Position = new Fraction(0, 1),
            Effects = { Substitute.For<IEffect>(), Substitute.For<IEffect>() }
        };

        subject.Effects[0].Apply(Arg.Any<double>(), Arg.Any<double>()).Returns(callInfo => callInfo.ArgAt<double>(0) + 0.5);
        subject.Effects[1].Apply(Arg.Any<double>(), Arg.Any<double>()).Returns(callInfo => callInfo.ArgAt<double>(0) / 2);

        Assert.Equal(0.75, subject.GetAmplitude(0.25 / 440.0, 0.4), 2);
    }
}
