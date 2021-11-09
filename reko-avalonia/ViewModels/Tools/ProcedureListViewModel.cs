using Dock.Model.ReactiveUI.Controls;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reko.UserInterfaces.Avalonia.ViewModels.Tools
{
    public class ProcedureListViewModel : Tool
    {
        private List<ProcedureItem> modelProcedures;

        public ProcedureListViewModel()
        {
            this.searchCriterion = "";
            this.modelProcedures =
                Enumerable.Range(1, 100_000)
                    .Select(n => n + 0x4000_0000)
                    .Select(n => new ProcedureItem($"fn{n:X8}", $"{n:X8}"))
                    .ToList();

            this.procedures = new List<ProcedureItem>(modelProcedures);
        }

        public string SearchCriterion
        {
            get { return searchCriterion; }
            set { 
                if (string.IsNullOrEmpty(searchCriterion))
                {
                    this.Procedures = new List<ProcedureItem>(this.modelProcedures);
                }
                else
                {
                    var criterion = searchCriterion.Trim();
                    this.Procedures = new List<ProcedureItem>(
                        modelProcedures.Where(p => p.Name.Contains(criterion)));
                }
                this.RaiseAndSetIfChanged(ref searchCriterion, value);
            }
        }
        private string searchCriterion;

        public List<ProcedureItem> Procedures
        {
            get { return procedures; }
            set { this.RaiseAndSetIfChanged(ref procedures, value); }
        }
        private List<ProcedureItem> procedures;

        public List<string> Dogs { get; set; } = new List<string>
        {
            "Laika",
            "Debbie",
            "Lassie",
            "Rufus"
        };

        public class ProcedureItem
        {
            public ProcedureItem(string name, string address)
            {
                this.Name = name; this.Address = address;
            }
            public string Name { get; }
            public string Address { get; }
        }
    }
}
