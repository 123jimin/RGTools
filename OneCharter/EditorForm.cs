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

        /// <summary>Map from int (numpad key) to GC elements (to insert)</summary>
        private static readonly Func<Element>[] GCShortkeyElements = {
            () => null,
            () => new RGData.GC.GCTapNote(),
            () => new RGData.GC.GCDualTapNote()
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

        /// <summary>Toggles play/stop of the editor.</summary>
        public void TogglePlay() {
            if (editView.IsPlaying) {
                editView.Pause();
            } else {
                editView.Play();
            }
        }

        /// <summary>Shows the measure creation dialogue, then returns the measure.</summary>
        /// <returns>A new measure, or null if the user decided not to create a new measure.</returns>
        private Measure GetANewMeasure() {
            // Sets the measureEditForm to create mode.
            measureEditForm.Measure = null;
            // Shows the dialog
            DialogResult result = measureEditForm.ShowDialog(this);
            return measureEditForm.Measure;
        }

        public void TryCreateMeasure() {
            Measure measure = GetANewMeasure();
            if (measure == null) return;

            editView.InsertMeasure(measure);
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
                    editView.NextBeat();
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
                    editView.PrevBeat();
                    break;
                case Keys.Down:
                    editView.PrevBeat();
                    break;
                case Keys.Up:
                    editView.NextBeat();
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
            editView.AddElement(new RGData.GC.GCTapNote());
        }

        private void dualTapToolStripMenuItem_Click(object sender, EventArgs e) {
            editView.AddElement(new RGData.GC.GCDualTapNote());
        }

        private void measureToolStripMenuItem_Click(object sender, EventArgs e) {
            TryCreateMeasure();
        }
        #endregion

    }
}
