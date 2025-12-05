using MidiSurface.ViewModels;
using System.ComponentModel;

namespace MidiSurface.Controls
{
    public partial class ChannelStripControl : System.Windows.Controls.UserControl
    {
        public ChannelStripControl()
        {
            InitializeComponent();

#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                DataContext = new ChannelStripViewModel(0, 0, 1);
            }
#endif
        }
    }
}

