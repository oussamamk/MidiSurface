// MainViewModel.cs
using System.Collections.ObjectModel;
using MidiSurface.ViewModels;

public class MainViewModel
{
    public ObservableCollection<ChannelStripViewModel> Strips { get; } = new();

    public MainViewModel()
    {
        // Create 8 channel strips
        for (int i = 0; i < 8; i++)
        {
            Strips.Add(new ChannelStripViewModel
            {
                ChannelName = $"CH {i + 1}"
            });
        }
    }
}