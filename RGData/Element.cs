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

        /// <summary>Returns whether given two elements cannot occur at the same time.</summary>
        /// <param name="other">The other element.</param>
        /// <returns>Whether this element and the other element occupies same space.</returns>
        public abstract bool OccupiesSamePlace(Element other);
        public static bool Collides(Element a, Element b) {
            if (a is Note != b is Note) return false;
            return a.OccupiesSamePlace(b) || b.OccupiesSamePlace(a);
        }
    }
}
