using Melanchall.DryWetMidi.Core;
using MidiSurface.ViewModels;
using System.Collections.ObjectModel;

public class MainViewModel
{
    public ObservableCollection<ChannelStripViewModel> Strips { get; } = new();

    public MainViewModel()
    {
        // Create 8 channel strips
        for (int i = 0; i < 8; i++)
        {
            Strips.Add(new ChannelStripViewModel(0, i)
            {
                ChannelName = $"CH {i + 1}"

            });
        }
    }

    internal void ProcessNoteOn(NoteOnEvent? noteOnEvent)
    {
        if (noteOnEvent == null)
            return;

        var button = Strips.SelectMany(s => s.Buttons)
             .FirstOrDefault(b => b.Channel == noteOnEvent?.Channel && b.Note == noteOnEvent?.NoteNumber);
        
        button?.SetPressedState(true);
    }

    internal void ProcessNoteOff(NoteOffEvent? noteOffEvent)
    {
        if (noteOffEvent == null)
            return;

        var button = Strips.SelectMany(s => s.Buttons)
            .FirstOrDefault(b => b.Channel == noteOffEvent?.Channel && b.Note == noteOffEvent?.NoteNumber);
        
        button?.SetPressedState(false);
    }

    internal void ProcessControlChange(ControlChangeEvent? controlChangeEvent)
    {
        if (controlChangeEvent == null)
            return;

        var knob = Strips
            .Select(s => s.Knob)
            .FirstOrDefault(k =>
                k.Channel == controlChangeEvent.Channel &&
                k.CCNumber == controlChangeEvent.ControlNumber);

        knob?.SetValue(controlChangeEvent.ControlValue);
    }

    internal void ProcessPitchBend(PitchBendEvent? pitchBendEvent)
    {
        if (pitchBendEvent == null)
            return;

        var fader = Strips
            .Select(s => s.Fader)
            .FirstOrDefault(f =>
                f.Channel == pitchBendEvent.Channel);

        fader?.SetValue((ushort)(pitchBendEvent.PitchValue/128));
    }
}