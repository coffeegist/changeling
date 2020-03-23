using System;
using CommandLine;
using CommandLine.Text;
using Changeling.Modules;
using System.Collections.Generic;

namespace Changeling
{
    public class Changeling
    {
        private const string CHANGELING_BANNER = @"
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


        static int Main(string[] args)
        {
            var parserResult = new Parser(with => with.HelpWriter = null).ParseArguments<ExtractCommandOptions, ListCommandOptions, ReplaceCommandOptions>(args);
            return parserResult.MapResult(
                (ExtractCommandOptions opts) => CommandCollection.ExtractCommand(opts),
                (ListCommandOptions opts) => CommandCollection.ListCommand(opts),
                (ReplaceCommandOptions opts) => CommandCollection.ReplaceCommand(opts),
                errs => DisplayHelp(parserResult, errs));
        }

        static int DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            HelpText helpText;

            if (HelpTextExtensions.IsVersion(errs))
            {
                helpText = new HelpText();
                helpText.Heading = CHANGELING_BANNER;
                helpText.Heading += "\n" + "v1.0";
                helpText.AddDashesToOption = true;
            }
            else
            {
                helpText = HelpText.AutoBuild(result, h =>
                {
                    h.Copyright = "";
                    h.Heading = CHANGELING_BANNER;
                    h.AddDashesToOption = true;

                    return HelpText.DefaultParsingErrorsHandler(result, h);
                }, e => e, true);
            }

            Console.WriteLine(helpText);
            return 1;
        }
    }
}
