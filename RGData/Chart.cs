using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    /// <summary>A class representing a chart data, without any metadata.
    /// Chart.Locations (obtained by CreateLocation) can be used to manipulate the chart.</summary>
    public partial class Chart {
        protected IList<Segment> segments;
        public IList<Segment> Segments { get => segments; }
        protected ISet<Location> locations;

        /// <summary>Length of this chart in ms.</summary>
        public double Length {
            get {
                double length = 0.0d;
                foreach (Segment segment in segments) length += segment.Length;
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

        /// <summary>Adds a new segment to the end of this chart.</summary>
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

        /// <summary>Adds a new measure to the end of this chart.</summary>
        /// <param name="measure">The measure to be added.</param>
        public void Add(Measure measure) {
            if (segments.Count == 0) {
                throw new InvalidOperationException("Tried to add a new measure to a chart without any segment!");
            }

            segments.Last().Add(measure);

            // Move the location into this measure if necessary.
            foreach (Location location in locations) {
                if (location.IsLastMeasure()) {
                    location.HandleOverflow();
                }
            }
        }

        /// <summary>Add an element at specific location on this chart.</summary>
        /// <param name="element">The element to be added.</param>
        /// <param name="location">The location which the element will be added (beatOffset will be ignored!).</param>
        public void AddElement(Element element, Location location) {
            if (location.IsLastMeasure() && (location.Segment is Segment tSeg)) {
                // Extends the last measure.
                Measure measure = location.Measure;
                if (location.Beat >= measure.TotalBeats) {
                    ExtendMeasure(measure, location.Beat + 1);
                }
            }
            location.Measure.Add(element, location.Beat);
        }

        /// <summary>Extends a measure the location is pointing.</summary>
        /// <param name="location">The location which is pointing to the measure.</param>
        /// <param name="totalBeats">New totalBeats for the selected measure.</param>
        public void ExtendMeasureAt(Location location, int totalBeats) {
            // End of another measure
            if (location.IsAtBeginningOfAMeasure()) ExtendMeasure(location.LastMeasureBeforeThis(), totalBeats);
            else ExtendMeasure(location.Measure, totalBeats);
        }

        /// <summary>Extends a measure.</summary>
        /// <param name="measure">The measure which will be extended.</param>
        /// <param name="totalBeats">New totalBeats for the selected measure.</param>
        public void ExtendMeasure(Measure measure, int totalBeats) {
            measure.Extend(totalBeats);
            UpdateLocations();
        }

        /// <summary>Make the measure finer by increasing its quantBeat value.</summary>
        /// <param name="location">The location which is pointing to the measure.</param>
        /// <param name="fineQuantBeat">The desired quantBeat for the measure.</param>
        public void FineMeasureAt(Location location, int fineQuantBeat) {
            FineMeasure(location.Measure, fineQuantBeat);
        }

        /// <summary>Make the measure finer by increasing its quantBeat value.</summary>
        /// <param name="measure">The measure which will be made finer.</param>
        /// <param name="fineQuantBeat">The desired quantBeat for the measure.</param>
        public void FineMeasure(Measure measure, int fineQuantBeat) {
            throw new NotImplementedException();
        }

        /// <summary>Insert a given measure at the given location.</summary>
        /// <param name="location">The location which the measure will be added.</param>
        /// <param name="measure">The measure to be added.</param>
        public void InsertMeasureAt(Location location, Measure measure) {
            if (location.IsLastMeasure() && location.beat >= location.Measure.TotalBeats) {
                // Append a measure to the end of the chart.
                throw new NotImplementedException();
            }
            // TODO: do not split if measure can be 'blended in'.
            SplitMeasureAt(location);
            // Indices for the new measure.
            Segment targetSegment = location.Segment;
            int targetSegmentIndex = location.segmentIndex;
            int targetMeasureIndex = location.measureIndex;
            if (location.measureIndex == 0 && targetSegmentIndex > 0) {
                targetSegment = segments[--targetSegmentIndex];
                targetMeasureIndex = targetSegment.Measures.Count;
            }
            targetSegment.Measures.Insert(targetMeasureIndex, measure);

            foreach(Location loc in locations) {
                if(loc.segmentIndex == targetSegmentIndex && loc.measureIndex >= targetMeasureIndex) {
                    loc.measureIndex++;
                }
                loc.NormalizeIndices();
                loc.RecomputeTime();
            }
        }

        /// <summary>Split a measure into two or many measures, cut at the given measure.</summary>
        /// <param name="location">The location which will be used to cut a measure.</param>
        public void SplitMeasureAt(Location location) {
            // No need to split.
            if (location.IsAtBeginningOfAMeasure()) return;
            throw new NotImplementedException();
        }

        /// <summary>Normalize indices and recompute times for all locations.</summary>
        protected void UpdateLocations() {
            foreach(Location location in locations) {
                location.NormalizeIndices();
                location.RecomputeTime();
            }
        }
    }
}
