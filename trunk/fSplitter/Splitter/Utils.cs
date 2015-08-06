using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSplitter {

    internal enum OPERATION_SPIT {
        BY_BYTE,
        BY_KBYTE,
        BY_MBYTE,
        BY_GBYTE,
        BY_LINES

    }


    internal abstract class Utils {

        /// <summary>
        ///  Unit order converter
        /// </summary>
        /// <param name="items"></param>
        /// <param name="unitOrder">0 bytes 1 kbytes 2 Mbytes 3 Gb 4 Lines</param>
        /// <returns></returns>
        public static Int64 unitConverter(Int64 items, OPERATION_SPIT units) {
            Int64 result = items;
            Int64 factor = 0;
            switch (units) {
                case OPERATION_SPIT.BY_KBYTE:
                    factor = 1;
                    break;
                case OPERATION_SPIT.BY_MBYTE:
                    factor = 2;
                    break;
                case OPERATION_SPIT.BY_GBYTE:
                    factor = 3;
                    break;
            }
            if (factor > 0) {
                result = (Int64)Math.Ceiling(items * Math.Pow(1024, factor));
            }
            return result;
        }

        public static String getMessageText(MESSAGE msg,params Object[] args) {
            String message = "";
            switch (msg) {
                case MESSAGE.ERROR_FILESYSTEM_NOTALLOW_SIZE:
                    message = String.Format(Properties.Resources.ERROR_FILESYSTEM_NOTALLOW_SIZE, args);
                    break;
                case MESSAGE.ERROR_MINIMUN_PART_SIZE:
                    message = String.Format(Properties.Resources.ERROR_MINIMUN_PART_SIZE, args);
                    break;
                case MESSAGE.ERROR_NO_SPACE_TO_SPLIT:
                    message = Properties.Resources.ERROR_NO_SPACE_TO_SPLIT;
                    break;
                case MESSAGE.ERROR_OPENING_FILE:
                    message = String.Format(Properties.Resources.ERROR_OPENING_FILE, args);
                    break;
                case MESSAGE.ERROR_TOTALSIZE_NOTEQUALS:
                    message = Properties.Resources.ERROR_OPENING_FILE;
                    break;
            }
            return message;
        }

        public static MessageBoxIcon getMessageIcon(MESSAGE msg) {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            switch (msg) {
                case MESSAGE.ERROR_NO_SPACE_TO_SPLIT:
                case MESSAGE.ERROR_OPENING_FILE:
                    icon = MessageBoxIcon.Hand;
                    break;
                case MESSAGE.ERROR_FILESYSTEM_NOTALLOW_SIZE:
                case MESSAGE.ERROR_MINIMUN_PART_SIZE:
                case MESSAGE.ERROR_TOTALSIZE_NOTEQUALS:
                    icon = MessageBoxIcon.Error;
                    break;
            }
            return icon;
        }
    }
}
