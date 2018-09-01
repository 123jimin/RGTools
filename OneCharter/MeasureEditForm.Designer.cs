namespace OneCharter {
    partial class MeasureEditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblSpecifyLengthIn = new System.Windows.Forms.Label();
            this.cbMeasureType = new System.Windows.Forms.ComboBox();
            this.lblNumMeasures = new System.Windows.Forms.Label();
            this.inNumMeasures = new System.Windows.Forms.NumericUpDown();
            this.inGroupBeats = new System.Windows.Forms.NumericUpDown();
            this.inQuantBeat = new System.Windows.Forms.NumericUpDown();
            this.lblTimeSignature = new System.Windows.Forms.Label();
            this.lblLengthInMS = new System.Windows.Forms.Label();
            this.inLengthInMS = new System.Windows.Forms.TextBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.inNumMeasures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inGroupBeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inQuantBeat)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSpecifyLengthIn
            // 
            this.lblSpecifyLengthIn.AutoSize = true;
            this.lblSpecifyLengthIn.Location = new System.Drawing.Point(12, 9);
            this.lblSpecifyLengthIn.Name = "lblSpecifyLengthIn";
            this.lblSpecifyLengthIn.Size = new System.Drawing.Size(103, 12);
            this.lblSpecifyLengthIn.TabIndex = 0;
            this.lblSpecifyLengthIn.Text = "Specify length in:";
            // 
            // cbMeasureType
            // 
            this.cbMeasureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMeasureType.FormattingEnabled = true;
            this.cbMeasureType.Items.AddRange(new object[] {
            "milliseconds",
            "# of beats"});
            this.cbMeasureType.Location = new System.Drawing.Point(129, 6);
            this.cbMeasureType.Name = "cbMeasureType";
            this.cbMeasureType.Size = new System.Drawing.Size(124, 20);
            this.cbMeasureType.TabIndex = 1;
            this.cbMeasureType.SelectedIndexChanged += new System.EventHandler(this.cbMeasureType_SelectedIndexChanged);
            // 
            // lblNumMeasures
            // 
            this.lblNumMeasures.AutoSize = true;
            this.lblNumMeasures.Location = new System.Drawing.Point(12, 46);
            this.lblNumMeasures.Name = "lblNumMeasures";
            this.lblNumMeasures.Size = new System.Drawing.Size(90, 12);
            this.lblNumMeasures.TabIndex = 2;
            this.lblNumMeasures.Text = "# of measures:";
            // 
            // inNumMeasures
            // 
            this.inNumMeasures.Location = new System.Drawing.Point(129, 44);
            this.inNumMeasures.Name = "inNumMeasures";
            this.inNumMeasures.Size = new System.Drawing.Size(45, 21);
            this.inNumMeasures.TabIndex = 3;
            // 
            // inGroupBeats
            // 
            this.inGroupBeats.Location = new System.Drawing.Point(129, 118);
            this.inGroupBeats.Name = "inGroupBeats";
            this.inGroupBeats.Size = new System.Drawing.Size(45, 21);
            this.inGroupBeats.TabIndex = 4;
            this.inGroupBeats.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // inQuantBeat
            // 
            this.inQuantBeat.Location = new System.Drawing.Point(129, 145);
            this.inQuantBeat.Name = "inQuantBeat";
            this.inQuantBeat.Size = new System.Drawing.Size(45, 21);
            this.inQuantBeat.TabIndex = 5;
            this.inQuantBeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTimeSignature
            // 
            this.lblTimeSignature.AutoSize = true;
            this.lblTimeSignature.Location = new System.Drawing.Point(12, 136);
            this.lblTimeSignature.Name = "lblTimeSignature";
            this.lblTimeSignature.Size = new System.Drawing.Size(94, 12);
            this.lblTimeSignature.TabIndex = 6;
            this.lblTimeSignature.Text = "Time signature:";
            // 
            // lblLengthInMS
            // 
            this.lblLengthInMS.AutoSize = true;
            this.lblLengthInMS.Location = new System.Drawing.Point(12, 86);
            this.lblLengthInMS.Name = "lblLengthInMS";
            this.lblLengthInMS.Size = new System.Drawing.Size(93, 12);
            this.lblLengthInMS.TabIndex = 7;
            this.lblLengthInMS.Text = "Length (in ms):";
            // 
            // inLengthInMS
            // 
            this.inLengthInMS.Location = new System.Drawing.Point(111, 83);
            this.inLengthInMS.Name = "inLengthInMS";
            this.inLengthInMS.Size = new System.Drawing.Size(63, 21);
            this.inLengthInMS.TabIndex = 8;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(194, 118);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(59, 21);
            this.btnInsert.TabIndex = 9;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(194, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 21);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // MeasureEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 172);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.inLengthInMS);
            this.Controls.Add(this.lblLengthInMS);
            this.Controls.Add(this.lblTimeSignature);
            this.Controls.Add(this.inQuantBeat);
            this.Controls.Add(this.inGroupBeats);
            this.Controls.Add(this.inNumMeasures);
            this.Controls.Add(this.lblNumMeasures);
            this.Controls.Add(this.cbMeasureType);
            this.Controls.Add(this.lblSpecifyLengthIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MeasureEditForm";
            this.Text = "Create/Edit a measure...";
            ((System.ComponentModel.ISupportInitialize)(this.inNumMeasures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inGroupBeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inQuantBeat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSpecifyLengthIn;
        private System.Windows.Forms.ComboBox cbMeasureType;
        private System.Windows.Forms.Label lblNumMeasures;
        private System.Windows.Forms.NumericUpDown inNumMeasures;
        private System.Windows.Forms.NumericUpDown inGroupBeats;
        private System.Windows.Forms.NumericUpDown inQuantBeat;
        private System.Windows.Forms.Label lblTimeSignature;
        private System.Windows.Forms.Label lblLengthInMS;
        private System.Windows.Forms.TextBox inLengthInMS;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnCancel;
    }
}