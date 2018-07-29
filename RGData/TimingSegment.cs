using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public class TimingSegment {
        protected IList<Measure> measures;
        protected double qpm = 120;
        protected uint offset = 0;

        public IList<Measure> Measures { get => measures; }

        /// <summary>QPM for this segment</summary>
        public double QPM { get => qpm; set => qpm = value; }
        public double BPM { get => qpm; set => qpm = value; }
        /// <summary>ms per a quad note for this segment</summary>
        public double MSPQ { get => 60_000d / qpm; }
        /// <summary>ms per a whole note for this segment</summary>
        public double MSPW { get => 240_000d / qpm; }
        /// <summary>Time in ms which this timing segment actually starts</summary>
        public uint Offset { get => offset; set => offset = value; }

        /// <summary>Number of 4th notes in this segment</summary>
        public double QuadCount {
            get {
                double quads = 0.0d;
                foreach (Measure measure in measures) {
                    quads += measure.QuadLength;
                }
                return quads + offset / MSPQ;
            }
        }
        /// <summary>Number of whole notes in this segment</summary>
        public double WholeCount {
            get {
                double wholes = 0.0d;
                foreach (Measure measure in measures) {
                    wholes += measure.WholeLength;
                }
                return wholes + offset / MSPW;
            }
        }
        /// <summary>Length of this timing segment in ms</summary>
        public double Length {
            get {
                double wholes = 0.0d;
                // This is probably more accurate than using LengthOf.
                foreach (Measure measure in measures) {
                    wholes += measure.WholeLength;
                }
                return wholes * MSPW + offset;
            }
        }

        public TimingSegment(double qpm = 120.0d, uint offset = 0) {
            measures = new List<Measure>();
            this.qpm = qpm;
            this.offset = offset;
        }

        public TimingSegment Add(Measure measure) {
            measures.Add(measure);
            return this;
        }

        public void Simplify() {
            foreach(Measure measure in measures) {
                measure.Simplify();
            }
        }

        /// <summary>Computes the length of a measure in ms.</summary>
        /// <param name="measure">The measure which length will be measured.</param>
        /// <returns>Length of the measure in milliseconds.</returns>
        public double LengthOf(Measure measure) {
            return measure.WholeLength * MSPW;
        }

        /// <summary>Computes the length of a beat of a measure in ms.</summary>
        /// <param name="measure">The measure whose beat's length will be measured.</param>
        /// <returns>Length of a beat of the measure in milliseconds.</returns>
        public double BeatLengthOf(Measure measure) {
            return MSPW / measure.QuantBeat;
        }
    }
}
