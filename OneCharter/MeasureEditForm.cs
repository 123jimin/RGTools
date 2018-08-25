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
                measure = value;
                if(measure == null) {
                    // Create a new measure.
                    isEditing = false;
                    // TODO: create a new default measure
                } else {
                    // Editing this measure... fill the values
                    isEditing = true;
                    // TODO: fill the values of controllers.
                }
            }
        }
        public MeasureEditForm() {
            InitializeComponent();
            cbMeasureType.SelectedIndex = 1;
        }

        private void cbMeasureType_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
