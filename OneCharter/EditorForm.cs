using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneCharter {
    public partial class EditorForm : Form {
        private EditView editView;

        public EditorForm() {
            InitializeComponent();
            KeyPreview = true;

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            editView = new EditView(this);
            editView.StartPlaying += EditView_StartPlaying;
            editView.StopPlaying += EditView_StopPlaying;
        }

        private void EditView_StopPlaying(object sender, EventArgs e) {
            playStopButton.Image = Properties.Resources.IconPlay;
            playStopButton.Text = "Play";
        }

        private void EditView_StartPlaying(object sender, EventArgs e) {
            playStopButton.Image = Properties.Resources.IconPause;
            playStopButton.Text = "Pause";
        }

        public void TogglePlay() {
            if (editView.IsPlaying) {
                editView.Pause();
            } else {
                editView.Play();
            }
        }

        #region Events
        private void aboutOneCharterToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void EditorForm_Resize(object sender, EventArgs e) {
            // editorPanel.Refresh();
        }

        private void playStopButton_Click(object sender, EventArgs e) {
            TogglePlay();
        }

        private void EditorForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case HotKey.TogglePlay:
                    TogglePlay();
                    break;
                case HotKey.Delete:

                    break;
                case Keys.Down:
                    editView.MovePast();
                    break;
                case Keys.Up:
                    editView.MoveFuture();
                    break;
            }
        }

        private void EditorForm_Load(object sender, EventArgs e) {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            // TODO: Ask to save an unsaved file
            Application.Exit();
        }

        private void tapToolStripMenuItem_Click(object sender, EventArgs e) {
            editView.AddElement(new RGData.GC.GCTapNote(0));
        }

        private void dualTapToolStripMenuItem_Click(object sender, EventArgs e) {
            editView.AddElement(new RGData.GC.GCDualTapNote(0));
        }
        #endregion
    }
}
