namespace Noisier;

public class Track {
    public List<Fraction> Positions { get; set; } = [];
    public List<Note> Notes { get; set; } = [];
    public WaveformCalculator WaveformCalculator { get; set; } = WaveformCalculators.Sine();
    public VolumeCalculator VolumeCalculator { get; set; } = VolumeCalculators.Constant();

    public double GetAmplitude(int position, double frequency, double beatDuration) {
        var timePoint = position / frequency;
        
        return Notes
            .SelectMany(note => Positions.Select(position => new { Position = position.Value + note.Position.Value, Duration = note.Duration.Value, note.Pitches }))
            .Where(note => position >= note.Position * beatDuration && position < (note.Position + note.Duration) * beatDuration)
            .Sum(note => VolumeCalculator(note.Duration * beatDuration, position - note.Position * beatDuration) * note.Pitches.Sum(pitch => WaveformCalculator(timePoint, pitch.Frequency)));
    }
}
