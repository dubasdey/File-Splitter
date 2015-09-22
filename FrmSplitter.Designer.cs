namespace FileSplitter {
    partial class FrmSplitter {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplitter));
            this.lbFile = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.cmdSelectFile = new System.Windows.Forms.Button();
            this.grpSplitSize = new System.Windows.Forms.GroupBox();
            this.lbEstimatedParts = new System.Windows.Forms.Label();
            this.cmbUnits = new System.Windows.Forms.ComboBox();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.cmdStart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelSpitting = new System.Windows.Forms.Panel();
            this.lbCurrentFileProgress = new System.Windows.Forms.Label();
            this.lbAllFilesProgress = new System.Windows.Forms.Label();
            this.progressBarFileSize = new System.Windows.Forms.ProgressBar();
            this.progressBarFiles = new System.Windows.Forms.ProgressBar();
            this.lbSplitInfo = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.Label();
            this.grpSplitSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            this.panelSpitting.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFile
            // 
            this.lbFile.Location = new System.Drawing.Point(20, 11);
            this.lbFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(103, 20);
            this.lbFile.TabIndex = 0;
            this.lbFile.Text = "File";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(131, 9);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(435, 22);
            this.txtFile.TabIndex = 1;
            // 
            // cmdSelectFile
            // 
            this.cmdSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelectFile.Location = new System.Drawing.Point(574, 5);
            this.cmdSelectFile.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSelectFile.Name = "cmdSelectFile";
            this.cmdSelectFile.Size = new System.Drawing.Size(84, 28);
            this.cmdSelectFile.TabIndex = 2;
            this.cmdSelectFile.Text = "...";
            this.cmdSelectFile.UseVisualStyleBackColor = true;
            this.cmdSelectFile.Click += new System.EventHandler(this.cmdSelectFile_Click);
            // 
            // grpSplitSize
            // 
            this.grpSplitSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSplitSize.Controls.Add(this.lbEstimatedParts);
            this.grpSplitSize.Controls.Add(this.cmbUnits);
            this.grpSplitSize.Controls.Add(this.numSize);
            this.grpSplitSize.Location = new System.Drawing.Point(20, 39);
            this.grpSplitSize.Margin = new System.Windows.Forms.Padding(4);
            this.grpSplitSize.Name = "grpSplitSize";
            this.grpSplitSize.Padding = new System.Windows.Forms.Padding(4);
            this.grpSplitSize.Size = new System.Drawing.Size(546, 57);
            this.grpSplitSize.TabIndex = 3;
            this.grpSplitSize.TabStop = false;
            this.grpSplitSize.Text = "Split Size";
            // 
            // lbEstimatedParts
            // 
            this.lbEstimatedParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEstimatedParts.Location = new System.Drawing.Point(8, 20);
            this.lbEstimatedParts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbEstimatedParts.Name = "lbEstimatedParts";
            this.lbEstimatedParts.Size = new System.Drawing.Size(205, 25);
            this.lbEstimatedParts.TabIndex = 2;
            this.lbEstimatedParts.Tag = "";
            this.lbEstimatedParts.Text = "0 parts of";
            this.lbEstimatedParts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUnits
            // 
            this.cmbUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnits.FormattingEnabled = true;
            this.cmbUnits.Location = new System.Drawing.Point(312, 20);
            this.cmbUnits.Margin = new System.Windows.Forms.Padding(4);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Size = new System.Drawing.Size(224, 24);
            this.cmbUnits.TabIndex = 1;
            this.cmbUnits.SelectedIndexChanged += new System.EventHandler(this.controlValueChangedEvent);
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(221, 23);
            this.numSize.Margin = new System.Windows.Forms.Padding(4);
            this.numSize.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(83, 22);
            this.numSize.TabIndex = 0;
            this.numSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSize.ValueChanged += new System.EventHandler(this.controlValueChangedEvent);
            // 
            // cmdStart
            // 
            this.cmdStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStart.Enabled = false;
            this.cmdStart.Location = new System.Drawing.Point(574, 50);
            this.cmdStart.Margin = new System.Windows.Forms.Padding(4);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(84, 41);
            this.cmdStart.TabIndex = 4;
            this.cmdStart.Text = global::FileSplitter.Properties.Resources.START;
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.lbStart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "log";
            this.openFileDialog.Filter = global::FileSplitter.Properties.Resources.FILE_EXTENSIONS;
            // 
            // panelSpitting
            // 
            this.panelSpitting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSpitting.Controls.Add(this.lbCurrentFileProgress);
            this.panelSpitting.Controls.Add(this.lbAllFilesProgress);
            this.panelSpitting.Controls.Add(this.progressBarFileSize);
            this.panelSpitting.Controls.Add(this.progressBarFiles);
            this.panelSpitting.Controls.Add(this.lbSplitInfo);
            this.panelSpitting.Location = new System.Drawing.Point(20, 104);
            this.panelSpitting.Margin = new System.Windows.Forms.Padding(4);
            this.panelSpitting.Name = "panelSpitting";
            this.panelSpitting.Size = new System.Drawing.Size(638, 160);
            this.panelSpitting.TabIndex = 5;
            this.panelSpitting.Visible = false;
            // 
            // lbCurrentFileProgress
            // 
            this.lbCurrentFileProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrentFileProgress.Location = new System.Drawing.Point(24, 82);
            this.lbCurrentFileProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCurrentFileProgress.Name = "lbCurrentFileProgress";
            this.lbCurrentFileProgress.Size = new System.Drawing.Size(610, 29);
            this.lbCurrentFileProgress.TabIndex = 4;
            this.lbCurrentFileProgress.Text = "Actual file progress";
            // 
            // lbAllFilesProgress
            // 
            this.lbAllFilesProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAllFilesProgress.Location = new System.Drawing.Point(24, 21);
            this.lbAllFilesProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAllFilesProgress.Name = "lbAllFilesProgress";
            this.lbAllFilesProgress.Size = new System.Drawing.Size(600, 26);
            this.lbAllFilesProgress.TabIndex = 3;
            this.lbAllFilesProgress.Text = "All files progress";
            // 
            // progressBarFileSize
            // 
            this.progressBarFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarFileSize.BackColor = System.Drawing.Color.White;
            this.progressBarFileSize.ForeColor = System.Drawing.Color.Green;
            this.progressBarFileSize.Location = new System.Drawing.Point(27, 116);
            this.progressBarFileSize.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarFileSize.Name = "progressBarFileSize";
            this.progressBarFileSize.Size = new System.Drawing.Size(607, 26);
            this.progressBarFileSize.Step = 1;
            this.progressBarFileSize.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarFileSize.TabIndex = 2;
            this.progressBarFileSize.UseWaitCursor = true;
            // 
            // progressBarFiles
            // 
            this.progressBarFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarFiles.BackColor = System.Drawing.Color.White;
            this.progressBarFiles.ForeColor = System.Drawing.Color.Green;
            this.progressBarFiles.Location = new System.Drawing.Point(27, 51);
            this.progressBarFiles.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarFiles.Name = "progressBarFiles";
            this.progressBarFiles.Size = new System.Drawing.Size(607, 26);
            this.progressBarFiles.Step = 1;
            this.progressBarFiles.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarFiles.TabIndex = 1;
            this.progressBarFiles.UseWaitCursor = true;
            // 
            // lbSplitInfo
            // 
            this.lbSplitInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSplitInfo.Location = new System.Drawing.Point(0, 0);
            this.lbSplitInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSplitInfo.Name = "lbSplitInfo";
            this.lbSplitInfo.Size = new System.Drawing.Size(638, 21);
            this.lbSplitInfo.TabIndex = 0;
            this.lbSplitInfo.Text = "Procesing File .........";
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.Location = new System.Drawing.Point(17, 103);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(641, 161);
            this.lbInfo.TabIndex = 7;
            this.lbInfo.Text = "First select a file to split. Next select the part size and  push on \"Start\" to s" +
    "tart splitting the file in smaller parts";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(672, 273);
            this.Controls.Add(this.lbFile);
            this.Controls.Add(this.panelSpitting);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.grpSplitSize);
            this.Controls.Add(this.cmdSelectFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lbInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(580, 320);
            this.Name = "FrmSplitter";
            this.Text = "File Splitter";
            this.grpSplitSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            this.panelSpitting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button cmdSelectFile;
        private System.Windows.Forms.GroupBox grpSplitSize;
        private System.Windows.Forms.ComboBox cmbUnits;
        private System.Windows.Forms.NumericUpDown numSize;
        private System.Windows.Forms.Label lbEstimatedParts;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panelSpitting;
        private System.Windows.Forms.Label lbSplitInfo;
        private System.Windows.Forms.ProgressBar progressBarFiles;
        private System.Windows.Forms.ProgressBar progressBarFileSize;
        private System.Windows.Forms.Label lbCurrentFileProgress;
        private System.Windows.Forms.Label lbAllFilesProgress;
        private System.Windows.Forms.Label lbInfo;
    }
}

