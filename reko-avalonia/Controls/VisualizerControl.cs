using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Reko.UserInterfaces.Avalonia.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reko.UserInterfaces.Avalonia.Views
{
    public class VisualizerControl : Control
    {
        public static readonly DirectProperty<VisualizerControl, object?> VisualizerProperty =
            AvaloniaProperty.RegisterDirect<VisualizerControl, object?>(
                nameof(Visualizer),
                v => v.Visualizer,
                (v, vv) => v.Visualizer = vv);

        public object? Visualizer
        {   get => visualizer;
            set => this.visualizer = value; }
        private object? visualizer;

        public override void Render(DrawingContext context)
        {
            context.FillRectangle(Brushes.Red, new Rect(0, 0, 100, 100));
            base.Render(context);
        }
    }
}
