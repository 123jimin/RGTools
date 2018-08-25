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
            this.label1 = new System.Windows.Forms.Label();
            this.cbMeasureType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Measure Size:";
            // 
            // cbMeasureType
            // 
            this.cbMeasureType.FormattingEnabled = true;
            this.cbMeasureType.Items.AddRange(new object[] {
            "in milliseconds",
            "in # of beats"});
            this.cbMeasureType.Location = new System.Drawing.Point(150, 6);
            this.cbMeasureType.Name = "cbMeasureType";
            this.cbMeasureType.Size = new System.Drawing.Size(132, 20);
            this.cbMeasureType.TabIndex = 1;
            this.cbMeasureType.SelectedIndexChanged += new System.EventHandler(this.cbMeasureType_SelectedIndexChanged);
            // 
            // MeasureEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 261);
            this.Controls.Add(this.cbMeasureType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MeasureEditForm";
            this.Text = "Create/Edit a measure...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMeasureType;
    }
}