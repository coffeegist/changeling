using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using CommandLine;

namespace Changeling.Modules
{

    [Verb("list", HelpText = "List embedded resources in a .NET binary.")]
    public class ListCommandOptions : BaseCommandOptions { }

    public static partial class CommandCollection
    {

        public static int ListCommand(ListCommandOptions options)
        {
            AssemblyDefinition assemblyDef = AssemblyDefinition.ReadAssembly(options.Assembly);

            if (options.Verbose)
            {
                Console.Error.WriteLine("Loaded assembly " + assemblyDef);
            }

            Collection<Resource> resources = assemblyDef.MainModule.Resources;

            Console.WriteLine("\nResource Name");
            Console.WriteLine("-------------");

            foreach (EmbeddedResource resource in resources)
            {
                Console.WriteLine($"{resource.Name}");
            }

            Console.WriteLine();

            return 0;
        }
    }
}
