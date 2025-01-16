using System.Security.Cryptography;

namespace Noisier;

public class NoteGenerator : IDisposable {
    private const int iterations = 1000;
    private const int baseOctave = 4;
    private const int duration = 8;
    private const int denominator = 4;
    private const int bandwidth = 7;

    private static readonly byte[] salt = Enumerable.Range(0, 20).Select(i => (byte)(8 + i * 4)).ToArray();

    public DeriveBytes DeriveBytes { get; internal set; }
    public IList<PitchClass> Scale { get; set; }

    public NoteGenerator(string value, IList<PitchClass> scale) {
        DeriveBytes = new Rfc2898DeriveBytes(value, salt, iterations, HashAlgorithmName.SHA256);
        Scale = scale;
    }

    // TODO adjust bandwidth???
    // TODO add pauses, maybe chords somehow?
    // TODO add tests
    public IEnumerable<Note> Generate() {
        var note = GenerateNote(new Fraction(0, denominator));

        while (note.Position.Value + note.Duration.Value <= duration) {
            yield return note;

            note = GenerateNote(note.Position + note.Duration);
        }
    }

    internal Note GenerateNote(Fraction position) {
        var pitchClassIndex = GetValue(-bandwidth, bandwidth * 2 + 1);
        var octave = baseOctave + pitchClassIndex / Scale.Count;
        var pitchClass = Scale[(pitchClassIndex + Scale.Count) % Scale.Count];

        if (pitchClassIndex < 0) {
            octave--;
        }

        return new Note(position, new Fraction(GetValue(1, 5), denominator), new Pitch(pitchClass, octave));
    }

    internal int GetValue(int includingMinimum, int excludingMaximum) {
        var range = excludingMaximum - includingMinimum;
        var byteCount = range switch {
            < 1 << 8 => 1,
            < 1 << 16 => 2,
            < 1 << 24 => 3,
            _ => 4
        };

        var value = GetBiasedValue();

        while (value >= (1 << byteCount * 8) / range * range) {
            value = GetBiasedValue();
        }


        return (int)value % range + includingMinimum;

        uint GetBiasedValue() => DeriveBytes.GetBytes(byteCount).Aggregate((uint)0, (value, b) => (value << 8) + b);
    }

    public void Dispose() {
        DeriveBytes.Dispose();
        GC.SuppressFinalize(this);
    }
}
