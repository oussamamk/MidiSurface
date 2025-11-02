using System.ComponentModel;
using System.Windows.Controls;
using MidiSurface.ViewModels;

namespace MidiSurface.Controls
{
    public partial class ChannelStripControl : UserControl
    {
        public ChannelStripControl()
        {
            InitializeComponent();

#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                DataContext = new ChannelStripViewModel(0, 0);
            }
#endif
        }
    }
}

