using Reko.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class CommandTargetAdapter : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ICommandTarget target;

        public CommandTargetAdapter(ICommandTarget target)
        {
            this.target = target;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is CommandID cmdid)
            {
                target.Execute(cmdid);
            }
        }
    }
}
