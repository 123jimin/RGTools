using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData
{
    /// <summary>Represents measures with fixed time.</summary>
    public class OffsetMeasure: Measure {
        protected double length;

        /// <summary>Length of a measure in ms.</summary>
        public double UnitLength { get => length; }

        /// <summary>Length of these measures in ms.</summary>
        public double TotalLength { get => length * totalBeats; }

        /// <summary>Create new measures with fixed timespan.</summary>
        /// <param name="length">Length of a measure in ms.</param>
        /// <param name="count">Total amount of measures.</param>
        public OffsetMeasure(double length, int count = 1) {
            if (count <= 0) throw new ArgumentOutOfRangeException("# of measures should be more than zero!");
            this.length = length;
            totalBeats = count;
        }
    }
}
