using System.Collections.Generic;
using Mono.Cecil;

namespace EOSPlugin
{
    
    public static class SteamWorksPatcher
    {
        public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll", "steam_api64.dll" };

        // Patches the assemblies
        public static void Patch(ref AssemblyDefinition assembly)
        {
            if (assembly.Name.Name != string.Empty)
            {
                System.Diagnostics.Trace.WriteLine("Patching assembly: " + assembly.Name.Name);
            }
            // Patcher code here
        }
    }
}
