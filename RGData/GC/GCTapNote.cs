using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData.GC {
    /// <summary>
    /// A normal note for GrooveCoaster
    /// </summary>
    public class GCTapNote : GCShortNote {
        public GCTapNote() {}
        public override bool OccupiesSamePlace(Element other) {
            if (!(other is Note)) return false;
            return true;
        }
    }
}
