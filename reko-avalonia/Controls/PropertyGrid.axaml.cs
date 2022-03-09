using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.VisualTree;
using System;
using System.Diagnostics;

namespace Reko.UserInterfaces.Avalonia.Controls
{
    public partial class PropertyGrid : UserControl
    {
        private object? objValue;
        private PropertyItem? selectedProperty;

        public PropertyGrid()
        {
            InitializeComponent();
            AddHandler(InputElement.PointerPressedEvent, (s, e) => Bob_PointerPressed(s, e), handledEventsToo:true);
        }

        private void Bob_PointerPressed(object? s, PointerPressedEventArgs e)
        {
            if (e.Source is TextBlock text && text.Classes.Contains("PropGridCell")
                && text.DataContext is PropertyItem item
                && this.DataContext is PropertyGridViewModel vm)
            {
                vm.SelectedPropertyItem = item;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static DirectProperty<PropertyGrid, object?> ObjectProperty =
            AvaloniaProperty.RegisterDirect<PropertyGrid, object?>(
                nameof(Object),
                o => o.Object,
                (o, v) => o.Object  = v);


        public object? Object
        {
            get { return this.objValue; }
            set
            {
                if (this.SetAndRaise(ObjectProperty, ref this.objValue, value))
                {
                    this.DataContext = new PropertyGridViewModel(value);
                }
            }
        }

        public void propName_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender is null)
                return;
            var border = (Border)sender;
            var propitem = (PropertyItem?)border.DataContext;
            SelectProperty(propitem);
            var q = border.Classes.ToString();
            Debug.Print("Border:classes: {0}", string.Join(",", border.Classes));
        }

        private void SelectProperty(PropertyItem? propitem)
        {
            if (propitem is not null && propitem != selectedProperty)
            {
                if (this.selectedProperty is not null)
                {
                    this.selectedProperty.Selected = false;
                }
                selectedProperty = propitem;
                propitem.Selected = true;
            }
        }

        public void propValue_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _ = this;
        }
    }
}
