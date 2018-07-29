using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    /// <summary>
    /// An abstract class representing long(hold) notes.
    /// </summary>
    public abstract class LongNote: Note {
        public LongNote(int beginTime, int endTime): base(beginTime) {

        }
    }
}
