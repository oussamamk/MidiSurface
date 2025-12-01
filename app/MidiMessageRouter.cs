// MidiMessageRouter.cs
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using MidiSurface.ViewModels;
using System.Windows.Threading;

public class MidiMessageRouter : IDisposable
{
    private InputDevice? _inputDevice;
    private readonly DeviceViewModel _devViewModel;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public MidiMessageRouter(DeviceViewModel devViewModel)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        _devViewModel = devViewModel;
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
            else if  (e.Event is ControlChangeEvent)
            {
                _devViewModel.ProcessControlChange(e.Event as ControlChangeEvent);
            }
            else if (e.Event is PitchBendEvent)
            {
                _devViewModel.ProcessPitchBend(e.Event as PitchBendEvent);
            }
            else if (e.Event is SysExEvent)
            {
                _devViewModel.ProcessSysEx(e.Event as SysExEvent);
            }
        }); 
    }

    private void HandelNoteOn(MidiEvent midiEvent)
    {
        _devViewModel.ProcessNoteOn(midiEvent as NoteOnEvent);
    }

    public void Dispose()
    {
        _inputDevice?.StopEventsListening();
        _inputDevice?.Dispose();
    }
}