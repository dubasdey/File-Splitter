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
using System.Runtime.InteropServices;
using FileSplitter.Attributes;
using FileSplitter.Enums;

namespace FileSplitter {

    /// <summary>
    /// Program start
    /// </summary>
    static class Program {
		
        /// <summary>
        /// OK Exit code (To use with command line exit codes)
        /// </summary>
		private static Int32 EXIT_CODE_OK= 0;

        /// <summary>
        /// Failed Exit code (To use with command line exit codes)
        /// </summary>
		private static Int32 EXIT_CODE_FAIL= 1;
		
        /// <summary>
        /// Call to User32.dll Search for window
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Call to user32.ddl allow to show or hide a window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Last file used
        /// </summary>
        private static String lastFile = "";

        /// <summary>
        /// Change the Visibility of a Command line console
        /// </summary>
        /// <param name="visible"></param>
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
        /// Application entry Point 
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            Console.Title = Application.ProductName +  " " + Application.ProductVersion + " Console Window";
            CommandLine cmd = new CommandLine(); 
            if (args != null && args.Length > 0) {   
                cmd.parseArguments(args);

                // Print help and exit
                if (cmd.hasKey(global::FileSplitter.CommandLine.HelpParameter) || cmd.hasKey(global::FileSplitter.CommandLine.HelpParameterAlt)) {
                    cmd.printUsageHelp();
                    Environment.Exit(EXIT_CODE_OK);
                
                // Split
                } else if (cmd.hasKey(global::FileSplitter.CommandLine.SplitParameterCmd)) {
                    List<string> splitParams = cmd.getParamsOfKey(CommandLine.SplitParameterCmd);
                    if (splitParams.Count < 3) {
                        Console.WriteLine("Missing parameter");
                        cmd.printUsageHelp();

                        // return an ErrorLevel in case it is processed in a Batch file
                        Environment.Exit(EXIT_CODE_FAIL );  
                    } else {
                        // check size
                        Int64 size = 0;
                        bool delete = false;
                        string format, destinationFolder, outLogFile;
                        SplitUnit mode = SplitUnit.Bytes;
                        string sizeParameter = splitParams[CommandLine.SizeParameterIndex];
                        string unitParameterLowered = args[CommandLine.UnitParameterIndex].ToLower();

                        // Check size
                        if (!Int64.TryParse(sizeParameter, out size)) {
                            Console.WriteLine("Invalid size");
                            cmd.printUsageHelp();
                            Environment.Exit(EXIT_CODE_FAIL);
                        }

                        mode = UnitAttribute.Parse<SplitUnit>(unitParameterLowered);

                        if (mode == SplitUnit.Incorrect) {
                            Console.WriteLine("Invalid size unit");
                            cmd.printUsageHelp();
                            Environment.Exit(EXIT_CODE_FAIL);
                        }

                        size = Utils.unitConverter(size, mode);

                        // check delete original
                        if (cmd.hasKey(CommandLine.DeleteParameterCmd)) {
                            delete = true;
                        }
                        
                        Func<string, string, string> extractKeyWhenSet = (string parameter, string errorMessage) => {
                            string result = null;
                            if (cmd.hasKey(parameter)) {
                                if (cmd.hasParams(parameter)) {
                                    result = cmd.getParamsOfKeyAsString(parameter);
                                } else {
                                    Console.WriteLine(errorMessage);
                                    cmd.printUsageHelp();
                                    Environment.Exit(EXIT_CODE_FAIL);
                                }
                            }
                            return result;
                        };

                        // Check format
                        format = extractKeyWhenSet(CommandLine.FormatParameterCmd, "Invalid format");
                        
                        // Check destination Folder
                        destinationFolder = extractKeyWhenSet(CommandLine.DestinationFolderParameterCmd, "Invalid destination");
                       
                        // Check file to save names
                        outLogFile = extractKeyWhenSet(CommandLine.LogFileParameterCmd, "Invalid file");
                        
                        // check file exists
                        String fileName = args[3];
                        if (File.Exists(fileName)) {
                            FileSplitWorker fs = new FileSplitWorker();
                            fs.start += new FileSplitWorker.StartHandler(fs_splitStart);
                            fs.finish += new FileSplitWorker.FinishHandler(fs_splitEnd);
                            fs.processing += new FileSplitWorker.ProcessHandler(fs_splitProcess);
                            fs.message += new FileSplitWorker.MessageHandler(fs_message);
                            fs.FileName = fileName;
                            fs.PartSize = size;
                            fs.OperationMode = mode;
                            fs.DeleteOriginalFile = delete;
                            fs.DestinationFolder = destinationFolder;
                            fs.GenerationLogFile = outLogFile;
                            if (format != null) {
                                fs.FileFormatPattern = format;
                            }
                            fs.doSplit();

                            Environment.Exit(EXIT_CODE_OK);       
                        } else {
                            Console.WriteLine("File does not exist");
                            cmd.printUsageHelp();
                            Environment.Exit(EXIT_CODE_FAIL);
                        }
                    }

                /* TODO JOIN */
                } else {
                    Console.WriteLine("Unrecognized Command");
                    cmd.printUsageHelp();
                    Environment.Exit(EXIT_CODE_FAIL);
                }

            } else {
                setConsoleWindowVisibility(false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmSplitter());

                Environment.Exit(EXIT_CODE_OK);
            }
        }

        /// <summary>
        /// Handler for event in FileSplitWorker, Message from process
        /// </summary>
        /// <param name="server"></param>
        /// <param name="args"></param>
        static void fs_message(object server, MessageArgs args){
            Console.WriteLine(Utils.getMessageText(args.Message, args.Parameters));
        }

        /// <summary>
        /// Handler for event in FileSplitWorker, Process start
        /// </summary>
        static void fs_splitStart(){
            Console.WriteLine("Starting splitting operation");
        }

        /// <summary>
        /// Handler for event in FileSplitWorker, Process in execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

        static void fs_splitProcess(object sender, ProcessingArgs args) {
            if (lastFile != args.FileName) {
                lastFile = args.FileName;
                Console.WriteLine("Writing " + lastFile);
            } 
        }

        /// <summary>
        /// Handler for event in FileSplitWorker, Process End
        /// </summary>
        static void fs_splitEnd() {
            Console.WriteLine("Done!");
        }
     }
}