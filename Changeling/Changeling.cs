using CommandLine;
using Changeling.Modules;

namespace Changeling
{
    public class Changeling
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<ExtractCommandOptions, ListCommandOptions, ReplaceCommandOptions>(args)
                .MapResult(
                  (ExtractCommandOptions opts) => CommandCollection.ExtractCommand(opts),
                  (ListCommandOptions opts) => CommandCollection.ListCommand(opts),
                  (ReplaceCommandOptions opts) => CommandCollection.ReplaceCommand(opts),
                  errs => 1);
        }
    }
}
