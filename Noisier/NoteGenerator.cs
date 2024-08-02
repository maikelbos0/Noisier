using System.Security.Cryptography;

namespace Noisier;

public class NoteGenerator : IDisposable {
    private const int iterations = 1000;
    private const int baseOctave = 4;
    private const int duration = 8;
    private const int denominator = 4;
    private const int bandwidth = 2;

    private static readonly byte[] salt = Enumerable.Range(0, 20).Select(i => (byte)(8 + i * 4)).ToArray();

    private readonly Rfc2898DeriveBytes pbkdf2;
    private readonly IList<PitchClass> scale;

    public NoteGenerator(string value, IList<PitchClass> scale) {
        pbkdf2 = new Rfc2898DeriveBytes(value, salt, iterations, HashAlgorithmName.SHA256);
        this.scale = scale;
    }

    public IEnumerable<Note> Generate() {
        var note = GenerateNote(new Fraction(0, denominator));

        while (note.Position.Value + note.Duration.Value <= duration) {
            yield return note;

            note = GenerateNote(note.Position + note.Duration);
        }
    }

    private Note GenerateNote(Fraction position) {
        var pitchClassIndex = (int)(pbkdf2.GetBytes(1).Single() % (scale.Count * bandwidth) - (scale.Count * bandwidth) / 2);
        var octave = baseOctave + pitchClassIndex / scale.Count;
        var pitchClass = scale[(pitchClassIndex + scale.Count) % scale.Count];

        if (pitchClassIndex < 0) {
            octave--;
        }

        return new Note(position, new Fraction(pbkdf2.GetBytes(1).Single() % 4 + 1, denominator), new Pitch(pitchClass, octave));
    }

    public void Dispose() {
        pbkdf2.Dispose();
        GC.SuppressFinalize(this);
    }
}
