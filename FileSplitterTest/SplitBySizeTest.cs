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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using FileSplitter;
using FileSplitter.Enums;
using System.Diagnostics;

namespace FileSplitterTest {

    /// <summary>
    /// Split Test
    /// </summary>
    [TestClass]
    public class SplitBySizeTest {

        /// <summary>
        /// Common Split test
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="size"></param>
        private void sizeTest(SplitUnit unit,int size) {
            
            String tempPath = Path.GetTempPath() + @"fSplitTest\";
            String fileName = tempPath +"file" + size + unit + "F.txt";
            System.IO.Directory.CreateDirectory(tempPath);
            Debug.WriteLine("Split " + fileName + " to " + tempPath);

            TestUtils.createFile(fileName, TestUtils.SIZE_2MB);

            FileSplitWorker worker = new FileSplitWorker();

            worker.DeleteOriginalFile = true;
            worker.DestinationFolder = tempPath;
            worker.FileName = fileName;

            worker.OperationMode = unit;
            worker.PartSize = Utils.unitConverter(size, unit);

            worker.doSplit();
        }

        [TestMethod]
        public void test1() {
            Debug.WriteLine("Split 2 MB File");
            sizeTest(SplitUnit.MegaBytes, 2);


        }


    }
}
