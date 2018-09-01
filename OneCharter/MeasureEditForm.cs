using RGData;
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
    public partial class MeasureEditForm : Form {
        protected enum MeasureType {
            OFFSET = 0,
            BEAT = 1
        }

        /// <summary>false: creating, true: editing</summary>
        protected bool isEditing;
        public bool IsEditing {
            get => isEditing;
        }

        /// <summary>The measure which is being edited.</summary>
        protected Measure measure;
        public Measure Measure {
            get => measure;
            set {
                // Just in case.
                DialogResult = DialogResult.Cancel;
                if(value == null) {
                    // Create a new measure.
                    isEditing = false;
                    measure = CreateDefaultMeasure();
                } else {
                    // Editing this measure... fill the values
                    isEditing = true;
                    measure = value;
                }
                // Fill the values based on the measure.
                inNumMeasures.Value = measure.TotalBeats / measure.GroupBeats;
                if(measure is BeatMeasure bMeasure) {
                    cbMeasureType.SelectedIndex = (int) MeasureType.BEAT;

                    inQuantBeat.Value = bMeasure.QuantBeat;
                    inGroupBeats.Value = bMeasure.GroupBeats;
                }else if(measure is OffsetMeasure oMeasure) {
                    cbMeasureType.SelectedIndex = (int) MeasureType.OFFSET;

                    inLengthInMS.Text = oMeasure.UnitLength.ToString();
                } else {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>Creates and returns the default measure.</summary>
        /// <returns>The default measure object.</returns>
        private Measure CreateDefaultMeasure() {
            return new BeatMeasure(4);
        }

        public MeasureEditForm() {
            InitializeComponent();
            Measure = null;
        }

        /// <summary>
        /// Apply changes made using GUI to the measure.
        /// </summary>
        /// <returns>Whether the input is valid.</returns>
        protected bool ApplyChanges() {
            if (isEditing) throw new NotImplementedException();
            switch((MeasureType) cbMeasureType.SelectedIndex) {
                case MeasureType.OFFSET:
                    try {
                        double d = double.Parse(inLengthInMS.Text);
                        measure = new OffsetMeasure(d, (int) inNumMeasures.Value);
                    } catch {
                        return false;
                    }
                    break;
                case MeasureType.BEAT:
                    try {
                        measure = new BeatMeasure(
                            (int) inQuantBeat.Value,
                            (int) inGroupBeats.Value,
                            (int) inGroupBeats.Value * (int) inNumMeasures.Value);
                    } catch {
                        return false;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            return true;
        }

        private void cbMeasureType_SelectedIndexChanged(object sender, EventArgs e) {
            inLengthInMS.Enabled = inQuantBeat.Enabled = inGroupBeats.Enabled = false;
            switch ((MeasureType) cbMeasureType.SelectedIndex) {
                case MeasureType.OFFSET:
                    inLengthInMS.Enabled = true;
                    break;
                case MeasureType.BEAT:
                    inQuantBeat.Enabled = inGroupBeats.Enabled = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e) {
            if (ApplyChanges()) {
                DialogResult = DialogResult.OK;
            }
            Close();
        }
    }
}
