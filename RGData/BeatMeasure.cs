using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public class BeatMeasure: Measure {
        protected int quantBeat = 1;

        /// <summary>Unit beat size</summary>
        public int QuantBeat { get => quantBeat; set => Fine(value); }

        /// <summary>Amount of 4th notes in this measure.</summary>
        public double QuadLength { get => totalBeats * 4.0d / quantBeat; }
        /// <summary>Amount of whole(1st) notes in this measure.</summary>
        public double WholeLength { get => (double) (totalBeats) / quantBeat; }

        public BeatMeasure(int quantBeat = 4): this(quantBeat, quantBeat) {}
        public BeatMeasure(int quantBeat, int groupBeats): this(quantBeat, groupBeats, groupBeats) {}
        public BeatMeasure(int quantBeat, int groupBeats, int totalBeats) {
            this.quantBeat = quantBeat;
            this.groupBeats = groupBeats;
            Extend(totalBeats);
        }

        internal void Fine(int adjustQuant) {
            if (adjustQuant == quantBeat) return;
            int multiplier = adjustQuant / Util.GCD(quantBeat, adjustQuant);
            SortedList<int, ISet<Element>> newList = new SortedList<int, ISet<Element>>();
            foreach (var pair in elements) {
                newList.Add(pair.Key * multiplier, pair.Value);
            }
            elements = newList;
            quantBeat *= multiplier;
            groupBeats *= multiplier;
            totalBeats *= multiplier;
        }

        internal void Coarsen() {
            int divider = Util.GCD(quantBeat, groupBeats);
            divider = Util.GCD(divider, totalBeats);
            if (divider == 1) return;
            foreach (int time in elements.Keys) {
                divider = Util.GCD(divider, time);
                if (divider == 1) return;
            }

            quantBeat /= divider;
            groupBeats /= divider;
            totalBeats /= divider;

            SortedList<int, ISet<Element>> newList = new SortedList<int, ISet<Element>>();
            foreach (var pair in elements) {
                newList.Add(pair.Key / divider, pair.Value);
            }
            elements = newList;
        }
    }
}
