using Melanchall.DryWetMidi.Core;
using System.Collections.ObjectModel;
using System.Text;

namespace MidiSurface.ViewModels
{
    public class DeviceViewModel
    {
        int DevType { get; set; }
        string Name { get; set; }
        public ObservableCollection<ChannelStripViewModel> Strips { get; } = new();
        public ObservableCollection<ButtonViewModel> CButtons { get; } = new();

        public DeviceViewModel(string name, int stripCount, int devType)
        {
            Name = name;
            DevType = devType;

            for (int i = 0; i < stripCount; i++)
            {
                Strips.Add(new ChannelStripViewModel(0, i, devType)
                {
                    ChannelName = $"{i + 1}"

                });
            }

            if (devType == 2)
            {
                for (int i = 0; i < 6; i++)
                {
                    CButtons.Add(new ButtonViewModel
                    {
                        Label = $"G{i + 1}",
                        BtnContent = $"G{i + 1}",
                        Channel = 0,
                        Note = 90 + i
                    });
                }
            }
        }

        internal void ProcessNoteOn(NoteOnEvent? noteOnEvent)
        {
            if (noteOnEvent == null)
                return;

            var button = Strips.SelectMany(s => s.Buttons)
                 .FirstOrDefault(b => b.Channel == noteOnEvent?.Channel && b.Note == noteOnEvent?.NoteNumber);

            button?.SetPressedState(noteOnEvent.Velocity);
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

            fader?.SetValue((ushort)(pitchBendEvent.PitchValue / 128));
        }

        public class SysexRecord
        {
            private byte[] data;
            public byte Index { get; set; }

            public SysexRecord(byte Index, byte[] data)
            {
                this.Index = Index;
                this.data = data;
            }
            public string ValueString
            {
                get
                {
                    if (data.Length == 0)
                        return string.Empty;

                    return Encoding.ASCII.GetString(data);
                }
            }
        }

        public static List<SysexRecord> ParseCustomSysexBuffer(byte[] buffer)
        {
            var records = new List<SysexRecord>();
            int pos = 2;

            while (pos < buffer.Length)
            {
                // Check for end marker
                if (buffer[pos] == 0xF7)
                {
                    break; // End of data
                }

                // Must have at least index + length + 1 data byte
                if (pos + 2 > buffer.Length)
                {
                    throw new ArgumentException("Buffer truncated: missing index/length");
                }

                byte index = buffer[pos];
                byte length = buffer[pos + 1];
                pos += 2;

                // Ensure enough data remains
                if (pos + length > buffer.Length)
                {
                    throw new ArgumentException($"Buffer truncated: expected {length} data bytes for index {index}");
                }

                // Read 'length' bytes
                byte[] values = new byte[length];
                Array.Copy(buffer, pos, values, 0, length);
                pos += length + 1;

                records.Add(new SysexRecord(index, values));
            }

            return records;
        }


        internal void ProcessSysEx(SysExEvent? sysExEvent)
        {
            if (sysExEvent == null)
                return;
            var d = sysExEvent.Data;
            List<SysexRecord> records = ParseCustomSysexBuffer(d);


            if (DevType == 1)
            {
                foreach (var rec in records)
                {
                    if (rec.Index < 8)
                    {
                        Strips[rec.Index].Knob.SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 8 && rec.Index < 16)
                    {
                        Strips[rec.Index - 8].Fader.SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 16 && rec.Index < 24)
                    {
                        Strips[rec.Index - 16].Buttons[0].SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 24 && rec.Index < 32)
                    {
                        Strips[rec.Index - 24].Buttons[1].SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 32 && rec.Index < 40)
                    {
                        Strips[rec.Index - 32].Buttons[2].SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 40 && rec.Index < 48)
                    {
                        Strips[rec.Index - 40].Buttons[3].SetLabel(rec.ValueString);
                    }
                }
            }
            else
            {
                foreach (var rec in records)
                {
                    if (rec.Index < 9)
                    {
                        Strips[rec.Index].Knob.SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 9 && rec.Index < 18)
                    {
                        Strips[rec.Index - 9].Fader.SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 18 && rec.Index < 27)
                    {
                        Strips[rec.Index - 18].Buttons[0].SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 27 && rec.Index < 36)
                    {
                        Strips[rec.Index - 27].Buttons[1].SetLabel(rec.ValueString);
                    }
                    else if (rec.Index >= 36 && rec.Index < 42)
                    {
                        CButtons[rec.Index - 36].SetLabel(rec.ValueString);
                    }
                }
            }
        }
    }
}