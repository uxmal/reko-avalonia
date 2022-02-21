using Reko.Gui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class MenuSystem
    {
        private static Guid Guid = Guid.NewGuid();

        public MenuSystem(ICommandTarget target)
        {
            var mc = new CommandTargetAdapter(target);

            CommandItem RekoItem(string name, int cmdId) =>
                new CommandItem { Text = name, Command = mc, CommandID = new CommandID(Gui.CmdSets.GuidReko, cmdId) };
            CommandItem ProtoItem(string name, CommandID cmdId) =>
                new CommandItem { Text = name, Command = mc, CommandID = cmdId };

            this.MainMenu = new ObservableCollection<CommandItem>
            {
                new CommandItem { Text="_File", MenuItems =
                    {
                    RekoItem("_Open...", Gui.CmdIds.FileOpen),
                    ProtoItem("_New Layout", CommandIDs.FileNewLayout),
                    new CommandItem { Text="-" },
                    RekoItem("E_xit", Gui.CmdIds.FileExit)
                    }
                },
                new CommandItem { Text="_Window", MenuItems =
                    {
                    ProtoItem("_Exit Windows", CommandIDs.ExitWindows),
                    new CommandItem { Text="-" },
                    ProtoItem("_Show Windows", CommandIDs.ShowWindows),
                    }
                },
                new CommandItem { Text="_Options", MenuItems=
                    {

                    }
                }
            };

            /*
      <MenuItem Header="_Options">
        <MenuItem x:Name="OptionsIsDragEnabled" Header="Enable Drag">
          <MenuItem.Icon>
            <CheckBox IsChecked="{Binding $parent[Window].(id:DockProperties.IsDragEnabled)}"
                      BorderThickness="0"
                      IsHitTestVisible="False"
                      x:CompileBindings="False" />
          </MenuItem.Icon>
        </MenuItem>
        <Separator />
        <MenuItem x:Name="OptionsIsDropEnabled" Header="Enable Drop">
          <MenuItem.Icon>
            <CheckBox IsChecked="{Binding $parent[Window].(id:DockProperties.IsDropEnabled)}"
                      BorderThickness="0"
                      IsHitTestVisible="False"
                      x:CompileBindings="False" />
          </MenuItem.Icon>
        </MenuItem>
      </MenuItem>
    </Menu>
            */
        }

        public void Execute(object? parameter)
        {
            if (parameter is not null && this.CommandTarget is not null)
            {
                this.CommandTarget.Execute((CommandID?)parameter);
                //RefreshMenus();
            }
        }

        public ObservableCollection<CommandItem> MainMenu { get; }

        public ICommandTarget? CommandTarget { get; set; }
    }
}
