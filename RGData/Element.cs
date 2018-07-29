using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RGData {
    /// <summary>
    /// An abstract class representing things on a chart.
    /// </summary>
    public abstract class Element {
        protected int beatTime;

        /// <summary>Timing of this element (in relative to the measure this element is in)</summary>
        public int BeatTime { get => beatTime; set => beatTime = value; }

        public Element(int beatTime) {
            this.beatTime = beatTime;
        }
    }
}
