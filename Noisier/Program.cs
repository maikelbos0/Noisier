using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

//var effects = new List<IEffect>() { new LinearAmplitudeDecrease() };
var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.BeatsPerMinute = 30;
waveCreator.Tracks.Add(new() {
    Notes = [
        new() { Pitches = { new(PitchClass.C, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(0, 1) },
        new() { Pitches = { new(PitchClass.D, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(1, 1) },
        new() { Pitches = { new(PitchClass.E, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(2, 1) },
        new() { Pitches = { new(PitchClass.F, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(3, 1) },
        new() { Pitches = { new(PitchClass.G, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(4, 1) },
        new() { Pitches = { new(PitchClass.A, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(5, 1) },
        new() { Pitches = { new(PitchClass.B, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(6, 1) },
        new() { Pitches = { new(PitchClass.C, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(7, 1) },
        //new() { Pitches = { new(PitchClass.C, 3), new(PitchClass.E, 3), new(PitchClass.G, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(0, 4) },
        //new() { Pitches = { new(PitchClass.D, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(4, 4) },
        //new() { Pitches = { new(PitchClass.E, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(5, 4) },
        //new() { Pitches = { new(PitchClass.F, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(6, 4) },
        //new() { Pitches = { new(PitchClass.G, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(7, 4) },
        //new() { Pitches = { new(PitchClass.A, 3), new(PitchClass.C, 4), new(PitchClass.E, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(8, 4) },
        //new() { Pitches = { new(PitchClass.B, 3) }, Duration = new Fraction(1, 1), Position = new Fraction(12, 4) },
        //new() { Pitches = { new(PitchClass.C, 4), new(PitchClass.E, 4), new(PitchClass.G, 4) }, Duration = new Fraction(1, 1), Position = new Fraction(14, 4) },
    ]
});

//foreach (var note in waveCreator.Notes) {
//    note.Effects = effects;
//}

waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
