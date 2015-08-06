﻿/*
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

            // Init Texts
            this.lbFile.Text = Properties.Resources.FILE;
            this.grpSplitSize.Text = Properties.Resources.SPLITSIZE;
            this.cmdStart.Text = Properties.Resources.START;
            this.openFileDialog.Filter = Properties.Resources.FILE_EXTENSIONS;
            this.lbCurrentFileProgress.Text = Properties.Resources.ACTUAL_FILE_PROGRESS;
            this.lbAllFilesProgress.Text = Properties.Resources.ALL_FILES_PROGRESS;
            this.lbInfo.Text = Properties.Resources.SPLIT_INFO;
            this.lbEstimatedParts.Text = Properties.Resources.SPLINT_INFO_PARTS_DEF;
            this.Text = Properties.Resources.TITLE;

            cmbUnits.Items.Add(new ComboboxItem("bytes", OPERATION_SPIT.BY_BYTE));
            cmbUnits.Items.Add(new ComboboxItem("Kilobytes", OPERATION_SPIT.BY_KBYTE));
            cmbUnits.Items.Add(new ComboboxItem("Megabytes", OPERATION_SPIT.BY_MBYTE));
            cmbUnits.Items.Add(new ComboboxItem("Gigabytes", OPERATION_SPIT.BY_GBYTE));
            cmbUnits.Items.Add(new ComboboxItem(Properties.Resources.CMB_LINES, OPERATION_SPIT.BY_LINES));

            fileSplitter = new FileSplitter();
            fileSplitter.start += new FileSplitter.StartHandler(fileSplitter_splitStart);
            fileSplitter.finish += new FileSplitter.FinishHandler(fileSplitter_splitEnd);
            fileSplitter.processing += new FileSplitter.ProcessHandler(fileSplitter_splitProcess);
            fileSplitter.message += new FileSplitter.MessageHandler(fileSplitter_message);

            cmbUnits.SelectedIndex = 2;


            loadPreferences();
        }

        void fileSplitter_message(object server, MessageArgs args) {
            MessageBox.Show(Utils.getMessageText(args.Message,args.Parameters), "", MessageBoxButtons.OK, Utils.getMessageIcon(args.Message));
        }

        /// <summary>
        /// Event launched when Split is in process
        /// </summary>
        /// <param name="sender">spliter object</param>
        /// <param name="args">Paramaters of actual split</param>
        void fileSplitter_splitProcess(object sender, ProcessingArgs args) {
            lbSplitInfo.Text = String.Format(Properties.Resources.SPLITTING_FILE, args.FileName);
            if (fileSplitter.OperationMode !=  OPERATION_SPIT.BY_LINES) {
                progressBarFiles.Style = ProgressBarStyle.Continuous;
                int percPart = Convert.ToInt32((args.Part * 100) / args.Parts);
                if (percPart < progressBarFiles.Maximum) {
                    progressBarFiles.Value = percPart;
                } else {
                    progressBarFiles.Value = progressBarFiles.Maximum;
                }
            } else {
                progressBarFiles.Style = ProgressBarStyle.Marquee;
            }

            int percSize = Convert.ToInt32((args.PartSizeWritten * 100) / args.PartSize);
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
                if (fileSplitter.OperationMode !=  OPERATION_SPIT.BY_LINES) {
                    lbEstimatedParts.Text = String.Format(Properties.Resources.SPLIT_INFO_PARTS, fileSplitter.Parts);
                } else {
                    lbEstimatedParts.Text = Properties.Resources.NUMBER_OF_LINES;
                }
            }
        }

        /// <summary>
        /// Event on number or type of size changes. 
        /// this recalcs the number of parts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlValueChangedEvent(object sender, EventArgs e) {
            updatePreferences();
            if (cmbUnits.SelectedItem != null) {
                fileSplitter.OperationMode = ((ComboboxItem)cmbUnits.SelectedItem).Value;
            }
            fileSplitter.PartSize = Utils.unitConverter((Int64)numSize.Value, fileSplitter.OperationMode);
            if (fileSplitter.OperationMode != OPERATION_SPIT.BY_LINES ){
                lbEstimatedParts.Text = String.Format(Properties.Resources.SPLIT_INFO_PARTS, fileSplitter.Parts);
            } else {
                lbEstimatedParts.Text = Properties.Resources.NUMBER_OF_LINES;
            }
        }

        /// <summary>
        /// Start Splitting operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbStart_Click(object sender, EventArgs e) {
            fileSplitter.doSplit();
        }

        /// <summary>
        /// Load User preferences
        /// </summary>
        private void loadPreferences() {
            cmbUnits.SelectedIndex = Properties.Settings.Default.typeIndex;
            numSize.Value = Properties.Settings.Default.itemsNumber;
            fileSplitter.OperationMode = ((ComboboxItem)cmbUnits.SelectedItem).Value;
            fileSplitter.PartSize = Utils.unitConverter((Int64)numSize.Value, fileSplitter.OperationMode);
        }

        /// <summary>
        /// Store user Prefences
        /// </summary>
        private void updatePreferences() {
            Properties.Settings.Default.typeIndex = cmbUnits.SelectedIndex;
            Properties.Settings.Default.itemsNumber = (Int32)numSize.Value;
            Properties.Settings.Default.Save();
        }
    }
}
