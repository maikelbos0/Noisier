using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.BeatsPerMinute = 30;
waveCreator.Tracks.Add(new() {
    VolumeCalculator = (noteDuration, relativePosition) => 10000 * (1 - relativePosition / noteDuration),
    Notes = [
        new(new(0, 4), new(1, 1), new(PitchClass.C, 3), new(PitchClass.E, 3), new(PitchClass.G, 3)),
        new(new(4, 4), new(1, 1), new Pitch(PitchClass.D, 3)),
        new(new(5, 4), new(1, 1), new Pitch(PitchClass.E, 3)),
        new(new(6, 4), new(1, 1), new Pitch(PitchClass.F, 3)),
        new(new(7, 4), new(1, 1), new Pitch(PitchClass.G, 3)),
        new(new(8, 4), new(1, 1), new(PitchClass.A, 3), new(PitchClass.C, 4), new(PitchClass.E, 4)),
        new(new(12, 4), new(1, 1), new Pitch(PitchClass.B, 3)),
        new(new(14, 4), new(1, 1), new(PitchClass.C, 4), new(PitchClass.E, 4), new(PitchClass.G, 4)),
    ]
});

waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
