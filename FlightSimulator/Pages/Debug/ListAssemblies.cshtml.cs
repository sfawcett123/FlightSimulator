using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace Listener.Pages.Debug
{
    /// <exclude />
    public class ListAssembliesModel : PageModel
    {
        private readonly List<string> strings= new()
        {
            "Listener",
            "Microsoft.FlightSimulator.SimConnect",
            "BoardManager",
            "MenuLibrary",
            "SimListener"
        };

        /// <exclude />
        public record AssemblyDetails
        {
            /// <exclude />
            public string? Name { get; set; }
            /// <exclude />
            public Version? Version { get; set; }
        }
        /// <exclude />
        public List<AssemblyDetails> Desired { get; private set; }

        /// <exclude />
        public ListAssembliesModel()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            List<AssemblyDetails> Assemblies = new();
            foreach (var assembly in currentDomain.GetAssemblies().ToList())
            {
                AssemblyName assemName = assembly.GetName();
                if (assemName is not null)
                {
                    Assemblies.Add(new AssemblyDetails()
                    {
                        Name = assemName.Name??"Unknown",
                        Version = assemName.Version??new Version( 0,0 ),
                    });
                }
            }

            Desired = Assemblies.Where(a => strings.Any(x => x.ToString() == a.Name)).ToList();

        }

        /// <exclude />
        public void OnGet()
        {
        }
    }
}
