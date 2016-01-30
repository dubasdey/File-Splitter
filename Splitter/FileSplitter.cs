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
using System.IO;
using System.Text;

namespace FileSplitter {
   

    /// <summary>
    /// File Splitter class.
    /// This class does all the calculations and the splitting operations
    /// </summary>
    internal class FileSplitter {

        /// <summary>
        /// 
        /// Default buffer size
        /// </summary>
        private const Int32 BUFFER_SIZE = 1024 * 4;

        /// <summary>
        /// Buffer size for big files
        /// </summary>
        private const Int32 BUFFER_SIZE_BIG = 1024 * 1024 * 10;

        /// <summary>
        /// 1 GB constant
        /// </summary>
        private const long GIGABYTE = 1073741824L;

        /// <summary>
        /// 1 MB constant
        /// </summary>
        private const long MEGABYTE = 1048576L;
        /// <summary>
        /// Delegate for Split start
        /// </summary>
        public delegate void StartHandler();

        /// <summary>
        /// Delegate for Split end
        /// </summary>
        public delegate void FinishHandler();

        /// <summary>
        /// Delegate for Split process
        /// </summary>
        /// <param name="sender">splitter</param>
        /// <param name="args">process parameters</param>
        public delegate void ProcessHandler(Object sender,ProcessingArgs args);

        /// <summary>
        /// Delegate for Split messages
        /// </summary>
        /// <param name="server"></param>
        /// <param name="args"></param>
        public delegate void MessageHandler(Object server, MessageArgs args);

        /// <summary>
        /// Spliter Start event
        /// </summary>
        public event StartHandler start;

        /// <summary>
        /// Splitern End event
        /// </summary>
        public event FinishHandler finish;

        /// <summary>
        /// Splitter process event
        /// </summary>
        public event ProcessHandler processing;

        /// <summary>
        /// Splitter messages event
        /// </summary>
        public event MessageHandler message;

        /// <summary>
        /// Getter for the part size in bytes
        /// </summary>
        public Int64 PartSize { get ; set ; }
        
        /// <summary>
        /// Filename to be split
        /// </summary>
        public String FileName { get ; set ; }

        /// <summary>
        /// Operation Mode
        /// </summary>
        public OPERATION_SPIT OperationMode { get; set; }

        /// <summary>
        /// Calculates number of parts, based on size of file a part size
        /// </summary>
        // modified to return long
        // relies on other code to ensure file name exists
        public Int32 Parts {
            get {
                Int32 parts = 0;
                if (OperationMode != OPERATION_SPIT.BY_LINES) { 
                    if (this.FileName != null && this.FileName.Length > 0 && File.Exists(this.FileName)) {
                        FileInfo fi = new FileInfo(this.FileName);
                        if (fi.Length > this.PartSize) {
                            parts = (Int32)Math.Ceiling((double)fi.Length / this.PartSize);
                        } else {
                            parts = 1;
                        }
                    }
                }
                return parts;
            }
        }



        /// <summary>
        /// File pattern. If different to default pattern
        /// {0} for current file number
        /// {1} for total files
        /// </summary>
        public String FileFormatPattern { get; set; }

        /// <summary>
        /// Delete original file if end is correct
        /// </summary>
        public Boolean DeleteOriginalFile { get; set; }

        /// <summary>
        /// Destination folder. If different to current folder
        /// </summary>
        public String DestinationFolder { get; set; }

        /// <summary>
        /// File used to store generated file names
        /// </summary>
        public String GenerationLogFile { get; set; }

        /// <summary>
        /// Adds the file name to the log file
        /// </summary>
        /// <param name="fileName"></param>
        private void registerCreatedFile(String fileName) {
            if (GenerationLogFile != null) {
                File.AppendAllText(GenerationLogFile, fileName + Environment.NewLine);
            }
        }

        /// <summary>
        /// Launch splitStart event
        /// </summary>
        private void onStart() {
            if (start != null) {
                start();
            }
        }

        /// <summary>
        /// Launch splitEnd event
        /// </summary>
        private void onFinish() {
            if (finish != null) {
                finish();
            }
        }

        /// <summary>
        /// Launch splitProcess event
        /// </summary>
        /// <param name="filename">actual processing filename</param>
        /// <param name="part">actual part</param>
        /// <param name="partSizeWritten">bytes written in this part</param>
        /// <param name="totalParts">total parts</param>
        /// <param name="partSize">part size</param>
        private void onProcessing(String filename, Int64 part, Int64 partSizeWritten, Int64 totalParts, Int64 partSize) {
            if (processing != null) {
                processing(this, new ProcessingArgs(filename, part, partSizeWritten, totalParts, partSize));
            }
        }

        /// <summary>
        /// Launch Split Message event
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        private void onMessage(MESSAGE msg, params Object[] parameters) {
            if (message != null) {
                message(this, new MessageArgs(msg,parameters));
            }
        }

        /// <summary>
        /// Generates and registers next file name in the correct destination folder
        /// </summary>
        /// <param name="actualFileNumber"></param>
        /// <returns></returns>
        private String getNextFileName(Int64 actualFileNumber) {
            String actualFileName = String.Format(FileFormatPattern, actualFileNumber, this.Parts);
            registerCreatedFile(actualFileName);
            if (DestinationFolder != null) {
                actualFileName = Path.Combine(DestinationFolder, actualFileName);
            }
            return actualFileName;
        }

        /// <summary>
        /// Split file by number of lines
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="fileNameInfo"></param>
        /// <param name="sourceFileSize"></param>
        private void splitByLines(String inputFileName,Int64 sourceFileSize) {

            // Prepare file buffer
            byte[] buffer = new byte[BUFFER_SIZE_BIG];

            // File Pattern
            Int64 actualFileNumber = 1;
            String actualFileName = getNextFileName(actualFileNumber);

            // Error if cant create new file
            StreamReader inputReader = new StreamReader(inputFileName,true);
            Encoding enc =  inputReader.CurrentEncoding;
            StreamWriter outputWriter = new StreamWriter(actualFileName,false, enc, BUFFER_SIZE_BIG);

            Int32 linesReaded = 0;
            String line = "";
            do {
                line = inputReader.ReadLine();
                if (line != null) {
                    linesReaded++;
                    outputWriter.WriteLine(line);
                    if (linesReaded >= this.PartSize) {
                        linesReaded = 0;
                        outputWriter.Flush();
                        outputWriter.Close();
                        actualFileNumber++;
                        actualFileName = getNextFileName(actualFileNumber);
                        outputWriter = new StreamWriter(actualFileName, false, enc, BUFFER_SIZE_BIG);
                    }
                }
                onProcessing(actualFileName, actualFileNumber, linesReaded, 0, this.PartSize);
            } while (line != null);      
            outputWriter.Flush();
            outputWriter.Close();
            inputReader.Close();
        }




        /// <summary>
        /// Split by size
        /// </summary>
        private void splitBySize(String inputFileName, Int64 sourceFileSize) {

            // Minimun Part Size allowed 4kb
            if (this.PartSize < 1024) {
                onMessage(MESSAGE.ERROR_MINIMUN_PART_SIZE,1024);
                throw new SplitFailedException();
            }

            // Prepare file buffer
            int buffeSize = BUFFER_SIZE_BIG;
            if (buffeSize > this.PartSize) {
                buffeSize = Convert.ToInt32(this.PartSize);
            }
            byte[] buffer = new byte[buffeSize];
            Int64 bytesInTotal = 0;

            // File Pattern
            Int64 actualFileNumber = 1;
            String actualFileName = getNextFileName(actualFileNumber);

            // Check if file can be opened for read
            FileStream stmOriginal = null;
            FileStream stmWriter = null;
            try {
                stmOriginal = File.OpenRead(this.FileName);
            } catch {
                onMessage(MESSAGE.ERROR_OPENING_FILE);
                throw new SplitFailedException();
            }

            // Error if cant create new file
            try {
                stmWriter = File.Create(actualFileName);
            } catch {
                onMessage(MESSAGE.ERROR_OPENING_FILE); //TODO new error message 
                throw new SplitFailedException();
            }

            Int64 parts = this.Parts;
            Int64 bytesInPart = 0;
            Int32 bytesInBuffer = 1;
            while (bytesInBuffer > 0) {    // keep going while there is unprocessed data left in the input buffer

                // Read the file to current file pointer to fill buffer from 0 to total length
                bytesInBuffer = stmOriginal.Read(buffer, 0, buffer.Length);

                // If contains data process the buffer readed
                if (bytesInBuffer > 0){

                    // The entire block can be written into the same file
                    if ((bytesInPart + bytesInBuffer) <= this.PartSize){
                        stmWriter.Write(buffer, 0, bytesInBuffer);
                        bytesInPart += bytesInBuffer;
                        // Finish the current file and start a new file if required
                    } else {

                        // Fill the current file to the Full size if has pending content
                        Int32 pendingToWrite = (Int32)(this.PartSize - bytesInPart);

                        // Write the pending content to the current file
                        // If 0 The size written in last iteration is equals to block size
                        if (pendingToWrite > 0) {
                            stmWriter.Write(buffer, 0, pendingToWrite);
                        }
                        stmWriter.Flush();
                        stmWriter.Close();

                        // If the last write does not fullfill all the content, continue
                        if ((bytesInTotal + pendingToWrite) < sourceFileSize) {
                            bytesInPart = 0;

                            actualFileNumber++;
                            actualFileName = getNextFileName(actualFileNumber);
                            stmWriter = File.Create(actualFileName);

                            // Write the rest of the buffer if required into the new file
                            // if pendingToWrite is more than 0 write the part not written in previous file
                            // else write all in the new file
                            if (pendingToWrite > 0 && pendingToWrite <= bytesInBuffer) {
                                //stmWriter.Write(buffer,bytesInBuffer - pendingToWrite, bytesInBuffer);
                                stmWriter.Write(buffer, pendingToWrite, (bytesInBuffer - pendingToWrite));
                                bytesInPart += (bytesInBuffer - pendingToWrite);
                            } else if (pendingToWrite == 0) {
                                stmWriter.Write(buffer, 0, bytesInBuffer);
                                bytesInPart += bytesInBuffer;
                            }
                        }
                    }
                    bytesInTotal += bytesInBuffer;
                    onProcessing(actualFileName, actualFileNumber, bytesInPart, parts, this.PartSize);
                    // If no more data in source file close last stream
                } else {
                    stmWriter.Flush();
                    stmWriter.Close();
                }
            }

            if (bytesInTotal != sourceFileSize) {
                onMessage(MESSAGE.ERROR_TOTALSIZE_NOTEQUALS);
                throw new SplitFailedException();
            }
        }

        /// <summary>
        /// Do split operation
        /// </summary>
        public void doSplit() {
            try {
                onStart();

                FileInfo fileNameInfo = new FileInfo(this.FileName);
                
                // Check Space available
                DriveInfo driveInfo = new DriveInfo(fileNameInfo.Directory.Root.Name);
                Int64 sourceFileSize = fileNameInfo.Length;

                // Builds default pattern if FileFormatPattern is null
                if (FileFormatPattern == null) {
                    String zeros = new String('0',  this.Parts); // Padding
                    FileFormatPattern = Path.GetFileNameWithoutExtension(this.FileName) + "_{0:" + zeros + "}({1:" + zeros + "})" + fileNameInfo.Extension;
                }
                
                // Exception if not space available
                if (driveInfo.AvailableFreeSpace <= sourceFileSize) {
                    onMessage(MESSAGE.ERROR_NO_SPACE_TO_SPLIT);
                    throw new SplitFailedException();
                }

                // Check Drive Format Limitations
                if (driveInfo.DriveFormat == "FAT16") { // 2gb
                    if (this.PartSize > 2 * GIGABYTE) {
                        onMessage(MESSAGE.ERROR_FILESYSTEM_NOTALLOW_SIZE, "FAT16", 2, "Gb");
                        throw new SplitFailedException();
                    }
                } else if (driveInfo.DriveFormat == "FAT32") {  // 4gb
                    if (this.PartSize > 4 * GIGABYTE) {
                        onMessage(MESSAGE.ERROR_FILESYSTEM_NOTALLOW_SIZE, "FAT32", 4, "Gb");
                        throw new SplitFailedException();
                    }
                } else if (driveInfo.DriveFormat == "FAT12") {  // 4gb
                    if (this.PartSize > 32 * MEGABYTE) {
                        onMessage(MESSAGE.ERROR_FILESYSTEM_NOTALLOW_SIZE, "FAT12", 32, "Mb");
                        throw new SplitFailedException();
                    }
                }

                // Try create destination
                if (DestinationFolder != null) {
                    DirectoryInfo di = new DirectoryInfo(DestinationFolder);
                    if (!di.Exists) {
                        di.Create();
                    }
                }

                if (OperationMode != OPERATION_SPIT.BY_LINES) {
                    splitBySize(this.FileName, sourceFileSize);
                } else {
                    splitByLines(this.FileName, sourceFileSize);
                }

                // If no Exception breaks copy (delete new if required)
                if (DeleteOriginalFile && !fileNameInfo.IsReadOnly) {
                    fileNameInfo.Delete();
                }
            } catch (Exception){ 
                //TODO
            } finally {
                onFinish();
            }
        }
    }
}
