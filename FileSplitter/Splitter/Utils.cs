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
using FileSplitter.Enums;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileSplitter {

    public abstract class Utils {
        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string srcFile) {
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            FileStream file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf) {
                enc = Encoding.UTF8;
            } else if (buffer[0] == 0xfe && buffer[1] == 0xff) {
                enc = Encoding.Unicode;
            } else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff) {
                enc = Encoding.UTF32;
            } else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76) {
                enc = Encoding.UTF7;
            } else {
                enc = Encoding.ASCII;
            }
            return enc;
        }
        /// <summary>
        ///  Unit order converter
        /// </summary>
        /// <param name="items"></param>
        /// <param name="units">Kind of split unit to use</param>
        /// <returns></returns>
        public static Int64 unitConverter(Int64 items, SplitUnit units) {
            Int64 result = items;
            // Make sure to check if it's something valid we can split
            if (units != SplitUnit.Incorrect)
            {
                var info = UnitAttribute.GetFromField<SplitUnit>(units);
                // If the GetFromField fails; make sure to test it's not null
                if (info != null && info.Factor > 0)
                {
                    result = (Int64)Math.Ceiling(items * info.CalculatedFactor);
                }
            }
            return result;
        }
        public static String getMessageText(ExceptionMessage msg,params Object[] args) {
            String message;
            switch (msg) {
                case ExceptionMessage.ERROR_FILESYSTEM_NOTALLOW_SIZE:
                    message = String.Format(Properties.Resources.ERROR_FILESYSTEM_NOTALLOW_SIZE, args);
                    break;
                case ExceptionMessage.ERROR_MINIMUN_PART_SIZE:
                    message = string.Format(Properties.Resources.ERROR_MINIMUN_PART_SIZE, args);
                    break;
                case ExceptionMessage.ERROR_NO_SPACE_TO_SPLIT:
                    message = Properties.Resources.ERROR_NO_SPACE_TO_SPLIT;
                    break;
                case ExceptionMessage.ERROR_OPENING_FILE:
                    message = String.Format(Properties.Resources.ERROR_OPENING_FILE, args);
                    break;
                case ExceptionMessage.ERROR_TOTALSIZE_NOTEQUALS:
                    message = Properties.Resources.ERROR_TOTALSIZE_NOTEQUALS;
                    break;
                default:
                    message = string.Empty;
                    break;
            }
            return message;
        }
        public static MessageBoxIcon getMessageIcon(ExceptionMessage msg) {
            MessageBoxIcon icon;
            switch (msg) {
                case ExceptionMessage.ERROR_NO_SPACE_TO_SPLIT:
                case ExceptionMessage.ERROR_OPENING_FILE:
                case ExceptionMessage.ERROR_CREATING_FILE:
                case ExceptionMessage.ERROR_FILESYSTEM_NOTALLOW_SIZE:
                case ExceptionMessage.ERROR_MINIMUN_PART_SIZE:
                case ExceptionMessage.ERROR_TOTALSIZE_NOTEQUALS:
                    icon = MessageBoxIcon.Error;
                    break;
                default:
                    icon = MessageBoxIcon.Information;
                    break;
            }
            return icon;
        }
    }
}