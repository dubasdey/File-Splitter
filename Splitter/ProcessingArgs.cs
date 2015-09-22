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

namespace FileSplitter {

    internal class ProcessingArgs : EventArgs {

        /// <summary>
        /// File Name
        /// </summary>
        private String fileName = String.Empty;

        /// <summary>
        /// Part number of total parts
        /// </summary>
        private Int64 part = 0;

        /// <summary>
        /// Partsize Written
        /// </summary>
        private Int64 partSizeWritten = 0;

        /// <summary>
        /// Total parts
        /// </summary>
        private Int64 parts;

        /// <summary>
        /// Size in bytes of each part
        /// </summary>
        private Int64 partSize;

        /// <summary>
        /// Geter for FileName
        /// </summary>
        public String FileName {
            get { return fileName; }
        }

        /// <summary>
        /// Getter for Actual Part
        /// </summary>
        public Int64 Part {
            get { return part; }
        }

        /// <summary>
        /// Getter for bytes written of actual Part
        /// </summary>
        public Int64 PartSizeWritten {
            get { return partSizeWritten; }
        }

        /// <summary>
        /// Getter for  Size of a part in bytes
        /// </summary>
        public Int64 PartSize {
            get { return partSize; }
            set { partSize = value; }
        }

        /// <summary>
        /// Getter for  Total parts expected
        /// </summary>
        public Int64 Parts {
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
        public ProcessingArgs(String file, Int64 part, Int64 partSizeWritten, Int64 totalParts, Int64 partSize) {
            this.fileName = file;
            this.part = part;
            this.partSizeWritten = partSizeWritten;
            this.parts = totalParts;
            this.partSize = partSize;
        }
    }
}