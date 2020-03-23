using System;
using CommandLine;
using CommandLine.Text;
using Changeling.Modules;
using System.Collections.Generic;

namespace Changeling
{
    public class Changeling
    {
        static int Main(string[] args)
        {
            var parserResult = Parser.Default.ParseArguments<ExtractCommandOptions, ListCommandOptions, ReplaceCommandOptions>(args);
            return parserResult.MapResult(
                (ExtractCommandOptions opts) => CommandCollection.ExtractCommand(opts),
                (ListCommandOptions opts) => CommandCollection.ListCommand(opts),
                (ReplaceCommandOptions opts) => CommandCollection.ReplaceCommand(opts),
                errs => DisplayHelp(parserResult, errs));
        }

        static int DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            var helpText = HelpText.AutoBuild(result, h =>
            {
                h.Heading = @"
..................................-+*#@@#=*+-..................................
.*@@@#=*:-...................:=WWW@*:-...-+=@WWW=:.............................
@@-...-+=#WWWWW@=+-......-=WWW*................-+@WW+..........................
*W#....+*:.....-+=@WWWWWWW#:.......................-=W@#######@@@@@@WWWWWWWWWWW
..=W=......+#@*:................:@*...:*...-=..........:----..................#
....#W*........:=WW#:..............=+..@...=-.................:**=====*:....-@W
.....:W#...........=WW:...:==*=@#:..+-.......+@#*==+......*WW*-............#W=.
......-@#...........-@W::=+@WWWW@+=.........-#WWWWW@=:..+W@-.............+W@-..
.......-@#...........+@-:WW@#WWWWW#........+W@#WWWWWW#:-W*.............:@W:....
.........#W+..........#.-WWWWWWWWWW+.*+:*=-WWWWWWWWWW+..=-...........-@W=......
..........:@W=-.......*-..+@WWWWWW#-.+=:=-.+WWWWWW@+....*..........+@W=........
.............+@W@*-....+..............................-#.....-+#WWW#:..........
................-+=@WWW@W+.......-..::---:*..---.-:*#WW@WWWWW@=:...............
                "; //  https://www.topster.net/ascii-generator/

                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e, true);

            Console.WriteLine(helpText);
            return 1;
        }
    }
}
