using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData
{
    public abstract class Measure {
        protected int groupBeats = 1;
        protected int totalBeats = 0;

        protected SortedList<int, ISet<Element>> elements = new SortedList<int, ISet<Element>>();

        /// <summary>Gives pairs of (beat, element).</summary>
        public IList<(int, Element)> Elements {
            get {
                var v = new List<(int, Element)>();
                foreach (var kv in elements) {
                    foreach(Element element in kv.Value) {
                        v.Add((kv.Key, element));
                    }
                }
                return v;
            }
        }
        public IList<int> Beats { get => elements.Keys; }

        /// <summary>Total number of beats in this measure chunk.</summary>
        public int TotalBeats { get => totalBeats; }

        /// <summary>Number of beats per a measure</summary>
        public int GroupBeats { get => groupBeats; }

        /// <summary>Adds new measures to this measure group.</summary>
        /// <param name="totalBeats">Desired amount of total beats</param>
        internal void Extend(int totalBeats) {
            if (totalBeats <= this.totalBeats) throw new InvalidOperationException("TotalBeats can't be reduced.");
            if (totalBeats % groupBeats > 0) {
                this.totalBeats = totalBeats - (totalBeats % groupBeats) + groupBeats;
            } else {
                this.totalBeats = totalBeats;
            }
        }
        
        public void Add(int beat, Element element) {
            Add(element, beat);
        }

        public void Add(Element element, int beat) {
            if (elements.ContainsKey(beat)) {
                ISet<Element> set = elements[beat];
                bool noCollide = true;
                foreach (Element e in set) {
                    if (Element.Collides(e, element)) {
                        noCollide = false; break;
                    }
                }
                if (noCollide) set.Add(element);
            } else {
                ISet<Element> set = new HashSet<Element>();
                set.Add(element);
                if (beat >= totalBeats) {
                    // Do NOT auto-extend the measure here!
                    throw new ArgumentOutOfRangeException($"Tried to put element at {beat} in a measure with size {totalBeats}.");
                }
                elements.Add(beat, set);
            }
        }

        public void RemoveAt(int beat) {
            elements.Remove(beat);
        }
    }
}
