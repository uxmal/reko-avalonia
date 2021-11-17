using Reko.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control = global::Avalonia.Controls.Control;
using ReactiveUI;
using Avalonia;
using Avalonia.Media;
using Avalonia.Controls;

namespace Reko.UserInterfaces.Avalonia.Controls
{
    public partial class HexViewer : Control
    {
        public static StyledProperty<IBrush?> BackgroundProperty =
            Border.BackgroundProperty.AddOwner<HexViewer>();

        public static StyledProperty<IBrush?> ForegroundProperty =
            TextBlock.ForegroundProperty.AddOwner<HexViewer>();

        public static DirectProperty<HexViewer, MemoryArea?> MemoryAreaProperty =
            AvaloniaProperty.RegisterDirect<HexViewer, MemoryArea?>(
                nameof(MemoryArea),
                o => o.MemoryArea,
                (o, v) => o.MemoryArea = v);

        private MemoryArea? mem;
        private List<ClientSpan> spans;

        public HexViewer()
        {
            this.spans = new List<ClientSpan>();
            this.InitializeComponent();
            spans.Add(new ClientSpan(new Rect(10, 10, 400, 300)));
        }

        private void InitializeComponent()
        {
            this.VisualChildren.Clear();
        }

        public MemoryArea? MemoryArea
        {
            get { return this.mem; }
            set { 
                this.mem = value; 
                this.SetAndRaise(MemoryAreaProperty, ref this.mem, value);
            }
        }

        public IBrush? Background
        {
            get { return GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public IBrush? Foreground
        {
            get { return GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public override void Render(DrawingContext context)
        {
            var bounds = Bounds;
            var client = new Rect(0, 0, bounds.Width, bounds.Height);
            var bg = Background;
            if (bg is not null)
            {
                context.FillRectangle(bg, client);
            }
            foreach (var span in spans)
            {
                span.Render(this, context);
            }
        }
    }
}
