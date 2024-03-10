using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

var effects = new List<IEffect>() { new LinearAmplitudeDecrease() };
var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.BeatsPerMinute = 30;
waveCreator.Notes = [
    new Note(Pitch.C, 3, new Fraction(1, 1), new Fraction(0, 1)),
    new Note(Pitch.E, 3, new Fraction(1, 1), new Fraction(0, 1)),
    new Note(Pitch.G, 3, new Fraction(1, 1), new Fraction(0, 1)),

    new Note(Pitch.D, 3, new Fraction(1, 1), new Fraction(1, 1)),
    new Note(Pitch.E, 3, new Fraction(1, 1), new Fraction(5, 4)),
    new Note(Pitch.F, 3, new Fraction(1, 1), new Fraction(6, 4)),
    new Note(Pitch.G, 3, new Fraction(1, 1), new Fraction(7, 4)),
    
    new Note(Pitch.A, 3, new Fraction(1, 1), new Fraction(2, 1)),
    new Note(Pitch.C, 4, new Fraction(1, 1), new Fraction(2, 1)),
    new Note(Pitch.E, 4, new Fraction(1, 1), new Fraction(2, 1)),

    new Note(Pitch.B, 3, new Fraction(1, 1), new Fraction(3, 1)),
    
    new Note(Pitch.C, 4, new Fraction(1, 1), new Fraction(7, 2)),
    new Note(Pitch.E, 4, new Fraction(1, 1), new Fraction(7, 2)),
    new Note(Pitch.G, 4, new Fraction(1, 1), new Fraction(7, 2)),
];

foreach (var note in waveCreator.Notes) {
    note.Effects = effects;
}

waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
