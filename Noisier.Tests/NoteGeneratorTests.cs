using NSubstitute;
using System.Security.Cryptography;
using Xunit;

namespace Noisier.Tests;

public class NoteGeneratorTests {
    [Theory]
    [InlineData(0, 100, 0, 0)]
    [InlineData(0, 100, 99, 99)]
    [InlineData(0, 100, 50, 150)]
    [InlineData(500, 600, 550, 150)]

    [InlineData(0, 500, 0, 0, 0)]
    [InlineData(0, 500, 499, 1, 243)]
    [InlineData(0, 500, 250, 43, 242)]
    [InlineData(2500, 3000, 2750, 43, 242)]

    [InlineData(0, 100000, 0, 0, 0, 0)]
    [InlineData(0, 100000, 99999, 1, 134, 159)]
    [InlineData(0, 100000, 50000, 0, 195, 80)]
    [InlineData(500000, 600000, 550000, 0, 195, 80)]
    public void GetValueReturnsValueBetweenMinimumAndMaximum(uint includingMinimum, uint excludingMaximum, uint expectedValue, params int[] bytes) {
        var deriveBytes = Substitute.For<DeriveBytes>();
        var subject = new NoteGenerator("", Scales.CMajor) {
            DeriveBytes = deriveBytes
        };

        deriveBytes.GetBytes(Arg.Any<int>()).Returns(bytes.Select(b => (byte)b).ToArray());

        Assert.Equal(expectedValue, subject.GetValue(includingMinimum, excludingMaximum));
    }

    [Fact]
    public void GetValueEliminatesBias() {
        var deriveBytes = Substitute.For<DeriveBytes>();
        var subject = new NoteGenerator("", Scales.CMajor) {
            DeriveBytes = deriveBytes
        };

        deriveBytes.GetBytes(Arg.Any<int>()).Returns(
            [253, 232], // 65000
            [253, 231] // 64999
        );

        Assert.Equal<uint>(999, subject.GetValue(0, 1000));
    }
}
