using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class CommandItem : ReactiveObject
    {
        public string? Text
        {
            get { return text; }
            set { text = value; this.RaiseAndSetIfChanged(ref text, value, nameof(Text)); }
        }
        private string? text;

        public ICommand? Command
        {
            get { return command; }
            set { command = value; this.RaiseAndSetIfChanged(ref command, value, nameof(Command)); }
        }
        private ICommand? command;

        public CommandID? CommandID
        {
            get { return commandID; }
            set { commandID = value; this.RaiseAndSetIfChanged(ref commandID, value, nameof(CommandID)); }
        }
        private CommandID? commandID;

        public ObservableCollection<CommandItem> MenuItems { get; } = new();
    }
}
