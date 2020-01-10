using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using CommandLine;

namespace Changeling.Modules
{
    [Verb("replace", HelpText = "Extract embedded resources from a .NET binary.")]
    public class ReplaceCommandOptions : BaseCommandOptions
    {
        [Option('r', "resource", Required = true, HelpText = "The name of the resource to replace.")]
        public string ResourceName { get; set; }

        [Option('f', "file", Required = true, HelpText = "File containing the new resource data.")]
        public string Filename { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to new binary to write output to.")]
        public string OutputFile { get; set; }
    }

    public static partial class CommandCollection
    {
        public static int ReplaceCommand(ReplaceCommandOptions options)
        {
            AssemblyDefinition assemblyDef = AssemblyDefinition.ReadAssembly(options.Assembly);

            if (options.Verbose)
            {
                Console.Error.WriteLine("Loaded assembly " + assemblyDef.Name);
            }

            Collection<Resource> resources = assemblyDef.MainModule.Resources;

            EmbeddedResource selectedResource = ((EmbeddedResource)resources.FirstOrDefault(x => x.Name.EndsWith(options.ResourceName)));
            if (selectedResource != null)
            {
                EmbeddedResource newResource = new EmbeddedResource(selectedResource.Name, selectedResource.Attributes, File.ReadAllBytes(options.Filename));
                assemblyDef.MainModule.Resources.Remove(selectedResource);
                assemblyDef.MainModule.Resources.Add(newResource);
                assemblyDef.Write(options.OutputFile);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        private static void WriteResourceToAssembly(EmbeddedResource resource, string filename)
        {
            using (Stream stream = resource.GetResourceStream())
            using (FileStream fileStream = File.Create(filename))
            {

                stream.CopyTo(fileStream);
            }
        }
    }
}
