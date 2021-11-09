using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Reko.UserInterfaces.Avalonia.Views.Tools
{
    public class ConsoleView : UserControl
    {
        public ConsoleView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
