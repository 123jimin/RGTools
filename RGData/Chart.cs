using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    public partial class Chart {
        protected IList<Segment> segments;
        public IList<Segment> Segments { get => segments; }
        protected ISet<Location> locations;

        /// <summary>Length of this chart in ms.</summary>
        public double Length {
            get {
                double length = 0.0d;
                foreach(Segment segment in segments) length += segment.Length;
                return length;
            }
        }

        public Chart() {
            segments = new List<Segment>();
            locations = new HashSet<Location>();
        }

        public Location CreateLocation() {
            Location location = new Location(this);
            locations.Add(location);
            return location;

        }

        /// <summary>Adds a new segment to the end.</summary>
        /// <param name="segment">The segment to be added.</param>
        public void Add(Segment segment) {
            segments.Add(segment);

            // Move the location into this segment if necessary.
            foreach (Location location in locations) {
                if (location.IsLastMeasure()) {
                    location.HandleOverflow();
                }
            }
        }
        
        /// <summary>Add an element at specific location on this chart.</summary>
        /// <param name="element">The element to be added.</param>
        /// <param name="location">The location which the element will be added.</param>
        public void AddElement(Element element, Location location) {
            if (location.IsLastMeasure() && (location.Segment is Segment tSeg)) {
                // Extends the last measure.
                Measure measure = location.Measure;
                if (location.Beat >= measure.TotalBeats) {
                    measure.Extend(location.Beat + 1);
                }
            }
            location.Measure.Add(element, location.Beat);
        }
    }
}
