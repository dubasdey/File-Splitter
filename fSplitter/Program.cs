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
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace FileSplitter {
    static class Program {
		
		
		
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static String lastFile = "";

        private static void printHelp() {
            Console.WriteLine("Usage:");
            Console.WriteLine("fsplit -split <size> <unit> <filePath>");
            Console.WriteLine("\t\t size\t\t Size of parts");
            Console.WriteLine("\t\t unit\t\t unit of size 'b' 'kb 'mb' 'gb'");
            Console.WriteLine("\t\t filePath\t Path of the file to be split");
        }


        private static void setConsoleWindowVisibility(bool visible) {
            IntPtr hWnd = FindWindow(null, Console.Title);
            if (hWnd != IntPtr.Zero) {
                if (!visible) {
                    //Hide the window                    
                    ShowWindow(hWnd, 0); // 0 = SW_HIDE                
                } else{
                    //Show window again                    
                    ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA       
                }
            }
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
            Console.Title = Application.ProductName +  " " + Application.ProductVersion + " Console Window";

             if (args != null && args.Length > 1) {

                if (args[0].Equals("-split")) {
                    if (args.Length < 4) {
                        Console.WriteLine("Missing parameter");                      
                        printHelp();
                        Environment.Exit(1);  // return an ErrorLevel in case it is processed in a Batch file
                    } else {

                       
                        // check size
                        Int32 size = 0;
                        try {
                            size = Convert.ToInt32(args[1]);
                        } catch {
                            Console.WriteLine("Invalid size");
                            printHelp();
                            Environment.Exit(1);
                        }

                        // check units
                        
                        if (args[2].ToLower() == "b"){
                            // nothing to do
                        } else if (args[2].ToLower() == "kb") {
                            size = size * 1024;
                        } else if (args[2].ToLower() == "mb") {
                            size = size * 1024 * 1024;
                        }else if (args[2].ToLower() == "gb"){
                            size = size * 1024 * 1024 * 1024;
                        } else {
                            Console.WriteLine("Invalid size unit");
                            printHelp();
                            Environment.Exit(1);
                        }



                        // check file exists
                        String fileName = args[3];
                        if (File.Exists(fileName)) {
                            FileSplitter fs = new FileSplitter();
                            fs.start += new FileSplitter.StartHandler(fs_splitStart);
                            fs.finish += new FileSplitter.FinishHandler(fs_splitEnd);
                            fs.processing += new FileSplitter.ProcessHandler(fs_splitProcess);
                            fs.message += new FileSplitter.MessageHandler(fs_message);
                            fs.FileName = fileName;
                            fs.PartSize = size;

                            fs.doSplit();
                            Environment.Exit(1);       // return an ErrorLevel indicating successful launch
                        } else {
                            Console.WriteLine("File does not exist");
                            printHelp();
                            Environment.Exit(1);
                        }
                    }

                } else {
                    Console.WriteLine("Unrecognized Command");
                    printHelp();
                    Environment.Exit(1);
                }

            } else {
                setConsoleWindowVisibility(false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmSplitter());
                Environment.Exit(1);     // although there's not much point - the console window is no longer visible.  Does it need to be closed?
            }
        }


        static void fs_message(object server, MessageArgs args){
            Console.WriteLine(args.Type.ToString() + "\t"+ args.Message);
        }

        static void fs_splitStart(){
            Console.WriteLine("Starting splitting operation");
        }
        
        static void fs_splitProcess(object sender, ProcessingArgs args) {
            if (lastFile != args.FileName) {
                lastFile = args.FileName;
                Console.WriteLine("Writing " + lastFile);
            } 
            
        }

        static void fs_splitEnd() {
            Console.WriteLine("Done!");
        }

     }
}

