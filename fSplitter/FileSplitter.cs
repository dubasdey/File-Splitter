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
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FileSplitter {

    /// <summary>
    /// Arguments for Split process event
    /// </summary>
    internal class SplitProcessArgs: EventArgs{
       
        /// <summary>
        /// File Name
        /// </summary>
        private String fileName  = String.Empty;

        /// <summary>
        /// Part number of total parts
        /// </summary>
        private double part = 0;

        /// <summary>
        /// Partsize Written
        /// </summary>
        private double partSizeWritted = 0;

        /// <summary>
        /// Total parts
        /// </summary>
        private double parts;

        /// <summary>
        /// Size in bytes of each part
        /// </summary>
        private double partSize;

        /// <summary>
        /// Geter for FileName
        /// </summary>
        public String FileName {
            get { return fileName; }
        }

        /// <summary>
        /// Getter for Actual Part
        /// </summary>
        public double Part {
            get { return part; }
        }

        /// <summary>
        /// Getter for bytes written of actual Part
        /// </summary>
        public double PartSizeWritted {
            get { return partSizeWritted; }
        }

        /// <summary>
        /// Getter for  Size of a part in bytes
        /// </summary>
        public double PartSize {
            get { return partSize; }
            set { partSize = value; }
        }

        /// <summary>
        /// Getter for  Total parts expected
        /// </summary>
        public double Parts {
            get { return parts; }
            set { parts = value; }
        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="file">Actual processing file</param>
        /// <param name="part">Actual part</param>
        /// <param name="partSizeWritten">Files written in this part</param>
        /// <param name="totalParts">Total parts</param>
        /// <param name="partSize">Total size expected of each part</param>
        public SplitProcessArgs(String file, double part, double partSizeWritten,double totalParts,double partSize) {
            this.fileName = file;
            this.part  = part;
            this.partSizeWritted = partSizeWritten;
            this.parts = totalParts;
            this.partSize = partSize;
        }
    }


    /// <summary>
    /// File Splitter class.
    /// This class do all calc and splitting operations
    /// </summary>
    internal class FileSplitter {

        /// <summary>
        /// Default buffer size
        /// </summary>
        private static Int32 BUFFER_SIZE = 1024 * 4;
        private static Int32 BUFFER_SIZE_BIG = 1024 * 1024 * 10;
       

        /// <summary>
        /// Delegate for Split star
        /// </summary>
        public delegate void splitStartHandler();

        /// <summary>
        /// Delegate for Split end
        /// </summary>
        public delegate void splitEndHandler();

        /// <summary>
        /// Delegate for Split process
        /// </summary>
        /// <param name="sender">splitter</param>
        /// <param name="args">process parameters</param>
        public delegate void splitProcessHandler(Object sender,SplitProcessArgs args);

        /// <summary>
        /// Spliter Start event
        /// </summary>
        public event splitStartHandler splitStart;

        /// <summary>
        /// Splitern End event
        /// </summary>
        public event splitEndHandler splitEnd;

        /// <summary>
        /// Splitter process event
        /// </summary>
        public event splitProcessHandler splitProcess;


        /// <summary>
        /// Size of a part in bytes
        /// </summary>
        private double partSizeBytes;

        /// <summary>
        /// Source filename to split
        /// </summary>
        private String fileName;

        /// <summary>
        /// Splitter constructor
        /// </summary>
        public FileSplitter(){
            partSizeBytes = 0;
            fileName = String.Empty;
        }

        /// <summary>
        /// Getter for the part size in bytes
        /// </summary>
        public double PartSize {
            get { return partSizeBytes; }
        }
        
        /// <summary>
        /// Sets the part size
        /// </summary>
        /// <param name="partSize">part size in partSizeUnits scale</param>
        /// <param name="partSizeUnits">
        ///     Unit of partsize
        ///     <list type="">
        ///         <item>0 bytes</item>
        ///         <item>1 Kbytes</item>
        ///         <item>2 Mbytes</item>
        ///         <item>3 Gbytes</item>
        ///     </list>
        /// </param>
        public void setPartSize(decimal partSize, Int32 partSizeUnits) {
            Int32 index = partSizeUnits;
            partSizeBytes = Convert.ToDouble(partSize);
            if (index > 0) {
                partSizeBytes = Convert.ToInt32(partSize) * Math.Pow(1024, index);
            }
        }

        /// <summary>
        /// Filename to be splitted
        /// </summary>
        public String FileName {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Calculates number of parts, based on size of file a part size
        /// </summary>
        public double Parts {
            get {
                double parts = 1;
                if (fileName.Length > 0 && File.Exists(fileName)) {
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Length > partSizeBytes) {
                        parts = Math.Ceiling((double)fi.Length / partSizeBytes);
                    }
                }
                return parts;
            }
        }

        /// <summary>
        /// Launch splitStart event
        /// </summary>
        private void onSplitStart() {
            if (splitStart != null) {
                splitStart();
            }
        }

        /// <summary>
        /// Launch splitEnd event
        /// </summary>
        private void onSplitEnd() {
            if (splitEnd != null) {
                splitEnd();
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
        private void onSplitProcess(String filename, double part, double partSizeWritten, double totalParts, double partSize) {
            if (splitProcess != null) {
                splitProcess(this, new SplitProcessArgs(filename, part, partSizeWritten, totalParts, partSize));
            }
        }

        /// <summary>
        /// Do split operation
        /// </summary>
        public void doSplit() {
            onSplitStart();

            try {
                FileInfo fileNameInfo = new FileInfo (fileName);
                DriveInfo driveInfo = new DriveInfo(fileName);
                if (driveInfo.AvailableFreeSpace <= fileNameInfo.Length) {
                    MessageBox.Show("No Space available to split", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    onSplitEnd();
                    return;
                }

                FileStream stmOriginal = null;
                try {
                    stmOriginal = File.OpenRead(fileName);
                } catch {
                    MessageBox.Show("Error opening " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    onSplitEnd();
                    return;
                }

                byte[] buffer = new byte[BUFFER_SIZE];
                // if buffer is greater than partsize less bufer size
                if (partSizeBytes < BUFFER_SIZE) {
                    buffer = new byte[(int)partSizeBytes];
                    // if partSizeBytes is greater than 10 mb buffer of 10mb
                    // this reduces read/writting operations
                } else if (partSizeBytes > BUFFER_SIZE_BIG) {
                    buffer = new byte[BUFFER_SIZE_BIG];
                }

                double totalParts = this.Parts;
                int readedBytes = stmOriginal.Read(buffer, 0, buffer.Length);
                double actualPartSizeWritted = 0;
                double actualFileNumber = 0;
                String actualFileName = fileName + "." + actualFileNumber;
                FileStream stmWritter = File.Create(actualFileName);



                while (readedBytes > 0) {

                    // write buffer
                    stmWritter.Write(buffer, 0, readedBytes);

                    // increase part size
                    actualPartSizeWritted += readedBytes;

                    // launch processing event
                    onSplitProcess(actualFileName, actualFileNumber, actualPartSizeWritted, totalParts, partSizeBytes);

                    // nextPart?
                    if (actualPartSizeWritted >= partSizeBytes) {
                        actualFileNumber++;
                        actualPartSizeWritted = 0;
                        stmWritter.Flush();
                        stmWritter.Close();
                        actualFileName = fileName + "." + actualFileNumber;
                        stmWritter = File.Create(actualFileName);
                    }

                    // if readed less than buffer is last part
                    if (readedBytes < buffer.Length) {
                        break;
                    }
                    // read next
                    readedBytes = stmOriginal.Read(buffer, 0, buffer.Length);
                }
            } catch {
                MessageBox.Show("Error creating files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                onSplitEnd();
            }
        }
    }
}
