using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Input;
using Dock.Model.Controls;
using Dock.Model.Core;
using ReactiveUI;
using Reko.Core;
using Reko.Gui;
using Reko.UserInterfaces.Avalonia.Views;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    public class MainWindowViewModel :
        ReactiveObject,
        ICommandTarget
    {
        private readonly IFactory? _factory;
        private IRootDock? _layout;

        public MainWindowViewModel()
        {
            _factory = new DockFactory(new Project());

            DebugFactoryEvents(_factory);

            Layout = _factory?.CreateLayout();
            if (Layout is { })
            {
                _factory?.InitLayout(Layout);
                if (Layout is { } root)
                {
                    root.Navigate.Execute("Home");
                }
            }

            this.MenuSystem = new MenuSystem(this);
            this.ManeMenu = MenuSystem.MainMenu;

            NewLayout = ReactiveCommand.Create(ResetLayout);
            FileOpen = ReactiveCommand.Create(OpenFile);
        }

        public MenuSystem MenuSystem { get; }
        public ObservableCollection<CommandItem> ManeMenu { get; }
        public IRootDock? Layout
        {
            get => _layout;
            set => this.RaiseAndSetIfChanged(ref _layout, value);
        }

        public ICommand NewLayout { get; }
        public ICommand FileOpen { get; }
        public MainWindow? MainWindow { get; set; }

        private void DebugFactoryEvents(IFactory factory)
        {
            factory.ActiveDockableChanged += (_, args) =>
            {
                Debug.WriteLine($"[ActiveDockableChanged] Title='{args.Dockable?.Title}'");
            };

            factory.FocusedDockableChanged += (_, args) =>
            {
                Debug.WriteLine($"[FocusedDockableChanged] Title='{args.Dockable?.Title}'");
            };

            factory.DockableAdded += (_, args) =>
            {
                Debug.WriteLine($"[DockableAdded] Title='{args.Dockable?.Title}'");
            };

            factory.DockableRemoved += (_, args) =>
            {
                Debug.WriteLine($"[DockableRemoved] Title='{args.Dockable?.Title}'");
            };

            factory.DockableClosed += (_, args) =>
            {
                Debug.WriteLine($"[DockableClosed] Title='{args.Dockable?.Title}'");
            };

            factory.DockableMoved += (_, args) =>
            {
                Debug.WriteLine($"[DockableMoved] Title='{args.Dockable?.Title}'");
            };

            factory.DockableSwapped += (_, args) =>
            {
                Debug.WriteLine($"[DockableSwapped] Title='{args.Dockable?.Title}'");
            };

            factory.DockablePinned += (_, args) =>
            {
                Debug.WriteLine($"[DockablePinned] Title='{args.Dockable?.Title}'");
            };

            factory.DockableUnpinned += (_, args) =>
            {
                Debug.WriteLine($"[DockableUnpinned] Title='{args.Dockable?.Title}'");
            };

            factory.WindowOpened += (_, args) =>
            {
                Debug.WriteLine($"[WindowOpened] Title='{args.Window?.Title}'");
            };

            factory.WindowClosed += (_, args) =>
            {
                Debug.WriteLine($"[WindowClosed] Title='{args.Window?.Title}'");
            };

            factory.WindowClosing += (_, args) =>
            {
                // NOTE: Set to True to cancel window closing.
#if false
                args.Cancel = true;
#endif
                Debug.WriteLine($"[WindowClosing] Title='{args.Window?.Title}', Cancel={args.Cancel}");
            };

            factory.WindowAdded += (_, args) =>
            {
                Debug.WriteLine($"[WindowAdded] Title='{args.Window?.Title}'");
            };

            factory.WindowRemoved += (_, args) =>
            {
                Debug.WriteLine($"[WindowRemoved] Title='{args.Window?.Title}'");
            };

            factory.WindowMoveDragBegin += (_, args) =>
            {
                // NOTE: Set to True to cancel window dragging.
#if false
                args.Cancel = true;
#endif
                Debug.WriteLine($"[WindowMoveDragBegin] Title='{args.Window?.Title}', Cancel={args.Cancel}, X='{args.Window?.X}', Y='{args.Window?.Y}'");
            };

            factory.WindowMoveDrag += (_, args) =>
            {
                Debug.WriteLine($"[WindowMoveDrag] Title='{args.Window?.Title}', X='{args.Window?.X}', Y='{args.Window?.Y}");
            };

            factory.WindowMoveDragEnd += (_, args) =>
            {
                Debug.WriteLine($"[WindowMoveDragEnd] Title='{args.Window?.Title}', X='{args.Window?.X}', Y='{args.Window?.Y}");
            };
        }

        public void CloseLayout()
        {
            if (Layout is IDock dock)
            {
                if (dock.Close.CanExecute(null))
                {
                    dock.Close.Execute(null);
                }
            }
        }

        public async void OpenFile()
        {
            if (MainWindow is null)
                throw new System.InvalidOperationException($"Property {nameof(MainWindow)}) must be set.");
            var dlg = new global::Avalonia.Controls.OpenFileDialog();
            var files = await dlg.ShowAsync(MainWindow);
            var _ = files?.Length;
        }

        public void ResetLayout()
        {
            if (Layout is not null)
            {
                if (Layout.Close.CanExecute(null))
                {
                    Layout.Close.Execute(null);
                }
            }

            var layout = _factory?.CreateLayout();
            if (layout is not null)
            {
                Layout = layout;
                _factory?.InitLayout(layout);
            }
        }

        bool ICommandTarget.QueryStatus(CommandID cmdId, CommandStatus status, CommandText text)
        {
            if (cmdId.Guid == CmdSets.GuidReko)
            {
                switch (cmdId.ID)
                {
                case CmdIds.FileOpen:
                case CmdIds.FileExit:
                    status.Status = MenuStatus.Enabled | MenuStatus.Visible;
                    return true;
                }
            }
            else if (cmdId.Guid == CommandIDs.Guid)
            {
                status.Status = MenuStatus.Enabled | MenuStatus.Visible;
            }
            return false;
        }

        bool ICommandTarget.Execute(CommandID cmdId)
        {
            if (cmdId.Guid == CmdSets.GuidReko)
            {
                switch (cmdId.ID)
                {
                case CmdIds.FileOpen:
                    OpenFile();
                    return true;
                case CmdIds.FileExit:
                    MainWindow?.Close();
                    return true;
                }
            }
            return false;
        }
    }
}
