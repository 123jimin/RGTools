namespace OneCharter {
    partial class EditorForm {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.measureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timingSegmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.grooveCoasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dualTapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dualFlickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adlibToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.holdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scratchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slideHoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dualHoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.deleteCurrentItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playStopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutOneCharterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.timeStatLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton4,
            this.playStopButton,
            this.toolStripDropDownButton3});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(327, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::OneCharter.Properties.Resources.IconFile;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(54, 22);
            this.toolStripDropDownButton2.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::OneCharter.Properties.Resources.IconFileAdd;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::OneCharter.Properties.Resources.IconUpload;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::OneCharter.Properties.Resources.IconDownload;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::OneCharter.Properties.Resources.IconExit;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.measureToolStripMenuItem,
            this.timingSegmentToolStripMenuItem,
            this.toolStripSeparator3,
            this.grooveCoasterToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::OneCharter.Properties.Resources.IconPlus;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(65, 22);
            this.toolStripDropDownButton1.Text = "&Insert";
            // 
            // measureToolStripMenuItem
            // 
            this.measureToolStripMenuItem.Name = "measureToolStripMenuItem";
            this.measureToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.measureToolStripMenuItem.Text = "Measure";
            this.measureToolStripMenuItem.Click += new System.EventHandler(this.measureToolStripMenuItem_Click);
            // 
            // timingSegmentToolStripMenuItem
            // 
            this.timingSegmentToolStripMenuItem.Name = "timingSegmentToolStripMenuItem";
            this.timingSegmentToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.timingSegmentToolStripMenuItem.Text = "Timing Segment";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(160, 6);
            // 
            // grooveCoasterToolStripMenuItem
            // 
            this.grooveCoasterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tapToolStripMenuItem,
            this.dualTapToolStripMenuItem,
            this.flickToolStripMenuItem,
            this.dualFlickToolStripMenuItem,
            this.adlibToolStripMenuItem,
            this.toolStripSeparator2,
            this.holdToolStripMenuItem,
            this.beatToolStripMenuItem,
            this.scratchToolStripMenuItem,
            this.slideHoldToolStripMenuItem,
            this.dualHoldToolStripMenuItem});
            this.grooveCoasterToolStripMenuItem.Name = "grooveCoasterToolStripMenuItem";
            this.grooveCoasterToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.grooveCoasterToolStripMenuItem.Text = "GrooveCoaster";
            // 
            // tapToolStripMenuItem
            // 
            this.tapToolStripMenuItem.Name = "tapToolStripMenuItem";
            this.tapToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.tapToolStripMenuItem.Text = "Tap";
            this.tapToolStripMenuItem.Click += new System.EventHandler(this.tapToolStripMenuItem_Click);
            // 
            // dualTapToolStripMenuItem
            // 
            this.dualTapToolStripMenuItem.Name = "dualTapToolStripMenuItem";
            this.dualTapToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.dualTapToolStripMenuItem.Text = "Dual Tap";
            this.dualTapToolStripMenuItem.Click += new System.EventHandler(this.dualTapToolStripMenuItem_Click);
            // 
            // flickToolStripMenuItem
            // 
            this.flickToolStripMenuItem.Name = "flickToolStripMenuItem";
            this.flickToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.flickToolStripMenuItem.Text = "Flick";
            // 
            // dualFlickToolStripMenuItem
            // 
            this.dualFlickToolStripMenuItem.Name = "dualFlickToolStripMenuItem";
            this.dualFlickToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.dualFlickToolStripMenuItem.Text = "Dual Flick";
            // 
            // adlibToolStripMenuItem
            // 
            this.adlibToolStripMenuItem.Name = "adlibToolStripMenuItem";
            this.adlibToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.adlibToolStripMenuItem.Text = "Ad-lib";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(127, 6);
            // 
            // holdToolStripMenuItem
            // 
            this.holdToolStripMenuItem.Name = "holdToolStripMenuItem";
            this.holdToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.holdToolStripMenuItem.Text = "Hold";
            // 
            // beatToolStripMenuItem
            // 
            this.beatToolStripMenuItem.Name = "beatToolStripMenuItem";
            this.beatToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.beatToolStripMenuItem.Text = "Beat";
            // 
            // scratchToolStripMenuItem
            // 
            this.scratchToolStripMenuItem.Name = "scratchToolStripMenuItem";
            this.scratchToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.scratchToolStripMenuItem.Text = "Scratch";
            // 
            // slideHoldToolStripMenuItem
            // 
            this.slideHoldToolStripMenuItem.Name = "slideHoldToolStripMenuItem";
            this.slideHoldToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.slideHoldToolStripMenuItem.Text = "Slide Hold";
            // 
            // dualHoldToolStripMenuItem
            // 
            this.dualHoldToolStripMenuItem.Name = "dualHoldToolStripMenuItem";
            this.dualHoldToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.dualHoldToolStripMenuItem.Text = "Dual Hold";
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteCurrentItemToolStripMenuItem});
            this.toolStripDropDownButton4.Image = global::OneCharter.Properties.Resources.IconPencil;
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(56, 22);
            this.toolStripDropDownButton4.Text = "&Edit";
            // 
            // deleteCurrentItemToolStripMenuItem
            // 
            this.deleteCurrentItemToolStripMenuItem.Image = global::OneCharter.Properties.Resources.IconEraser;
            this.deleteCurrentItemToolStripMenuItem.Name = "deleteCurrentItemToolStripMenuItem";
            this.deleteCurrentItemToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.deleteCurrentItemToolStripMenuItem.Text = "Delete current item";
            this.deleteCurrentItemToolStripMenuItem.Click += new System.EventHandler(this.deleteCurrentItemToolStripMenuItem_Click);
            // 
            // playStopButton
            // 
            this.playStopButton.Image = global::OneCharter.Properties.Resources.IconPlay;
            this.playStopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playStopButton.Name = "playStopButton";
            this.playStopButton.Size = new System.Drawing.Size(49, 22);
            this.playStopButton.Text = "Play";
            this.playStopButton.Click += new System.EventHandler(this.playStopButton_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutOneCharterToolStripMenuItem});
            this.toolStripDropDownButton3.Image = global::OneCharter.Properties.Resources.IconQuestion;
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(61, 22);
            this.toolStripDropDownButton3.Text = "&Help";
            // 
            // aboutOneCharterToolStripMenuItem
            // 
            this.aboutOneCharterToolStripMenuItem.Image = global::OneCharter.Properties.Resources.IconInfo;
            this.aboutOneCharterToolStripMenuItem.Name = "aboutOneCharterToolStripMenuItem";
            this.aboutOneCharterToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutOneCharterToolStripMenuItem.Text = "About OneCharter";
            this.aboutOneCharterToolStripMenuItem.Click += new System.EventHandler(this.aboutOneCharterToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeStatLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 553);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(327, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // timeStatLabel
            // 
            this.timeStatLabel.Name = "timeStatLabel";
            this.timeStatLabel.Size = new System.Drawing.Size(66, 17);
            this.timeStatLabel.Text = "(Time Stat)";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 575);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Name = "EditorForm";
            this.Text = "OneCharter";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EditorForm_KeyDown);
            this.Resize += new System.EventHandler(this.EditorForm_Resize);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton playStopButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel timeStatLabel;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grooveCoasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dualTapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dualFlickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adlibToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem holdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scratchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slideHoldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dualHoldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem measureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timingSegmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem aboutOneCharterToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem deleteCurrentItemToolStripMenuItem;
    }
}

