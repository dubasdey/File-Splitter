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
using FileSplitter.Attributes;
using FileSplitter.Exceptions;
using FileSplitter.Enums;
using System;
using System.IO;
using System.Text;

namespace FileSplitter {

    /// <summary>
    /// File Splitter class.
    /// This class does all the calculations and the splitting operations
    /// </summary>
    /// <remarks>
    /// Renamed class to prevent conflicts and prevent having to use global::
    /// </remarks>
    public class FileSplitWorker {

        #region Buffer and size constants

        /// <summary>
        /// Default buffer size
        /// </summary>
        static readonly Int32 BUFFER_SIZE = (Int32)UnitAttribute.GetFromField<SplitUnit>(SplitUnit.KiloBytes).CalculatedFactor * 4;
        
        /// <summary>
        /// Buffer size for big files
        /// </summary>
        static readonly Int32 BUFFER_SIZE_BIG = (Int32)UnitAttribute.GetFromField<SplitUnit>(SplitUnit.MegaBytes).CalculatedFactor * 10;
        
        /// <summary>
        /// 1 MB constant
        /// </summary>
        static readonly Int64 MEGABYTE = (Int64)UnitAttribute.GetFromField<SplitUnit>(SplitUnit.MegaBytes).CalculatedFactor;// 1048576L;
        
        /// <summary>
        /// 1 GB constant
        /// </summary>
        static readonly Int64 GIGABYTE = (Int64)UnitAttribute.GetFromField<SplitUnit>(SplitUnit.GigaBytes).CalculatedFactor;// 1073741824L;
        
        /// <summary>
        /// The minimum part size we allow
        /// </summary>
        static readonly Int32 MINIMUM_PART_SIZE = BUFFER_SIZE;

        #endregion

        #region File System related limits

        const string DriveFormat_FAT12 = "FAT12";
        const string DriveFormat_FAT16 = "FAT16";
        const string DriveFormat_FAT32 = "FAT32";
        const Int32 DriveFormat_FAT12_MaxAmount = 32;
        const Int32 DriveFormat_FAT16_MaxAmount = 2;
        const Int32 DriveFormat_FAT32_MaxAmount = 4;
        static readonly Int64 DriveFormat_FAT12_Factor = MEGABYTE;
        static readonly Int64 DriveFormat_FAT16_Factor = GIGABYTE;
        static readonly Int64 DriveFormat_FAT32_Factor = GIGABYTE;
        const string DriveFormat_FAT12_FactorName = "Mb";
        const string DriveFormat_FAT16_FactorName = "Gb";
        const string DriveFormat_FAT32_FactorName = "Gb";

        #endregion

        #region Delegates and events 

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
        public delegate void ProcessHandler(Object sender, ProcessingArgs args);

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

        #endregion

        #region Private variables 

        /// <summary>
        /// Stores the numbers of partes calculated if the partSize o unit is not changed
        /// to reduce the number of recalculations
        /// </summary>
        private Int32 partsCache;

        /// <summary>
        /// Size of the part (depending on the part unit)
        /// </summary>
        private Int64 partSize;

        /// <summary>
        /// Operation mode to use
        /// </summary>
        private SplitUnit operationMode;

        #endregion

        #region Properties

        /// <summary>
        /// Getter for the part size in bytes
        /// </summary>
        public Int64 PartSize {
            get {
                return partSize;
            }
            set {
                if (value != partSize) {
                    partsCache = 0;
                    partSize = value;
                }
            }
        }

        /// <summary>
        /// Filename to be split
        /// </summary>
        public String FileName { get; set; }

        /// <summary>
        /// Operation Mode
        /// </summary>
        public SplitUnit OperationMode {
            get {
                return operationMode;
            }
            set {
                if (value != operationMode) {
                    partsCache = 0;
                    operationMode = value;
                }
            }
        }

        /// <summary>
        /// Calculates number of parts, based on size of file a part size
        /// </summary>
        // modified to return long
        // relies on other code to ensure file name exists
        public Int32 Parts {
            get {
                if (partsCache < 1) {
                    if (this.FileName != null && this.FileName.Length > 0 && File.Exists(this.FileName)) {
                        double items = 0;
                        if (OperationMode != SplitUnit.Lines) {
                            FileInfo fi = new FileInfo(this.FileName);
                            if (fi.Length > this.PartSize) {
                                items = fi.Length;
                            } 
                        } else {
                            // Detect number of lines
                            items = getNumberOfLines(this.FileName);
                        }
                        partsCache = (Int32)Math.Ceiling(items / this.PartSize);
                    }
                }
                return partsCache;
            }
            set {
                partsCache = value;
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

        #endregion

        #region event helper functions 

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
        private void onMessage(ExceptionMessage msg, params Object[] parameters) {
            if (message != null) {
                message(this, new MessageArgs(msg, parameters));
            }
        }
        #endregion 

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
        /// Detects the total number of lines
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <returns></returns>
        private Int64 getNumberOfLines(String inputFileName) {
            StreamReader inputReader = new StreamReader(inputFileName, true);
            Int64 linesReaded = 0;
            String line = "";
            do {
                line = inputReader.ReadLine();
                linesReaded++;
            } while (line != null);
            inputReader.Close();
            return linesReaded;
        }

        /// <summary>
        /// Split file by number of lines
        /// </summary>
        /// <param name="inputFileName"></param>
        /// <param name="fileNameInfo"></param>
        /// <param name="sourceFileSize"></param>
        private void splitByLines(String inputFileName, Int64 sourceFileSize) {

            // File Pattern
            Int64 actualFileNumber = 1;
            String actualFileName = getNextFileName(actualFileNumber);

            // Error if cant create new file
            StreamReader inputReader = new StreamReader(inputFileName, true);
            Encoding enc = inputReader.CurrentEncoding;
            StreamWriter outputWriter = new StreamWriter(actualFileName, false, enc, BUFFER_SIZE_BIG);

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

            // Minimum Part Size allowed 4kb
            if (this.PartSize < MINIMUM_PART_SIZE) {
                onMessage(ExceptionMessage.ERROR_MINIMUN_PART_SIZE, MINIMUM_PART_SIZE);
                throw new SplitFailedException();
            }

            // Prepare file buffer
            int bufferSize = BUFFER_SIZE_BIG;

            if (bufferSize > this.PartSize) {
                bufferSize = Convert.ToInt32(this.PartSize);
            }
            byte[] buffer = new byte[bufferSize];
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
                onMessage(ExceptionMessage.ERROR_OPENING_FILE);
                throw new SplitFailedException();
            }

            // Error if cant create new file
            try {
                stmWriter = File.Create(actualFileName);
            } catch {
                onMessage(ExceptionMessage.ERROR_CREATING_FILE);
                throw new SplitFailedException();
            }

            Int64 parts = this.Parts;
            Int64 bytesInPart = 0;
            Int32 bytesInBuffer = 1;

            // keep going while there is unprocessed data left in the input buffer
            while (bytesInBuffer > 0) {   

                // Read the file to current file pointer to fill buffer from 0 to total length
                bytesInBuffer = stmOriginal.Read(buffer, 0, buffer.Length);

                // If contains data process the buffer readed
                if (bytesInBuffer > 0) {

                    // The entire block can be written into the same file
                    if ((bytesInPart + bytesInBuffer) <= this.PartSize) {
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
                onMessage(ExceptionMessage.ERROR_TOTALSIZE_NOTEQUALS);
                throw new SplitFailedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalFile"></param>
        private void assertDriveSpace(String destination, Int64 requiredSize) {
            // Check Space available
            DirectoryInfo di = new DirectoryInfo(destination);
            DriveInfo driveInfo = new DriveInfo(di.Root.Name);

            if (driveInfo.DriveType == DriveType.Fixed || driveInfo.DriveType == DriveType.Removable) {

                // Exception if not space available
                if (driveInfo.AvailableFreeSpace <= requiredSize) {
                    onMessage(ExceptionMessage.ERROR_NO_SPACE_TO_SPLIT);
                    throw new SplitFailedException();
                }

                // Check Drive Format Limitations. Only for Fixed removable drives
                // FAT16 2GB
                // FAT32 4GB
                // FAT12 4GB
                if (driveInfo.DriveFormat == DriveFormat_FAT16) {
                    if (this.PartSize > DriveFormat_FAT16_MaxAmount * DriveFormat_FAT16_Factor) {
                        onMessage(ExceptionMessage.ERROR_FILESYSTEM_NOTALLOW_SIZE, DriveFormat_FAT16, DriveFormat_FAT16_MaxAmount, DriveFormat_FAT16_FactorName);
                        throw new SplitFailedException();
                    }
                } else if (driveInfo.DriveFormat == DriveFormat_FAT32) {
                    if (this.PartSize > DriveFormat_FAT32_MaxAmount * DriveFormat_FAT32_Factor) {
                        onMessage(ExceptionMessage.ERROR_FILESYSTEM_NOTALLOW_SIZE, DriveFormat_FAT32, DriveFormat_FAT32_MaxAmount, DriveFormat_FAT32_FactorName);
                        throw new SplitFailedException();
                    }
                } else if (driveInfo.DriveFormat == DriveFormat_FAT12) {
                    if (this.PartSize > DriveFormat_FAT12_MaxAmount * DriveFormat_FAT12_Factor) {
                        onMessage(ExceptionMessage.ERROR_FILESYSTEM_NOTALLOW_SIZE, DriveFormat_FAT12, DriveFormat_FAT12_MaxAmount, DriveFormat_FAT12_FactorName);
                        throw new SplitFailedException();
                    }
                }
            }
        }


        /// <summary>
        /// Do split operation
        /// </summary>
        public void doSplit() {
            try {
                // initializa variables
                partsCache = 0;

                // Set start event
                onStart();

                FileInfo fileNameInfo = new FileInfo(this.FileName);
                Int64 sourceFileSize = fileNameInfo.Length;

                // Builds default pattern if FileFormatPattern is null
                if (FileFormatPattern == null) {
                    // Use the part's string length (e.g. '123' -> 3) to determine the amount of padding needed
                    String zeros = new String('0', this.Parts.ToString().Length); // Padding
                    FileFormatPattern = Path.GetFileNameWithoutExtension(this.FileName) + "_{0:" + zeros + "}({1:" + zeros + "})" + fileNameInfo.Extension;
                }

                // Try create destination
                if (DestinationFolder != null) {
                    DirectoryInfo di = new DirectoryInfo(DestinationFolder);
                    if (!di.Exists) {
                        di.Create();
                    }
                } else {
                    //If destination not set use original file path
                    DestinationFolder = Path.GetDirectoryName(this.FileName);
                }


                // Checks drive space to on destination
                assertDriveSpace(DestinationFolder, sourceFileSize);


                if (OperationMode != SplitUnit.Lines) {
                    splitBySize(this.FileName, sourceFileSize);
                } else {
                    splitByLines(this.FileName, sourceFileSize);
                }

                // If no Exception breaks copy (delete new if required)
                if (DeleteOriginalFile && !fileNameInfo.IsReadOnly) {
                    fileNameInfo.Delete();
                }
            } catch (Exception) {
                //TODO

            } finally {
                onFinish();
            }
        }
    }
}
