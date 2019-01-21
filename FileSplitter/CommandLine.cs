/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Text;
using System.Collections.Generic;


namespace FileSplitter {

    /// <summary>
    /// Commandline parser
    /// </summary>
    internal class CommandLine {


        internal const int SizeParameterIndex = 0;
        internal const int UnitParameterIndex = 2;

        internal const string SplitParameterCmd = "split";
        internal const string DeleteParameterCmd = "d";
        internal const string FormatParameterCmd = "f";
        internal const string DestinationFolderParameterCmd = "df";
        internal const string LogFileParameterCmd = "lf";
        internal const string UnitBytes = "b";
        internal const string UnitKiloBytes = "kb";
        internal const string UnitMegaBytes = "mb";
        internal const string UnitGigaBytes = "gb";
        internal const string UnitLines = "l";
        internal const string UnitFiles = "f";
        internal const string HelpParameter = "?";
        internal const string HelpParameterAlt = "h";
        internal const string FileEncoding = "fe";

        /// <summary>
        /// Parameters collection
        /// </summary>
        private Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();

        /// <summary>
        /// Parse arguments
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public void parseArguments(string[] args) {
            parameters.Clear();
            string lastKey = "";
            foreach (String arg in args) {
                if (arg.StartsWith("-")) {
                    lastKey = arg.Replace("-", "");
                    if (!parameters.ContainsKey(lastKey)) {
                        parameters.Add(lastKey, new List<string>());
                    }
                } else {
                    parameters[lastKey].Add(arg);
                }
            }
        }

        /// <summary>
        /// Check for key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean hasKey(string key) {
            return parameters.ContainsKey(key);
        }

        /// <summary>
        /// Detect if key exists with parameters
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean hasParams(string key) {
            return (parameters[key].Count > 0);
        }

        /// <summary>
        /// Get params of key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> getParamsOfKey(string key) {
            return parameters[key];
        }

        /// <summary>
        /// Get all parameters of key as string with spaces
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String getParamsOfKeyAsString(string key) {
            StringBuilder builder = new StringBuilder();
            foreach (string s in parameters[key]) {
                builder.Append(s).Append(" ");
            }
            return builder.ToString().TrimEnd();
        }

        /// <summary>
        /// Prints usage help
        /// </summary>
        public void printUsageHelp() {
            Console.WriteLine("Usage:");
            Console.WriteLine($"fsplit -{SplitParameterCmd} <size> <unit> <filePath> [-{DeleteParameterCmd}] [-{FormatParameterCmd} <format>] [-{DestinationFolderParameterCmd} <folder>] [-{LogFileParameterCmd} <file>]");
            Console.WriteLine();
            Console.WriteLine("Parameter help:");
            Console.WriteLine();
            Console.WriteLine($"-{SplitParameterCmd}");
            Console.WriteLine("  size        Size of parts");
            Console.WriteLine($"                If size unit is '{UnitLines}' defines number of lines");
            Console.WriteLine("                in other case the size in the selected unit");
            Console.WriteLine();//TODO: Retrieve units from the SplitUnit Attributes
            Console.WriteLine($"  unit        unit of size '{UnitBytes}' '{UnitKiloBytes} '{UnitMegaBytes}' '{UnitGigaBytes}' '{UnitLines}'");
            Console.WriteLine($"                {UnitBytes}  - bytes");
            Console.WriteLine($"                {UnitKiloBytes} - Kilobytes");
            Console.WriteLine($"                {UnitMegaBytes} - megabytes");
            Console.WriteLine($"                {UnitGigaBytes} - gigabytes");
            Console.WriteLine($"                {UnitLines}  - lines (based on endline detection)");
            Console.WriteLine($"                {UnitFiles}  - Files (Adjust size to get the dired files)");
            Console.WriteLine();
            Console.WriteLine("  filePath    Path of the file to be split.");
            Console.WriteLine();
            Console.WriteLine($" -{DeleteParameterCmd}           Delete the original file if the process is done.");
            Console.WriteLine();
            Console.WriteLine($" -{DestinationFolderParameterCmd} <folder>");
            Console.WriteLine("              Set destination folder for files.");
            Console.WriteLine($" -{LogFileParameterCmd} <file>");
            Console.WriteLine("              Set a file to store generated names.");
            Console.WriteLine();
            Console.WriteLine($" -{FormatParameterCmd} <format>  Use a custom format using pattern replacements");
            Console.WriteLine("               {0} the current part");
            Console.WriteLine("               {1} number of parts");
            Console.WriteLine();
            Console.WriteLine($" -{FileEncoding} <enc>  Use a custom encoding for split by lines.");
            Console.WriteLine("               UTF-8-BOM to create the files as UTF-8 with byte order mark");
            Console.WriteLine("               UTF-8-NOBOM to create the files as UTF-8 without byte order mark");
            Console.WriteLine("               Other valid encoding to force the encoding for resulting files");
            Console.WriteLine("               Do not set to use input file detected encondig");
            Console.WriteLine();
        }
    }
}
