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
using System.IO;


namespace FileSplitterTest {

    /// <summary>
    /// Test Utils
    /// </summary>
    internal class TestUtils {

        /// <summary>
        /// 2 MB Const
        /// </summary>
        public const int SIZE_2MB = 1024 * 1024 * 2;


        /// <summary>
        /// Block of 256 bytes
        /// </summary>
        private const String BLOCK = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWERTYUIOPASDFGHJKLZXCVBNM1234567890QWER";

        /// <summary>
        /// Create a dummy file
        /// </summary>
        /// <param name="fileName"> Full path</param>
        /// <param name="size">size in bytes</param>
        public static void createFile(String fileName, int size) {

            StreamWriter file = new StreamWriter(fileName, false);

            int blocks = size / BLOCK.Length;
            int rest = size % BLOCK.Length;

            for (int i = 0; i < blocks; i++) {
                file.Write(BLOCK);
            }

            if (rest > 0) {
                String part = BLOCK.Substring(0, rest+1);
                file.Write(part);
            }
            
        }
    }
}
