using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.BeatsPerMinute = 30;
waveCreator.Tracks.Add(new() {
    VolumeCalculator = (noteDuration, relativePosition) => 10000 * (1 - relativePosition / noteDuration),
    Notes = [
        new() { Pitches = { new(PitchClass.C, 3), new(PitchClass.E, 3), new(PitchClass.G, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(0, 4) },
        new() { Pitches = { new(PitchClass.D, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(4, 4) },
        new() { Pitches = { new(PitchClass.E, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(5, 4) },
        new() { Pitches = { new(PitchClass.F, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(6, 4) },
        new() { Pitches = { new(PitchClass.G, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(7, 4) },
        new() { Pitches = { new(PitchClass.A, 3), new(PitchClass.C, 4), new(PitchClass.E, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(8, 4) },
        new() { Pitches = { new(PitchClass.B, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(12, 4) },
        new() { Pitches = { new(PitchClass.C, 4), new(PitchClass.E, 4), new(PitchClass.G, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(14, 4) },
    ]
});

waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
