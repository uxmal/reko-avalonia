using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Reko.UserInterfaces.Avalonia.Views.Documents
{
    public partial class ArchitecturePropertiesView : UserControl
    {
        public ArchitecturePropertiesView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
