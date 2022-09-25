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
using FileSplitter.Enums;
using System;

namespace FileSplitter {

    /// <summary>
    /// Item to use in a ComboBox 
    /// </summary>
    internal class SplitUnitComboboxItem {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Associated text</param>
        /// <param name="key">SplitUnit</param>
        public SplitUnitComboboxItem(String text, SplitUnit key) {
            this.Text = text;
            this.Value = key;
        }

        /// <summary>
        /// Text for item
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Real value of item
        /// </summary>
        public SplitUnit Value { get; set; }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return Text;
        }
    }
}