using System.Security.Cryptography;

namespace Noisier;

public class NoteGenerator {
    private const int iterations = 1000;
    private const int baseOctave = 4;
    private const uint duration = 8;
    private const uint denominator = 4;
    private const uint bandwidth = 2;

    private readonly static byte[] salt = Enumerable.Range(0, 20).Select(i => (byte)(8 + i * 4)).ToArray();

    public static IEnumerable<Note> Generate(string value, IList<PitchClass> scale) {
        using var pbkdf2 = new Rfc2898DeriveBytes(value, salt, iterations, HashAlgorithmName.SHA256);

        var note = GenerateNote(pbkdf2, new Fraction(0, denominator), scale);

        while (note.Position.Value + note.Duration.Value <= duration) {
            yield return note;

            note = GenerateNote(pbkdf2, note.Position + note.Duration, scale);
        }
    }

    private static Note GenerateNote(Rfc2898DeriveBytes pbkdf2, Fraction position, IList<PitchClass> scale) {
        var pitchClassIndex = (int)(pbkdf2.GetBytes(1).Single() % (scale.Count * bandwidth) - (scale.Count * bandwidth) / 2);
        var octave = baseOctave + pitchClassIndex / scale.Count;
        var pitchClass = scale[(pitchClassIndex + scale.Count) % scale.Count];

        if (pitchClassIndex < 0) {
            octave--;
        }

        return new Note(position, new Fraction((uint)pbkdf2.GetBytes(1).Single() % 4 + 1, denominator), new Pitch(pitchClass, octave));
    }
}
