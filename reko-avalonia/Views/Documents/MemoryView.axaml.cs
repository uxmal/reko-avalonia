using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Reko.UserInterfaces.Avalonia.Views.Documents
{
    public partial class MemoryView : UserControl
    {
        public MemoryView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
