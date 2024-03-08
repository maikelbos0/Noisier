using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.BeatsPerMinute = 30;
waveCreator.Notes = [
    new Note(Pitch.C, 3, new Fraction(1, 1)),
    new Note(Pitch.D, 3, new Fraction(1, 4)),
    new Note(Pitch.E, 3, new Fraction(1, 4)),
    new Note(Pitch.F, 3, new Fraction(1, 4)),
    new Note(Pitch.G, 3, new Fraction(1, 4)),
    new Note(Pitch.A, 3, new Fraction(1, 1)),
    new Note(Pitch.B, 3, new Fraction(1, 2)),
    new Note(Pitch.C, 4, new Fraction(1, 2)),
];
waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
