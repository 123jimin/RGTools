using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public class Measure {
        protected int quantBeat;
        protected int size;

        /// <summary>Unit beat size</summary>
        public int QuantBeat { get => quantBeat; set => Fine(value); }
        /// <summary>Length of this measure, in terms of unit beats</summary>
        public int Size { get => size; }

        /// <summary>Number of 4th notes in this measure.</summary>
        public double QuadLength { get => size * 4.0d / quantBeat; }
        /// <summary>Number of whole(1st) notes in this measure.</summary>
        public double WholeLength { get => (double) (size) / quantBeat; }


        protected SortedList<int, Element> elements;
        public IList<Element> Elements { get => elements.Values; }

        public Measure(int quantBeat = 4) : this(quantBeat, quantBeat) { }
        public Measure(int quantBeat, int size) {
            elements = new SortedList<int, Element>();

            this.quantBeat = quantBeat;
            this.size = size;
        }

        public void Fine(int adjustQuant) {
            if (adjustQuant == quantBeat) return;
            int multiplier = adjustQuant / Util.GCD(quantBeat, adjustQuant);
            SortedList<int, Element> newList = new SortedList<int, Element>();
            foreach (var pair in elements) {
                Element element = pair.Value;
                element.BeatTime *= multiplier;
                newList.Add(element.BeatTime, element);
            }
            elements = newList;
            quantBeat *= multiplier;
            size *= multiplier;
        }

        public void Coarsen() {
            int divider = Util.GCD(quantBeat, size);
            if (divider == 1) return;
            foreach (int time in elements.Keys) {
                divider = Util.GCD(divider, time);
                if (divider == 1) return;
            }

            quantBeat /= divider;
            size /= divider;

            SortedList<int, Element> newList = new SortedList<int, Element>();
            foreach (var pair in elements) {
                Element element = pair.Value;
                element.BeatTime /= divider;
                newList.Add(element.BeatTime, element);
            }
            elements = newList;
        }

        public Measure Add(Element element) {
            elements.Add(element.BeatTime, element);
            return this;
        }

        public void Simplify() {
            Coarsen();
        }
    }
}
