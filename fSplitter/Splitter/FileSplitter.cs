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

namespace FileSplitter {



    /// <summary>
    /// File Splitter class.
    /// This class does all the calculations and the splitting operations
    /// </summary>
    internal class FileSplitter {

        /// <summary>
        /// Default buffer size
        /// </summary>
        private static Int32 BUFFER_SIZE = 1024 * 4;
        private static Int32 BUFFER_SIZE_BIG = 1024 * 1024 * 10;
       

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
        /// Calculates number of parts, based on size of file a part size
        /// </summary>
        // modified to return long
        // relies on other code to ensure file name exists
        public Int64 Parts {
            get {
                Int64 parts = 1;
                if (this.FileName!=null && this.FileName.Length > 0 && File.Exists(this.FileName)) {
                    FileInfo fi = new FileInfo(this.FileName);
                    if (fi.Length > this.PartSize) {
                        parts = (Int64)Math.Ceiling((double)fi.Length / this.PartSize); 
                    }
                }
                return parts;
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
        private void onProcessing(String filename, Int64 part, Int64 partSizeWritten, Int64 totalParts, Int64 partSize)
        {
            if (processing != null) {
                processing(this, new ProcessingArgs(filename, part, partSizeWritten, totalParts, partSize));
            }
        }

        /// <summary>
        /// Launch Split Message event
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        private void onMessage(String msg, MESSAGETYPE type) {
            if (message != null) {
                message(this, new MessageArgs(msg, type));
            }
        }

        /// <summary>
        /// Do split operation
        /// </summary>
        public void doSplit() {
            try {
				onStart();
				
            	// Minimun Part Size allowed 4kb
            	if (this.PartSize <4096){
					onMessage("Minimun part size must be 4Kb", MESSAGETYPE.WARN);
                    return;
            	}
                
                FileInfo fileNameInfo = new FileInfo (this.FileName);

                // Check Space available
                DriveInfo driveInfo = new DriveInfo(this.FileName);
                Int64 sourceFileSize = fileNameInfo.Length;
                if (driveInfo.AvailableFreeSpace <= fileNameInfo.Length) {
                    onMessage("No Space available to split", MESSAGETYPE.FATAL);
                    return;
                }
                
                
                // Check Drive Format Limitations
                if (driveInfo.DriveFormat == "FAT16") { // 2gb
                    if (this.PartSize > 2 * 1073741824L) {
                        onMessage("FAT16 File systems does not allow more than 2 Gb for each file.", MESSAGETYPE.ERROR);
                        return;
                    }
                }else  if (driveInfo.DriveFormat == "FAT32") {  // 4gb
                    if (this.PartSize > 4 * 1073741824L) {
                        onMessage("FAT32 File systems does not allow more than 4 Gb for each file.", MESSAGETYPE.ERROR);
                        return;
                    }
                }


                // Check if file can be opened for read
                FileStream stmOriginal = null;
                try {
                    stmOriginal = File.OpenRead(this.FileName);
                } catch {
                    onMessage("Error opening " + this.FileName, MESSAGETYPE.FATAL);
                    onFinish();
                    return;
                }

                // Prepare file buffer
                byte[] buffer = new byte[BUFFER_SIZE];
                // if buffer is greater than partsize, use smaller buffer size
                if (this.PartSize < BUFFER_SIZE) {
                    buffer = new byte[(int)this.PartSize];
                    // if partSizeBytes is greater than 10 mb, use buffer of 10mb
                    // this reduces read/write operations
                } else if (this.PartSize > BUFFER_SIZE_BIG) {
                    buffer = new byte[BUFFER_SIZE_BIG];
                }


                

                // File Pattern
                Int64 actualFileNumber = 1;
                String zeros = new String('0', this.Parts.ToString().Length); // Padding
                String fileNamePattern = fileNameInfo.DirectoryName + Path.GetFileNameWithoutExtension(this.FileName) + "_{0:" + zeros + "}({1:" + zeros + "})" + fileNameInfo.Extension;
                
				String actualFileName = String.Format(fileNamePattern,actualFileNumber, this.Parts);
                
                // Error if cant create new file
                FileStream stmWriter = File.Create(actualFileName);
                
                Int64 parts = this.Parts;
               	Int64 bytesInPart = 0;
               	Int64 bytesInTotal = 0;
               	Int32 bytesInBuffer  = 1; 
                while (bytesInBuffer > 0) {    // keep going while there is unprocessed data left in the input buffer
  
					// Read the file to current file pointer to fill buffer from 0 to total length
					bytesInBuffer = stmOriginal.Read(buffer, 0, buffer.Length);    
					
					// If contains data process the buffer readed
					if (bytesInBuffer>0){
						
						// The entire block can be written into the same file
						if ((bytesInPart + bytesInBuffer)<= this.PartSize){
							stmWriter.Write(buffer, 0, bytesInBuffer);
							bytesInPart+=bytesInBuffer;
							
						// Finish the current file and start a new file if required
						}else{
							
							// Fill the current file to the Full size if has pending content
							Int32 pendingToWrite = (Int32) (this.PartSize - bytesInPart);
							
							// Write the pending content to the current file
							// If 0 The size written in last iteration is equals to block size
							if (pendingToWrite>0){
								stmWriter.Write(buffer, 0, pendingToWrite);
							}
							stmWriter.Flush();
							stmWriter.Close();

							// If the last write does not fullfill all the content, continue
							if ((bytesInTotal + pendingToWrite)<sourceFileSize){
								bytesInPart=0;		
								
								actualFileNumber++;
		                        actualFileName = String.Format(fileNamePattern, actualFileNumber, parts);
		                        stmWriter = File.Create(actualFileName);	
		                        
		                        // Write the rest of the buffer if required into the new file
		                        // if pendingToWrite is more than 0 write the part not written in previous file
		                        // else write all in the new file
		                        if (pendingToWrite>0 && pendingToWrite<=bytesInBuffer){
		                        	//stmWriter.Write(buffer,bytesInBuffer - pendingToWrite, bytesInBuffer);
		                        	stmWriter.Write(buffer,pendingToWrite, (bytesInBuffer-pendingToWrite));
		                        }else if (pendingToWrite==0){
		                        	stmWriter.Write(buffer,0, bytesInBuffer);
		                        }
							}
						}
						
						bytesInTotal+=bytesInBuffer;
						onProcessing(actualFileName, actualFileNumber, bytesInPart, parts, this.PartSize );
					
					// If no more data in source file close last stream
					}else{
						stmWriter.Flush();
						stmWriter.Close();
					}
                }

               	
                if (bytesInTotal != sourceFileSize) {
                    onMessage("The total size of all the output file parts is not equal to the original data file size !", MESSAGETYPE.ERROR);
                }

           /* } catch (Exception ex) {
                onMessage("Error creating files." + ex.Message + "\n" + ex.Source, MESSAGETYPE.FATAL);*/
            } finally {
                onFinish();
            }
        }
    }
}
