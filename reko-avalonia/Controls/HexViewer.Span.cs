using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.Controls
{
    public partial class HexViewer
    {
        public class ClientSpan
        {
            public ClientSpan(Rect bounds)
            {
                this.Bounds = bounds;
            }

            public Rect Bounds { get; }

            public virtual void Render(HexViewer hexViewer, DrawingContext dc)
            {
                dc.FillRectangle(Brushes.Red, Bounds, 3);
            }
        }
    }
}
