using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using CommandLine;

namespace Changeling.Modules
{
    [Verb("extract", HelpText = "Extract embedded resources from a .NET binary.")]
    public class ExtractCommandOptions : BaseCommandOptions
    {
        [Option('r', "resource", Required = false, HelpText = "The name of the resource to extract.")]
        public string ResourceName { get; set; }
    }

    public static partial class CommandCollection
    {
        public static int ExtractCommand(ExtractCommandOptions options)
        {
            AssemblyDefinition assemblyDef = AssemblyDefinition.ReadAssembly(options.Assembly);

            if (options.Verbose)
            {
                Console.Error.WriteLine("Loaded assembly " + assemblyDef.Name);
            }

            Collection<Resource> resources = assemblyDef.MainModule.Resources;

            if (options.ResourceName != null)
            {
                EmbeddedResource selectedResource = ((EmbeddedResource)resources.FirstOrDefault(x => x.Name.EndsWith(options.ResourceName)));
                if (selectedResource != null)
                {
                    WriteResourceToFile(selectedResource, $"{selectedResource.Name}.template");
                }
            }
            else
            {
                foreach (EmbeddedResource resource in resources)
                {
                    WriteResourceToFile(resource, $"{resource.Name}.template");
                }
            }

            return 0;
        }

        private static void WriteResourceToFile(EmbeddedResource resource, string filename)
        {
            using (Stream stream = resource.GetResourceStream())
            using (FileStream fileStream = File.Create(filename))
            {

                stream.CopyTo(fileStream);
            }
        }
    }
}
