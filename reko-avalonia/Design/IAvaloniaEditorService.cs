using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.Design
{
    public interface IWindowsFormsEditorService
    {
        void CloseDropDown();

        void DropDownControl(Control control);

        ValueTask<bool> ShowDialog(Window dialog);
    }
}
