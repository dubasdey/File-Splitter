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
	/// Types of message to be used in Message Args
	/// </summary>
    internal enum MESSAGETYPE {
        INFO,
        WARN,
        ERROR,
        FATAL
    }
	
    

    /// <summary>
    /// Arguments for Split message
    /// </summary>
    internal class MessageArgs : EventArgs {

    	/// <summary>
    	/// Message to show
    	/// </summary>
        public String Message { get; set; }
        
        /// <summary>
        /// Type of message
        /// </summary>
        public MESSAGETYPE Type { get; set; }

        /// <summary>
        /// Constructor for the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public MessageArgs(String message, MESSAGETYPE type) {
            this.Message = message;
            this.Type = type;
        }
    }

}
