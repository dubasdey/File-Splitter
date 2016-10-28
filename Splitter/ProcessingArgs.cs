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

    /// <summary>
    /// Arguments to use in File Splitter events
    /// </summary>
    public class ProcessingArgs : EventArgs {

        /// <summary>
        /// File Name
        /// </summary>
        public String FileName { get; private set; } = String.Empty;

        /// <summary>
        /// Part number of total parts
        /// </summary>
        public Int64 Part { get; private set; } = 0;

        /// <summary>
        /// Partsize Written
        /// </summary>
        public Int64 PartSizeWritten { get; private set; } = 0;

        /// <summary>
        /// Total parts
        /// </summary>
        public Int64 Parts { get; private set; }

        /// <summary>
        /// Size in bytes of each part
        /// </summary>
        public Int64 PartSize { get; private set; }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="file">Actual processing file</param>
        /// <param name="part">Actual part</param>
        /// <param name="partSizeWritten">Files written in this part</param>
        /// <param name="totalParts">Total parts</param>
        /// <param name="partSize">Total size expected of each part</param>
        public ProcessingArgs(String file, Int64 part, Int64 partSizeWritten, Int64 totalParts, Int64 partSize) {
            FileName = file;
            Part = part;
            PartSizeWritten = partSizeWritten;
            Parts = totalParts;
            PartSize = partSize;
        }
    }
}