using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dock.Model.ReactiveUI.Controls;
using Reko.Core;
using Reko.Core.Configuration;

namespace Reko.UserInterfaces.Avalonia.ViewModels.Documents
{
    internal class ArchitecturePropertiesViewModel : Document
    {
        public ArchitecturePropertiesViewModel(IServiceProvider services)
        {
            var cfgSvc = services.RequireService<IConfigurationService>();
            Architectures = cfgSvc.GetArchitectures().ToList();
            SelectedArchitecture = Architectures.Find(x => x.Name == "ppc-be-32")!;
            Options = new FakeOptions();
        }

        public List<ArchitectureDefinition> Architectures { get; set; }

        public ArchitectureDefinition SelectedArchitecture { get; set; }

        public FakeOptions Options { get; set; }
    }

    public class FakeOptions
    {
        public int? Endianness { get; set; } = 1;
        public string? WordSize { get; set; } = "32";
    }
}
