﻿using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

var noteGenerator = new NoteGenerator("Memphis", Scales.CMajor);
var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.BeatsPerMinute = 30;
waveCreator.Tracks.Add(new() {
    Positions = [new(1, 1)],
    WaveformCalculator = WaveformCalculators.Piano(),
    VolumeCalculator = VolumeCalculators.LinearDecrease(),
    Notes = noteGenerator.Generate().ToList()
});

var bla = waveCreator.Tracks[0].Notes.SelectMany(note => note.Pitches).ToList();

//waveCreator.Tracks.Add(new() {
//    Positions = [new(1, 1)],
//    WaveformCalculator = WaveformCalculators.Piano(),
//    VolumeCalculator = VolumeCalculators.LinearDecrease(),
//    Notes = [
//        new(new(0, 4), new(1, 1), new(PitchClass.C, 3), new(PitchClass.E, 3), new(PitchClass.G, 3)),
//        new(new(4, 4), new(1, 1), new Pitch(PitchClass.D, 3)),
//        new(new(5, 4), new(1, 1), new Pitch(PitchClass.E, 3)),
//        new(new(6, 4), new(1, 1), new Pitch(PitchClass.F, 3)),
//        new(new(7, 4), new(1, 1), new Pitch(PitchClass.G, 3)),
//        new(new(8, 4), new(1, 1), new(PitchClass.A, 3), new(PitchClass.C, 4), new(PitchClass.E, 4)),
//        new(new(12, 4), new(1, 1), new Pitch(PitchClass.B, 3)),
//        new(new(14, 4), new(1, 1), new(PitchClass.C, 4), new(PitchClass.E, 4), new(PitchClass.G, 4)),
//    ]
//});
//waveCreator.Tracks.Add(new() {
//    Positions = [new(1, 1)],
//    WaveformCalculator = WaveformCalculators.Horn(),
//    VolumeCalculator = VolumeCalculators.Sine(),
//    Notes = [
//        new(new(0, 4), new(2, 1), new Pitch(PitchClass.C, 2)),
//        new(new(8, 4), new(1, 1), new Pitch(PitchClass.A, 2)),
//        new(new(12, 4), new(1, 1), new Pitch(PitchClass.C, 3)),
//    ]
//});

waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
