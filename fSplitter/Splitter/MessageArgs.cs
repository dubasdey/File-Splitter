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
	
    internal enum MESSAGE {
        ERROR_MINIMUN_PART_SIZE,
        ERROR_OPENING_FILE,
        ERROR_TOTALSIZE_NOTEQUALS,
        ERROR_NO_SPACE_TO_SPLIT,
        ERROR_FILESYSTEM_NOTALLOW_SIZE
    }

    /// <summary>
    /// Arguments for Split message
    /// </summary>
    internal class MessageArgs : EventArgs {

    	/// <summary>
    	/// Message to show
    	/// </summary>
        public MESSAGE Message { get; set; }
        
        public Object[] Parameters { get; set; }

        /// <summary>
        /// Constructor for the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public MessageArgs(MESSAGE message, Object[] parameters) {
            this.Message = message;
            this.Parameters = parameters;
        }
    }

}
