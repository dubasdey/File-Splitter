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
    /// Arguments for Split message
    /// </summary>
    public class MessageArgs : EventArgs {
    	/// <summary>
    	/// Message to show
    	/// </summary>
        public ExceptionMessage Message { get; set; }        
        public Object[] Parameters { get; set; }
        /// <summary>
        /// Constructor for the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public MessageArgs(ExceptionMessage message, Object[] parameters) {
            this.Message = message;
            this.Parameters = parameters;
        }
    }
}