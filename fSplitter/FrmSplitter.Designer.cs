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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.lbFile.AutoSize = true;
            this.lbFile.Location = new System.Drawing.Point(16, 11);
            this.lbFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(30, 17);
            this.lbFile.TabIndex = 0;
            this.lbFile.Text = "File";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(55, 7);
            this.txtFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(320, 22);
            this.txtFile.TabIndex = 1;
            // 
            // cmdSelectFile
            // 
            this.cmdSelectFile.Location = new System.Drawing.Point(391, 5);
            this.cmdSelectFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdSelectFile.Name = "cmdSelectFile";
            this.cmdSelectFile.Size = new System.Drawing.Size(49, 28);
            this.cmdSelectFile.TabIndex = 2;
            this.cmdSelectFile.Text = "...";
            this.cmdSelectFile.UseVisualStyleBackColor = true;
            this.cmdSelectFile.Click += new System.EventHandler(this.cmdSelectFile_Click);
            // 
            // grpSplitSize
            // 
            this.grpSplitSize.Controls.Add(this.lbEstimatedParts);
            this.grpSplitSize.Controls.Add(this.cmbUnits);
            this.grpSplitSize.Controls.Add(this.numSize);
            this.grpSplitSize.Location = new System.Drawing.Point(20, 39);
            this.grpSplitSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSplitSize.Name = "grpSplitSize";
            this.grpSplitSize.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSplitSize.Size = new System.Drawing.Size(328, 57);
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
            this.lbEstimatedParts.Size = new System.Drawing.Size(139, 25);
            this.lbEstimatedParts.TabIndex = 2;
            this.lbEstimatedParts.Tag = "{0} parts of";
            this.lbEstimatedParts.Text = "0 parts of";
            this.lbEstimatedParts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUnits
            // 
            this.cmbUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnits.FormattingEnabled = true;
            this.cmbUnits.Items.AddRange(new object[] {
            "b",
            "Kb",
            "Mb",
            "Gb",
            "Lines"});
            this.cmbUnits.Location = new System.Drawing.Point(245, 20);
            this.cmbUnits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Size = new System.Drawing.Size(73, 24);
            this.cmbUnits.TabIndex = 1;
            this.cmbUnits.SelectedIndexChanged += new System.EventHandler(this.controlValueChangedEvent);
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(155, 20);
            this.numSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.cmdStart.Enabled = false;
            this.cmdStart.Location = new System.Drawing.Point(356, 49);
            this.cmdStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(84, 41);
            this.cmdStart.TabIndex = 4;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.lbStart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "log";
            this.openFileDialog.Filter = "All Files|*.*";
            // 
            // panelSpitting
            // 
            this.panelSpitting.Controls.Add(this.label2);
            this.panelSpitting.Controls.Add(this.label1);
            this.panelSpitting.Controls.Add(this.progressBarFileSize);
            this.panelSpitting.Controls.Add(this.progressBarFiles);
            this.panelSpitting.Controls.Add(this.lbSplitInfo);
            this.panelSpitting.Location = new System.Drawing.Point(16, 103);
            this.panelSpitting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSpitting.Name = "panelSpitting";
            this.panelSpitting.Size = new System.Drawing.Size(420, 98);
            this.panelSpitting.TabIndex = 5;
            this.panelSpitting.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Actual file progress";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "All files progress";
            // 
            // progressBarFileSize
            // 
            this.progressBarFileSize.BackColor = System.Drawing.Color.White;
            this.progressBarFileSize.ForeColor = System.Drawing.Color.Green;
            this.progressBarFileSize.Location = new System.Drawing.Point(4, 78);
            this.progressBarFileSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarFileSize.Name = "progressBarFileSize";
            this.progressBarFileSize.Size = new System.Drawing.Size(409, 15);
            this.progressBarFileSize.Step = 1;
            this.progressBarFileSize.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarFileSize.TabIndex = 2;
            this.progressBarFileSize.UseWaitCursor = true;
            // 
            // progressBarFiles
            // 
            this.progressBarFiles.BackColor = System.Drawing.Color.White;
            this.progressBarFiles.ForeColor = System.Drawing.Color.Green;
            this.progressBarFiles.Location = new System.Drawing.Point(4, 39);
            this.progressBarFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarFiles.Name = "progressBarFiles";
            this.progressBarFiles.Size = new System.Drawing.Size(409, 15);
            this.progressBarFiles.Step = 1;
            this.progressBarFiles.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarFiles.TabIndex = 1;
            this.progressBarFiles.UseWaitCursor = true;
            // 
            // lbSplitInfo
            // 
            this.lbSplitInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbSplitInfo.Location = new System.Drawing.Point(0, 0);
            this.lbSplitInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSplitInfo.Name = "lbSplitInfo";
            this.lbSplitInfo.Size = new System.Drawing.Size(420, 17);
            this.lbSplitInfo.TabIndex = 0;
            this.lbSplitInfo.Tag = "Splitting file {0}";
            this.lbSplitInfo.Text = "Splitting file {0}";
            // 
            // lbInfo
            // 
            this.lbInfo.Location = new System.Drawing.Point(17, 103);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(424, 98);
            this.lbInfo.TabIndex = 7;
            this.lbInfo.Text = "First select a file to split. Next select the part size and  push on \"Start\" to s" +
    "tart splitting the file in smaller parts";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 218);
            this.Controls.Add(this.panelSpitting);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.grpSplitSize);
            this.Controls.Add(this.cmdSelectFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.lbFile);
            this.Controls.Add(this.lbInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSplitter";
            this.Text = "File Splitter";
            this.grpSplitSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            this.panelSpitting.ResumeLayout(false);
            this.panelSpitting.PerformLayout();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbInfo;
    }
}

