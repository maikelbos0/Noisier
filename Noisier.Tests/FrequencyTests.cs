namespace Noisier.Tests; 

public class FrequencyTests {
    [Theory]
    [InlineData(Note.A, 3, 220.0)]
    [InlineData(Note.C, 4, 261.63)]
    [InlineData(Note.CSharp, 4, 277.18)]
    [InlineData(Note.D, 4, 293.66)]
    [InlineData(Note.DSharp, 4, 311.13)]
    [InlineData(Note.E, 4, 329.63)]
    [InlineData(Note.F, 4, 349.23)]
    [InlineData(Note.FSharp, 4, 369.99)]
    [InlineData(Note.G, 4, 392.0)]
    [InlineData(Note.GSharp, 4, 415.3)]
    [InlineData(Note.A, 4, 440.0)]
    [InlineData(Note.ASharp, 4, 466.16)]
    [InlineData(Note.B, 4, 493.88)]
    [InlineData(Note.A, 5, 880.0)]
    public void Value(Note note, int octave, double expectedFrequency) {
        var subject = new Frequency(note, octave);

        Assert.Equal(expectedFrequency, subject.Value, 2);
    }
}