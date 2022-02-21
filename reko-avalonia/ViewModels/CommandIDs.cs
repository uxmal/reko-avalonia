using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public static class CommandIDs
    {
        public static Guid Guid = new Guid("{AECB98E0-011C-49BB-9AC3-E5FB4782B3CC}");

        public static CommandID FileNewLayout = new CommandID(Guid, 40_000);
        public static CommandID ExitWindows = new CommandID(Guid, 40_001);
        public static CommandID ShowWindows = new CommandID(Guid, 40_002);
    }
}
