using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public class Measure {
        protected int quantBeat;
        protected int groupBeats;
        protected int totalBeats;

        /// <summary>Unit beat size</summary>
        public int QuantBeat { get => quantBeat; set => Fine(value); }
        /// <summary>Total length of beats</summary>
        public int TotalBeats { get => totalBeats; }
        /// <summary>Number of beats per a measure</summary>
        public int GroupBeats { get => groupBeats; }

        /// <summary>Number of 4th notes in this measure.</summary>
        public double QuadLength { get => totalBeats * 4.0d / quantBeat; }
        /// <summary>Number of whole(1st) notes in this measure.</summary>
        public double WholeLength { get => (double) (totalBeats) / quantBeat; }


        protected SortedList<int, ISet<Element>> elements;
        // public IList<ISet<Element>> Elements { get => elements.Values; }
        public IList<Element> Elements {
            get {
                List<Element> v = new List<Element>();
                foreach(ISet<Element> set in elements.Values) v.AddRange(set);
                return v;
            }
        }
        public IList<int> Beats { get => elements.Keys; }

        public Measure(int quantBeat = 4): this(quantBeat, quantBeat) {}
        public Measure(int quantBeat, int groupBeats): this(quantBeat, groupBeats, groupBeats) {}
        public Measure(int quantBeat, int groupBeats, int totalBeats) {
            elements = new SortedList<int, ISet<Element>>();

            if(totalBeats % groupBeats > 0) {
                totalBeats -= totalBeats % groupBeats;
                totalBeats += groupBeats;
            }
            this.quantBeat = quantBeat;
            this.groupBeats = groupBeats;
            this.totalBeats = totalBeats;
        }

        public void Fine(int adjustQuant) {
            if (adjustQuant == quantBeat) return;
            int multiplier = adjustQuant / Util.GCD(quantBeat, adjustQuant);
            SortedList<int, ISet<Element>> newList = new SortedList<int, ISet<Element>>();
            foreach (var pair in elements) {
                ISet<Element> set = pair.Value;
                foreach(Element element in set) {
                    element.BeatTime *= multiplier;
                }
                newList.Add(pair.Key * multiplier, set);
            }
            elements = newList;
            quantBeat *= multiplier;
            groupBeats *= multiplier;
            totalBeats *= multiplier;
        }

        public void Coarsen() {
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
                ISet<Element> set = pair.Value;
                foreach(Element element in set) {
                    element.BeatTime /= divider;
                }
                newList.Add(pair.Key / divider, set);
            }
            elements = newList;
        }

        public Measure Add(Element element) {
            if (elements.ContainsKey(element.BeatTime)) {
                elements[element.BeatTime].Add(element);
            } else {
                ISet<Element> set = new HashSet<Element>();
                set.Add(element);
                elements.Add(element.BeatTime, set);
            }
            return this;
        }

        public void RemoveAt(int beat) {
            elements.Remove(beat);
        }

        public void Simplify() {
            Coarsen();
        }
    }
}
