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

namespace FileSplitter.Enums {
    /// <summary>
    /// The unit of how to split the files
    /// </summary>
    /// <created>Nick</created>
    public enum SplitUnit {

        /// <summary>
        /// Specify an incorrect entry to allow the reflection-based parser to return 
        /// default(T) without additional constructors and wrappers.
        /// </summary>
        /// <created>Nick</created>
        Incorrect = 0,
        [UnitAttribute(CommandLine.UnitBytes, 0)]
        Bytes,
        [UnitAttribute(CommandLine.UnitKiloBytes, 1)]
        KiloBytes,
        [UnitAttribute(CommandLine.UnitMegaBytes, 2)]
        MegaBytes,
        [UnitAttribute(CommandLine.UnitGigaBytes, 3)]
        GigaBytes,
        [UnitAttribute(CommandLine.UnitLines, 0)]
        Lines
    }
}