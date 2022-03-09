using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;
using Reko.UserInterfaces.Avalonia.ViewModels.Docks;
using Reko.UserInterfaces.Avalonia.ViewModels.Documents;
using Reko.UserInterfaces.Avalonia.ViewModels.Tools;
using Reko.UserInterfaces.Avalonia.ViewModels.Views;
using System;
using System.Collections.Generic;

namespace Reko.UserInterfaces.Avalonia.ViewModels
{
    /// <summary>
    /// This class is responsible for the layout of the dockable windows and documents
    /// in the shell.
    /// </summary>
    public class DockFactory : Factory
    {
        private readonly object _context;
        private IRootDock? _rootDock;

        public DockFactory(object context)
        {
            _context = context;
        }

        public override IDocumentDock CreateDocumentDock() => new CustomDocumentDock();

        public IDocumentDock? DocumentDock { get; private set; }

        public override IRootDock CreateLayout()
        {
            var document1 = new MemoryViewModel {Id = "Document1", Title = "Memory View"};
            var document2 = new DocumentViewModel {Id = "Document2", Title = "Document2"};
            var document3 = new DocumentViewModel {Id = "Document3", Title = "Document3", CanClose = true};
            
            var toolProjectBrowser = new ProjectBrowserViewModel {Id = "Tool1", Title = "Project browser"};
            var toolProcedureList = new ProcedureListViewModel {Id = "Tool2", Title = "Procedures"};

            var toolDiagnostics = new DiagnosticsViewModel {Id = "Tool3", Title = "Diagnostics"};
            var toolFindResults = new FindResultsViewModel {Id = "Tool4", Title = "Find Results"};
            var toolCallHierarchy = new CallHierarchyViewModel {Id = "Tool5", Title = "Call Hierarchy"};
            var toolConsole = new ConsoleViewModel {Id = "Tool7", Title = "Console", CanClose = false, CanPin = false};
            var toolOutput = new OutputViewModel { Id = "Tool6", Title = "Output" };

            var toolVisualizer = new VisualizerViewModel { Id = "Tool6", Title = "Visualizer", CanClose = true, CanPin = false};
            
            var toolProperties = new PropertiesViewModel {Id = "Tool8", Title = "Tool8", CanClose = false, CanPin = true};

            var leftDock = new ProportionalDock
            {
                Proportion = 0.25,
                Orientation = Orientation.Vertical,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    new ToolDock
                    {
                        ActiveDockable = toolProjectBrowser,
                        VisibleDockables = CreateList<IDockable>(toolProjectBrowser, toolProcedureList),
                        Alignment = Alignment.Left
                    }
                )
            };

            var bottomDock = new ProportionalDock
            {
                Proportion = 0.25,
                Orientation = Orientation.Horizontal,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    new ToolDock
                    {
                        ActiveDockable = toolDiagnostics,
                        VisibleDockables = CreateList<IDockable>(
                            toolDiagnostics, 
                            toolFindResults,
                            toolCallHierarchy,
                            toolConsole,
                            toolOutput),
                        Alignment = Alignment.Left
                    }
                )
            };

            var rightDock = new ProportionalDock
            {
                Proportion = 0.25,
                Orientation = Orientation.Vertical,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    new ToolDock
                    {
                        ActiveDockable = toolVisualizer,
                        VisibleDockables = CreateList<IDockable>(toolVisualizer),
                        Alignment = Alignment.Right,
                        GripMode = GripMode.Hidden
                    },
                    new ProportionalDockSplitter(),
                    new ToolDock
                    {
                        ActiveDockable = toolProperties,
                        VisibleDockables = CreateList<IDockable>(toolProperties),
                        Alignment = Alignment.Right,
                        GripMode = GripMode.AutoHide
                    }
                )
            };

            var documentDock = new CustomDocumentDock
            {
                IsCollapsable = false,
                ActiveDockable = document1,
                VisibleDockables = CreateList<IDockable>(document1, document2, document3),
                CanCreateDocument = true
            };

            var mainLayout = new ProportionalDock
            {
                Orientation = Orientation.Horizontal,
                VisibleDockables = CreateList<IDockable>
                (
                    leftDock,
                    new ProportionalDockSplitter(),
                    new ProportionalDock
                    {
                        Orientation = Orientation.Vertical,
                        VisibleDockables = CreateList<IDockable>(
                            documentDock,
                            new ProportionalDockSplitter(),
                            bottomDock
                        )
                    },
                    new ProportionalDockSplitter(),
                    rightDock
                )
            };

            var homeView = new HomeViewModel
            {
                Id = "Home",
                Title = "Home",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            var rootDock = CreateRootDock();

            rootDock.IsCollapsable = false;
            rootDock.DefaultDockable = homeView;
            rootDock.VisibleDockables = CreateList<IDockable>(homeView);

            DocumentDock = documentDock;
            _rootDock = rootDock;
            
            return rootDock;
        }

        public override void InitLayout(IDockable layout)
        {
            ContextLocator = new Dictionary<string, Func<object>>
            {
                //["Document1"] = () => new DemoDocument(),
                //["Document2"] = () => new DemoDocument(),
                //["Document3"] = () => new DemoDocument(),
                //["Tool1"] = () => new Tool1(),
                //["Tool2"] = () => new Tool2(),
                //["Tool3"] = () => new Tool3(),
                //["Tool4"] = () => new Tool4(),
                //["Tool5"] = () => new Tool5(),
                //["Tool6"] = () => new Tool6(),
                //["Tool7"] = () => new Tool7(),
                //["Tool8"] = () => new Tool8(),
                ["Home"] = () => _context
            };

            DockableLocator = new Dictionary<string, Func<IDockable?>>()
            {
                ["Root"] = () => _rootDock,
                ["Documents"] = () => DocumentDock
            };

            HostWindowLocator = new Dictionary<string, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () => new HostWindow()
            };

            base.InitLayout(layout);
        }
    }
}
