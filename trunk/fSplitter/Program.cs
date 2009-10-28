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
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace FileSplitter {
    static class Program {
        
        private static String lastFile = "";

        private static void printHelp() {
            Console.WriteLine("Usage:");
            Console.WriteLine("fsplit -split <size> <unit> <filePath>");
            Console.WriteLine("\t\tsize\t Size of parts");
            Console.WriteLine("\t\tunit\t unit of size 'b' 'kb 'mb'");
            Console.WriteLine("\t\filePath\t Path of file to be splitted");
        }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            // args:
            // 0 - action
            // 1 - split size
            // 2 - split units
            // 3 - file
            if (args != null && args.Length > 1) {

                if (args[0].Equals("-split")) {
                    if (args.Length < 4) {
                        Console.WriteLine("Missing parameter");
                        printHelp();

                    } else {
                        // check size
                        decimal size = 0;
                        try {
                            size = Convert.ToDecimal(args[1]);
                        } catch {
                            Console.WriteLine("Invalid size");
                            printHelp();
                            return;
                        }

                        // check units
                        Int32 units = 0;
                        if (args[2].ToLower() == "b"){
                            units = 0;
                        } else if (args[2].ToLower() == "kb") {
                            units = 1;
                        } else if (args[2].ToLower() == "mb") {
                            units = 2;
                        } else {
                            Console.WriteLine("Invalid size unit");
                            printHelp();
                            return;
                        }

                        // check file exists
                        String fileName = args[3];
                        if (File.Exists(fileName)) {
                            FileSplitter fs = new FileSplitter();
                            fs.splitStart += new FileSplitter.splitStartHandler(fs_splitStart);
                            fs.splitEnd += new FileSplitter.splitEndHandler(fs_splitEnd);
                            fs.splitProcess += new FileSplitter.splitProcessHandler(fs_splitProcess);
                            fs.FileName = fileName;
                            fs.setPartSize(size,units);
                            fs.doSplit();
                            
                        } else {
                            Console.WriteLine("File not exists");
                            printHelp();
                            return;
                        }
                    }

                } else {
                    Console.WriteLine("Unrecognized Command");
                    printHelp();
                }

            } else {
                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmSplitter());
            }
        }

        
        static void fs_splitProcess(object sender, SplitProcessArgs args) {
            if (lastFile != args.FileName) {
                lastFile = args.FileName;
                Console.WriteLine("Writting " + lastFile);
            } 
            
        }

        static void fs_splitEnd() {
            Console.WriteLine("Done!");
        }

        static void fs_splitStart() {
            Console.WriteLine("Starting splitting operation");
        }
    }
}

