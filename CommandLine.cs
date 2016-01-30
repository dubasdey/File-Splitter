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

        public String getParamsOfKeyAsString(string key) {
            StringBuilder builder = new StringBuilder();
            foreach (string s in parameters[key]) {
                builder.Append(s).Append(" ");
            }
            return builder.ToString().TrimEnd();
        }

        public void printUsageHelp() {
            Console.WriteLine("Usage:");
            Console.WriteLine("fsplit -split <size> <unit> <filePath> [-d] [-f <format>] [-df <folder>] [-lf <file>]");
            Console.WriteLine();
            Console.WriteLine("Parameter help:");
            Console.WriteLine();
            Console.WriteLine("-split");
            Console.WriteLine("  size        Size of parts");
            Console.WriteLine("                If size unit is 'l' defines number of lines");
            Console.WriteLine("                in other case the size in the selected unit");
            Console.WriteLine();
            Console.WriteLine("  unit        unit of size 'b' 'kb 'mb' 'gb' 'l'");
            Console.WriteLine("                b  - bytes");
            Console.WriteLine("                kb - Kilobytes");
            Console.WriteLine("                mb - megabytes");
            Console.WriteLine("                gb - gigabytes");
            Console.WriteLine("                l  - lines (based on endline detection)");
            Console.WriteLine();
            Console.WriteLine("  filePath    Path of the file to be split.");
            Console.WriteLine();
            Console.WriteLine(" -d           Delete the original file if the process is done.");
            Console.WriteLine();
            Console.WriteLine(" -df <folder>");
            Console.WriteLine("              Set destination folder for files.");
            Console.WriteLine(" -lf <file>");
            Console.WriteLine("              Set a file to store generated names.");
            Console.WriteLine();
            Console.WriteLine(" -f <format>  Use a custom format using pattern replacements");
            Console.WriteLine("               {0} the current part");
            Console.WriteLine("               {1} number of parts");
            Console.WriteLine();
        }
    }
}
