using System.Security.Cryptography;

namespace Noisier;

public class NoteGenerator : IDisposable {
    private const int iterations = 1000;
    private const int baseOctave = 4;
    private const int duration = 8;
    private const int denominator = 4;
    private const int bandwidth = 2;

    private static readonly byte[] salt = Enumerable.Range(0, 20).Select(i => (byte)(8 + i * 4)).ToArray();

    public DeriveBytes DeriveBytes { get; internal set; }
    public IList<PitchClass> Scale { get; set; }

    public NoteGenerator(string value, IList<PitchClass> scale) {
        DeriveBytes = new Rfc2898DeriveBytes(value, salt, iterations, HashAlgorithmName.SHA256);
        Scale = scale;
    }

    public IEnumerable<Note> Generate() {
        var note = GenerateNote(new Fraction(0, denominator));

        while (note.Position.Value + note.Duration.Value <= duration) {
            yield return note;

            note = GenerateNote(note.Position + note.Duration);
        }
    }

    private Note GenerateNote(Fraction position) {
        var pitchClassIndex = (int)(DeriveBytes.GetBytes(1).Single() % (Scale.Count * bandwidth) - (Scale.Count * bandwidth) / 2);
        var octave = baseOctave + pitchClassIndex / Scale.Count;
        var pitchClass = Scale[(pitchClassIndex + Scale.Count) % Scale.Count];

        if (pitchClassIndex < 0) {
            octave--;
        }

        return new Note(position, new Fraction(DeriveBytes.GetBytes(1).Single() % 4 + 1, denominator), new Pitch(pitchClass, octave));
    }

    internal uint GetValue(uint includingMinimum, uint excludingMaximum) {
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


        return value % range + includingMinimum;

        uint GetBiasedValue() => DeriveBytes.GetBytes(byteCount).Aggregate((uint)0, (value, b) => (value << 8) + b);
    }

    public void Dispose() {
        DeriveBytes.Dispose();
        GC.SuppressFinalize(this);
    }
}
