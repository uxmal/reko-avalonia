using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Reko.UserInterfaces.Avalonia.Views.Tools
{
    public class ProjectBrowserView : UserControl
    {
        public ProjectBrowserView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
