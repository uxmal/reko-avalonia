using System;
using System.Collections.Generic;
using System.Text;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class ListOption
    {
        public ListOption(string Text, object Value)
        {
            this.Text = Text;
            this.Value = Value;
        }

        public string Text { get; set; }
        public object? Value { get; set; }
    }
}

