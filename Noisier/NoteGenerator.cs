using System.Security.Cryptography;

namespace Noisier;

public class NoteGenerator {
    private const int iterations = 1000;
    private const uint baseOctave = 4;
    private const uint duration = 8;
    private const uint denominator = 4;

    private readonly static byte[] salt = Enumerable.Range(0, 20).Select(i => (byte)(8 + i * 4)).ToArray();

    public static IEnumerable<Note> Generate(string value) {
        using var pbkdf2 = new Rfc2898DeriveBytes(value, salt, iterations, HashAlgorithmName.SHA256);

        var note = GenerateNote(pbkdf2, new Fraction(0, denominator));

        while (note.Position.Value + note.Duration.Value <= duration) {
            yield return note;

            note = GenerateNote(pbkdf2, note.Position + note.Duration);
        }
    }

    private static Note GenerateNote(Rfc2898DeriveBytes pbkdf2, Fraction position) {
        return new Note(position, new Fraction((uint)pbkdf2.GetBytes(1).Single() % 4 + 1, denominator), new Pitch(PitchClass.C, 4));
    }
}
