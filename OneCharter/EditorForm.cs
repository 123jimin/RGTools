using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RGData;

namespace OneCharter {
    public partial class EditorForm : Form {
        private EditView editView;
        private MeasureEditForm measureEditForm;

        private static readonly Func<Element>[] GCShortkeyElements = {
            () => null,
            () => new RGData.GC.GCTapNote(0),
            () => new RGData.GC.GCDualTapNote(0)
        };

        public EditorForm() {
            InitializeComponent();
            measureEditForm = new MeasureEditForm();

            KeyPreview = true;

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            editView = new EditView(this);
            editView.StartPlaying += EditView_StartPlaying;
            editView.StopPlaying += EditView_StopPlaying;
        }


        public void TogglePlay() {
            if (editView.IsPlaying) {
                editView.Pause();
            } else {
                editView.Play();
            }
        }

        private Measure GetANewMeasure() {
            measureEditForm.ShowDialog(this);
            throw new NotImplementedException();
        }

        public void TryCreateMeasure() {
            // If the cursor is at the boundary, create a new measure.
        }

        #region Events
        private void EditView_StopPlaying(object sender, EventArgs e) {
            playStopButton.Image = Properties.Resources.IconPlay;
            playStopButton.Text = "Play";
        }

        private void EditView_StartPlaying(object sender, EventArgs e) {
            playStopButton.Image = Properties.Resources.IconPause;
            playStopButton.Text = "Pause";
        }

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
            if(Keys.D0 <= e.KeyCode && e.KeyCode <= Keys.D9) {
                int index = e.KeyCode - Keys.D0;
                if(index < GCShortkeyElements.Length) {
                    Element element = GCShortkeyElements[index]();
                    if (element != null) editView.AddElement(element);
                    editView.MoveFuture();
                    return;
                }
            }
            switch (e.KeyCode) {
                case HotKey.TogglePlay:
                    TogglePlay();
                    break;
                case HotKey.Delete:
                    editView.RemoveElementsAtCursor();
                    break;
                case HotKey.DeleteAndGoBack:
                    editView.RemoveElementsAtCursor();
                    editView.MovePast();
                    break;
                case Keys.Down:
                    editView.MovePast();
                    break;
                case Keys.Up:
                    editView.MoveFuture();
                    break;
            }
        }

        private void deleteCurrentItemToolStripMenuItem_Click(object sender, EventArgs e) {
            editView.RemoveElementsAtCursor();
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

        private void measureToolStripMenuItem_Click(object sender, EventArgs e) {
            TryCreateMeasure();
        }
        #endregion

    }
}
