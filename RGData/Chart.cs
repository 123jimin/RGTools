using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public class Chart {
        protected IList<TimingSegment> segments;
        public IList<TimingSegment> Segments { get => segments; }

        /// <summary>Length of this chart in ms.</summary>
        public double Length {
            get {
                double length = 0.0d;
                foreach(TimingSegment segment in segments) length += segment.Length;
                return length;
            }
        }

        public Chart() {
            segments = new List<TimingSegment>();
        }
        
        public Chart Add(TimingSegment segment) {
            segments.Add(segment);
            return this;
        }

        public void Simplify() {
            foreach (TimingSegment segment in segments) {
                segment.Simplify();
            }
        }
        
        /// <summary>Returns how many 4th notes exist in given time.</summary>
        /// <param name="ms">Time in ms</param>
        /// <returns>How many 4th notes from the beginning is</returns>
        public double GetQuadCount(double ms) {
            if (segments.Count == 0) return 0;
            if (ms <= 0) {
                // Undershoot
                TimingSegment firstSegment = segments.First();
                return ms / firstSegment.MSPQ;
            }
            // TODO: make this code more efficient
            double currentQuad = 0.0d;
            foreach (TimingSegment segment in segments) {
                double len = segment.Length;
                if(ms < len) {
                    return currentQuad + ms / segment.MSPQ;
                }
                currentQuad += segment.QuadCount;
                ms -= len;
            }
            // Overshoot
            TimingSegment lastSegment = segments.Last();
            return currentQuad + ms / lastSegment.MSPQ;
        }
    }
}
