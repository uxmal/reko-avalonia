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
using Reko.Core;

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

        public static DirectProperty<HexViewer, Address?> TopAddressProperty =
            AvaloniaProperty.RegisterDirect<HexViewer, Address?>(
                nameof(TopAddress),
                o => o.TopAddress,
                (o, v) => o.TopAddress = v);

        private MemoryArea? mem;
        private List<ClientSpan> spans;
        private Address? addrTop;

        public HexViewer()
        {
            this.spans = new List<ClientSpan>();
            this.InitializeComponent();
            this.ClearVisualElements();
            this.GenerateVisualElements();
        }

        private void GenerateVisualElements()
        {
            spans.Add(new ClientSpan(new Rect(10, 10, 400, 300)));
            spans.Add(new TextSpan("Hello Reko!", new Rect(20, 20, 100, 200)));
        }

        private void ClearVisualElements()
        {
            this.spans = new List<ClientSpan>();
        }

        private void InitializeComponent()
        {
        }

        public MemoryArea? MemoryArea
        {
            get { return this.mem; }
            set { if (this.SetAndRaise(MemoryAreaProperty, ref this.mem, value))
                    OnMemoryAreaChanged();
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

        public Address? TopAddress
        {
            get { return this.addrTop; }
            set { SetAndRaise(TopAddressProperty, ref this.addrTop, value); }
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

        private void OnMemoryAreaChanged()
        {
            ClearVisualElements();
            var mem = this.MemoryArea;
            if (mem is not null)
            {
                GenerateVisualElements();
                this.TopAddress = mem.BaseAddress;
            }
        }
    }
}
