using Dock.Model.ReactiveUI.Controls;
using Reko.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.ViewModels.Documents
{
    public class MemoryViewModel : Document
    {
        public MemoryViewModel()
        {
        }

        public MemoryArea? MemoryArea { get; set; }
    }
}
