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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileSplitter {

    /// <summary>
    /// Splitter Form
    /// </summary>
    public partial class FrmSplitter : Form {

        /// <summary>
        /// Spliter class. Do Split operations
        /// </summary>
        private FileSplitter fileSplitter;

        /// <summary>
        /// Start Components & assign events
        /// </summary>
        public FrmSplitter() {
            
            InitializeComponent();

            // splitter object
            fileSplitter = new FileSplitter();
            fileSplitter.splitStart += new FileSplitter.splitStartHandler(fileSplitter_splitStart);
            fileSplitter.splitEnd += new FileSplitter.splitEndHandler(fileSplitter_splitEnd);
            fileSplitter.splitProcess += new FileSplitter.splitProcessHandler(fileSplitter_splitProcess); 
            
            // 0 bytes 1 kbytes  2 Mbytes
            cmbUnits.SelectedIndex = 2;
            numSize.Value = 10;
            fileSplitter.setPartSize(10, 2); 
        }

        /// <summary>
        /// Event launched when Split is in process
        /// </summary>
        /// <param name="sender">spliter object</param>
        /// <param name="args">Paramaters of actual split</param>
        void fileSplitter_splitProcess(object sender, SplitProcessArgs args) {
            progressBarFiles.Maximum = 100;
            progressBarFiles.Step = 1;
            progressBarFileSize.Maximum = 100;
            progressBarFileSize.Step = 1;

            lbSplitInfo.Text = String.Format(lbSplitInfo.Tag.ToString(), args.FileName);
            int percPart = Convert.ToInt32((args.Part * 100) / args.Parts);
            int percSize = Convert.ToInt32((args.PartSizeWritted * 100) / args.PartSize);

            if (percPart < progressBarFiles.Maximum) {
                progressBarFiles.Value = percPart;
            } else {
                progressBarFiles.Value = progressBarFiles.Maximum;
            }

            if (percSize < progressBarFileSize.Maximum) {
                progressBarFileSize.Value = percSize;
            }else{
                progressBarFileSize.Value = progressBarFileSize.Maximum;
            }
            Application.DoEvents();
        }

        /// <summary>
        /// Event launched on Split ends
        /// </summary>
        void fileSplitter_splitEnd() {
            panelSpitting.Visible = false;
            grpSplitSize.Enabled = true;
            cmdStart.Enabled = true;
            cmdSelectFile.Enabled = true;
        }

        /// <summary>
        /// Event launched on Split Start
        /// </summary>
        void fileSplitter_splitStart() {
            panelSpitting.Visible = true;
            grpSplitSize.Enabled = false;
            cmdStart.Enabled = false;
            cmdSelectFile.Enabled = false;
        }

        /// <summary>
        /// Button for File selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelectFile_Click(object sender, EventArgs e) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                cmdStart.Enabled = true;
                fileSplitter.FileName =  this.txtFile.Text = openFileDialog.FileName;
                lbEstimatedParts.Text = String.Format(lbEstimatedParts.Tag.ToString(), fileSplitter.Parts);
            }
        }

        /// <summary>
        /// Event on number or type of size changes. 
        /// this recalcs the number of parts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlValueChangedEvent(object sender, EventArgs e) {
            fileSplitter.setPartSize(numSize.Value, cmbUnits.SelectedIndex);   
            lbEstimatedParts.Text = String.Format(lbEstimatedParts.Tag.ToString(), fileSplitter.Parts);
        }

        /// <summary>
        /// Start Splitting operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbStart_Click(object sender, EventArgs e) {
            fileSplitter.doSplit();
        }
    }
}
