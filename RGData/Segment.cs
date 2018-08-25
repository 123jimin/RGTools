using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public class Segment {
        protected IList<Measure> measures;
        protected double qpm = 120;

        // TODO: scroll speed
        protected double speed = 1.0d;

        public IList<Measure> Measures { get => measures; }

        /// <summary>QPM (quarters per minute) for this segment</summary>
        public double QPM { get => qpm; set => qpm = value; }
        /// <summary>BPM (actually quarters per minute) for this segment</summary>
        public double BPM { get => qpm; set => qpm = value; }
        /// <summary>ms per a quad note for this segment</summary>
        public double MSPQ { get => 60_000d / qpm; }
        /// <summary>ms per a whole note for this segment</summary>
        public double MSPW { get => 240_000d / qpm; }

        /// <summary>Number of 4th notes in this segment</summary>
        public double QuadCount {
            get => 4 * WholeCount;
        }
        /// <summary>Number of whole notes in this segment</summary>
        public double WholeCount {
            get {
                double wholes = 0.0d;
                foreach (Measure measure in measures) {
                    switch (measure) {
                        case BeatMeasure bMeasure:
                            wholes += bMeasure.WholeLength;
                            break;
                        case OffsetMeasure oMeasure:
                            wholes += oMeasure.TotalLength / MSPW;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                return wholes;
            }
        }

        /// <summary>Length of this segment, in ms</summary>
        public double Length {
            get => WholeCount * MSPW;
        }

        public Segment(double qpm = 120.0d) {
            measures = new List<Measure>();
            this.qpm = qpm;
        }

        internal void Append(Measure measure) {
            measures.Add(measure);
        }

        /// <summary>Computes the length of a measure object in ms.</summary>
        /// <param name="measure">The measure which length will be measured.</param>
        /// <returns>Length of the measure in milliseconds.</returns>
        public double LengthOf(Measure measure) {
            switch (measure) {
                case BeatMeasure bMeasure:
                    return bMeasure.WholeLength * MSPW;
                case OffsetMeasure oMeasure:
                    return oMeasure.TotalLength;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>Computes the length of a measure object in ms.</summary>
        /// <param name="measureIndex">Index to the measure.</param>
        /// <returns>Length of the measure in milliseconds.</returns>
        public double LengthOf(int measureIndex) {
            return LengthOf(measures[measureIndex]);
        }

        /// <summary>Computes the length of a beat of a measure object in ms.</summary>
        /// <param name="measure">The measure whose beat's length will be measured.</param>
        /// <returns>Length of a beat of the measure in milliseconds.</returns>
        public double BeatLengthOf(Measure measure) {
            switch (measure) {
                case BeatMeasure bMeasure:
                    return MSPW / bMeasure.QuantBeat;
                case OffsetMeasure oMeasure:
                    return oMeasure.UnitLength;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>Computes the length of a beat of a measure in ms.</summary>
        /// <param name="measureIndex">Index to the measure.</param>
        /// <returns>Length of a beat of the measure in milliseconds.</returns>
        public double BeatLengthOf(int measureIndex) {
            return BeatLengthOf(measures[measureIndex]);
        }

        /// <summary>Computes the amount of quads in a measure object in ms.</summary>
        /// <param name="measure">The measure object which length will be measured.</param>
        /// <returns>Amount of quads in the given measure object.</returns>
        public double QuadLengthOf(Measure measure) {
            switch (measure) {
                case BeatMeasure bMeasure:
                    return bMeasure.QuadLength;
                case OffsetMeasure oMeasure:
                    return oMeasure.TotalLength / MSPQ;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>Computes the amount of quads in a measure object in ms.</summary>
        /// <param name="measureIndex">Index to the measure.</param>
        /// <returns>Amount of quads in the given measure object.</returns>
        public double QuadLengthOf(int measureIndex) {
            return QuadLengthOf(measures[measureIndex]);
        }
    }
}
