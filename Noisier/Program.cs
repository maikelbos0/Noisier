using Noisier;
using System.Diagnostics;

const string vlcPath = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";

var waveCreator = new WaveCreator();
var path = @"C:\Temp\test.wav";

waveCreator.Create(path);

Process.Start(new ProcessStartInfo(vlcPath) {
    Arguments = path
});
