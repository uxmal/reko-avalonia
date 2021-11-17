using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.Controls
{
    public partial class HexViewer
    {
        public class ClientSpan : StyledElement
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

        public class TextSpan : ClientSpan
        {
            public static StyledProperty<double> FontSizeProperty =
                global::Avalonia.Controls.TextBlock.FontSizeProperty.AddOwner<TextSpan>();

            public static StyledProperty<FontFamily> FontFamilyProperty =
                global::Avalonia.Controls.TextBlock.FontFamilyProperty.AddOwner<TextSpan>();


            public TextSpan(string text, Rect bounds)
                : base(bounds)
            {
                this.Text = text;
            }

            public string Text { get; set; }

            public override void Render(HexViewer hexViewer, DrawingContext dc)
            {
                dc.FillRectangle(Brushes.White, Bounds);
                var size = GetValue(FontSizeProperty);
                var font = new Typeface(GetValue(FontFamilyProperty));
                var fg = GetValue(ForegroundProperty);
                var tx = new TextLayout(this.Text, font, size, fg);
                using (dc.PushPostTransform(Matrix.CreateTranslation(Bounds.Left, Bounds.Top)))
                {
                    tx.Draw(dc);
                }
            }
        }
    }
}
