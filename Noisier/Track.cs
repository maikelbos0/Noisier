namespace Noisier;

public class Track {
    public List<Note> Notes { get; set; } = new();
    public WaveformCalculator WaveformCalculator { get; set; } = (double timePoint, double frequency) => Math.Sin(timePoint * frequency * 2 * Math.PI);
    public VolumeCalculator VolumeCalculator { get; set; } = () => 10000;

    public double GetAmplitude(uint position, double frequency, uint beatsPerMinute) {
        var beatDuration = 60 * frequency / beatsPerMinute;
        var timePoint = position / frequency;
        
        return Notes.Where(note => position >= note.Position.Value * beatDuration && position < (note.Position.Value + note.Duration.Value) * beatDuration)
            .Sum(note => VolumeCalculator() * note.Pitches.Sum(pitch => WaveformCalculator(timePoint, pitch.Frequency)));
    }
}

