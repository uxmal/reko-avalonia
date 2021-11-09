using System;
using System.Collections.Generic;
using System.Text;
using Dock.Model.ReactiveUI.Controls;
using ReactiveUI;

namespace Reko.UserInterfaces.Avalonia.ViewModels.Tools
{
    public class VisualizerViewModel : Tool
    {

        public List<ListOption> Visualizers { get; } = new List<ListOption>()
        {
            new ListOption("V1", "vis-1"),
            new ListOption("V2", "vis-2"),
            new ListOption("V3", "vis-3"),
        };

        public VisualizerViewModel()
        {
            this.selectedVisualizer = Visualizers[0];
        }


        public ListOption SelectedVisualizer {
            get => selectedVisualizer;
            set { this.RaiseAndSetIfChanged(ref selectedVisualizer, value); }
        }
        private ListOption selectedVisualizer;
    }
}
