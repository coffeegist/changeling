using CommandLine;

namespace Changeling.Modules
{
    [Verb("extract", HelpText = "Extract embedded resources from a .NET binary.")]
    public class BaseCommandOptions
    {
        [Option('a', "assembly", Required = true, HelpText = "The .NET assembly to view or extract resources from.")]
        public string Assembly { get; set; }

        [Option('v', "verbose", Required = false, Default = false, HelpText = "Increase the output verbosity.")]
        public bool Verbose { get; set; }
    }
}
