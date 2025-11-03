// MidiMessageRouter.cs
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System.Windows.Threading;

public class MidiMessageRouter : IDisposable
{
    private InputDevice? _inputDevice;
    private readonly MainViewModel _mainViewModel;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public MidiMessageRouter(MainViewModel mainViewModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _mainViewModel = mainViewModel;
    }

    public bool StartListening(string deviceName)
    {
        try
        {
            var dev = InputDevice.GetAll(); 
            _inputDevice = InputDevice.GetByName(deviceName);


            if (_inputDevice == null) return false;

            _inputDevice.EventReceived += OnMessageRecieved;
            _inputDevice.StartEventsListening();
            return true;
        }
        catch (Exception)
        {
            // Log error
            return false;
        }
    }

    private void OnMessageRecieved(object? sender, MidiEventReceivedEventArgs e)
    {
        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            if (e.Event is NoteOnEvent)
            {
                HandelNoteOn(e.Event);
            }
            else if (e.Event is NoteOffEvent)
            {
                HandelNoteOff(e.Event);
            }
            else if  (e.Event is ControlChangeEvent)
            {
                _mainViewModel.ProcessControlChange(e.Event as ControlChangeEvent);
            }
            else if (e.Event is PitchBendEvent)
            {
                _mainViewModel.ProcessPitchBend(e.Event as PitchBendEvent);
            }

        }); 
    }

    private void HandelNoteOff(MidiEvent midiEvent)
    {
        _mainViewModel.ProcessNoteOff(midiEvent as NoteOffEvent);
    }

    private void HandelNoteOn(MidiEvent midiEvent)
    {
        _mainViewModel.ProcessNoteOn(midiEvent as NoteOnEvent);
    }

    public void Dispose()
    {
        _inputDevice?.Dispose();
    }
}