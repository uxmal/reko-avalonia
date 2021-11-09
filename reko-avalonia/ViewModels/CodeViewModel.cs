using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class CodeViewModel
    {

        public ObservableCollection<string> Procedures { get; } = new();

        public CodeViewModel()
        {
            Procedures.Add("Hello");
            Procedures.Add("Goodbye");
        }
    }
}
