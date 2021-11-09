using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Reko.UserInterfaces.Avalonia.ViewModels;

namespace Reko.UserInterfaces.Avalonia.Views.Tools
{
    public partial class VisualizerView : UserControl
    {
        public VisualizerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public ListOption? Visualizer
        {
            get => visualizer;
            set => this.visualizer = value;
        }
        private ListOption? visualizer;
    }
}
