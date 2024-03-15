﻿namespace Noisier;

public class Track {
    public List<Note> Notes { get; set; } = [];
    public WaveformCalculator WaveformCalculator { get; set; } = (timePoint, frequency) => Math.Sin(timePoint * frequency * 2 * Math.PI);
    public VolumeCalculator VolumeCalculator { get; set; } = VolumeCalculators.Constant();

    public double GetAmplitude(uint position, double frequency, double beatDuration) {
        var timePoint = position / frequency;
        
        return Notes.Where(note => position >= note.Position.Value * beatDuration && position < (note.Position.Value + note.Duration.Value) * beatDuration)
            .Sum(note => VolumeCalculator(note.Duration.Value * beatDuration, position - note.Position.Value * beatDuration) * note.Pitches.Sum(pitch => WaveformCalculator(timePoint, pitch.Frequency)));
    }
}
