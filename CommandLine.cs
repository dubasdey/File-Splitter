using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
