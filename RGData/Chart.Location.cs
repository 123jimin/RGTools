using System;

namespace RGData {
    public partial class Chart {
        public class Location {
            protected Chart chart;

            // Current time (cache purpose)
            private double time = 0.0d;

            // Current location (main)
            internal int segmentIndex;

            internal Segment segment;
            internal int measureIndex;
            internal Measure measure;
            internal int beat;
            internal double beatOffset;

            #region Getters and Setters for a location

            /// <summary>The measure containing this location</summary>
            public Measure Measure { get => measure; }

            /// <summary>The segment containing this location</summary>
            public Segment Segment { get => segment; }

            /// <summary>The beat (in a measure) for this location</summary>
            public int Beat { get => beat; }

            /// <summary>The offset from the last beat before this location</summary>
            public double BeatOffset { get => beatOffset; }

            /// <summary>The time (in ms) from the beginning of the chart, for this location</summary>
            public double Time {
                get => time;
                set {
                    // TODO: checks whether (value - time) is small enough to compute indices easily.
                    SetTime(value);
                }
            }

            /// <summary># of quads from the beginning of the chart, for this location</summary>
            public double TimeInQuad {
                get {
                    double quads = 0.0d;
                    for (int i = 0; i < segmentIndex; i++) quads += chart.Segments[i].QuadCount;
                    for (int i = 0; i < measureIndex; i++) quads += segment.QuadLengthOf(segment.Measures[i]);
                    switch (measure) {
                        case BeatMeasure bMeasure:
                            quads += beat * (4.0d / bMeasure.QuantBeat) + beatOffset / segment.MSPQ;
                            break;
                        case OffsetMeasure oMeasure:
                            quads += (beat * oMeasure.UnitLength + beatOffset) / segment.MSPQ;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    return quads;
                }
            }

            /// <summary>The unit time for this location (in forward direction)</summary>
            public double UnitTime {
                get => segment.BeatLengthOf(measure);
            }

            #endregion Getters and Setters for a location

            #region Constructors

            /// <summary>Create a new location, pointing the beginning of a view.</summary>
            /// <param name="chart">The chart which this location will use.</param>
            public Location(Chart chart) : this(chart, 0.0d) { }

            /// <summary>Copy an existing location.</summary>
            /// <param name="location">The location which will be cloned.</param>
            public Location(Location location) : this(location.chart, location) { }

            /// <summary>Create a new location given time in ms.</summary>
            /// <param name="chart">The chart which this location will use.</param>
            /// <param name="time">The time in ms which this location will be pointing.</param>
            public Location(Chart chart, double time) {
                this.chart = chart;
                Time = time;
            }

            /// <summary>Create a new location based on an existing location.</summary>
            /// <param name="chart">The chart which this location will use.</param>
            /// <param name="location">A location which this location will be pointing.</param>
            public Location(Chart chart, Location location) {
                if (!ReferenceEquals(chart, location.chart)) {
                    // We can ignore this problem though...
                    throw new ArgumentException("The view and the location for Location constructor should match!");
                }

                this.chart = chart;
                time = location.time;

                SetLocation(location.segmentIndex, location.measureIndex, location.beat, location.beatOffset);
            }

            #endregion Constructors

            #region Public queries
            public override string ToString() {
                return $"[Location s{segmentIndex}:m{measureIndex}:b{beat} + {beatOffset}]";
            }

            /// <summary>Returns a representation string, readable by a human.</summary>
            /// <returns>A readable representation of this location.</returns>
            public string ToRepr() {
                string beatString;
                switch (measure) {
                    case BeatMeasure bMeasure:
                        beatString = $"({beat} of {bMeasure.TotalBeats}) / {bMeasure.QuantBeat}";
                        break;
                    case OffsetMeasure oMeasure:
                        beatString = $"{oMeasure.UnitLength}ms x {beat} of {oMeasure.TotalBeats}";
                        break;
                    default:
                        throw new NotImplementedException();
                }
                return $"Segment {segmentIndex+1}, Measure {measureIndex+1}, {beatString}";
            }

            /// <summary>Returns whether this location is in the very first measure.</summary>
            /// <returns>Whether this location is in the very first measure.</returns>
            public bool IsFirstMeasure() {
                return segmentIndex == 0 && measureIndex == 0;
            }

            /// <summary>Returns whether this location is in or after the very last measure.</summary>
            /// <returns>Whether this location is in or after the very last measure.</returns>
            public bool IsLastMeasure() {
                return segmentIndex == chart.Segments.Count - 1 && measureIndex == segment.Measures.Count - 1;
            }

            /// <summary>Returns whether this location is at a beginning of a measure.</summary>
            /// <returns>Whether this location is at a beginning of a measure.</returns>
            public bool IsAtBeginningOfAMeasure() {
                return beat == 0 && beatOffset == 0.0d;
            }

            /// <summary>Returns the previous measure of the measure this location is in.</summary>
            /// <returns>The last measure before the measure with this location,
            /// or the first measure if such measure does not exist.</returns>
            public Measure LastMeasureBeforeThis() {
                if (IsFirstMeasure() || measureIndex > 0) return measure;
                var prevSegMeasures = chart.segments[segmentIndex - 1].Measures;
                if(prevSegMeasures.Count == 0) {
                    // Look up previous segments.
                    // This should not occur in normal conditions.
                    throw new NotImplementedException();
                }
                return prevSegMeasures[prevSegMeasures.Count - 1];
            }
            #endregion

            #region Public operations

            /// <summary>Snaps the location to the last beat before this location.</summary>
            public void RemoveBeatOffset() {
                time -= beatOffset;
                beatOffset = 0.0d;
            }

            /// <summary>Move to the next beat.</summary>
            public void GoNextBeat() {
                RemoveBeatOffset();
                time += UnitTime; beat++;
                HandleOverflow();
                // TODO: check that time is updated correctly.
            }

            /// <summary>Move to the previous beat.</summary>
            public void GoPrevBeat() {
                if (beatOffset > 0.0d) {
                    RemoveBeatOffset(); return;
                }
                if (!IsFirstMeasure() || beat > 0) beat--;
                HandleUnderflow();
                // TODO: do not recompute time from scratch.
                RecomputeTime();
            }

            #endregion Public operations

            #region Comparisons
            public override bool Equals(object obj) {
                Location that = obj as Location;
                if (that == null) return false;
                return this == that;
            }
            public override int GetHashCode() {
                NormalizeIndices();

                int hash = 0x10CA7109;
                hash ^= segmentIndex * 0x1001;
                hash ^= measureIndex * 0xFFF;
                hash ^= beat * 0x81;
                hash ^= beatOffset.GetHashCode();

                return hash;
            }
            public static bool operator ==(Location l1, Location l2) {
                l1.NormalizeIndices(); l2.NormalizeIndices();
                if (l1.segmentIndex != l2.segmentIndex || l1.measureIndex != l2.measureIndex) return false;
                if (l1.beat != l2.beat || l1.beatOffset != l2.beatOffset) return false;
                return true;
            }
            public static bool operator !=(Location l1, Location l2) {
                return !(l1 == l2);
            }
            public static bool operator >(Location l1, Location l2) {
                l1.NormalizeIndices(); l2.NormalizeIndices();
                if (l1.segmentIndex < l2.segmentIndex) return false;
                if (l1.segmentIndex > l2.segmentIndex) return true;
                if (l1.measureIndex < l2.measureIndex) return false;
                if (l1.measureIndex > l2.measureIndex) return true;
                if (l1.beat < l2.beat) return false;
                if (l1.beat > l2.beat) return true;
                return l1.beatOffset > l2.beatOffset;
            }
            public static bool operator >=(Location l1, Location l2) {
                l1.NormalizeIndices(); l2.NormalizeIndices();
                if (l1.segmentIndex < l2.segmentIndex) return false;
                if (l1.segmentIndex > l2.segmentIndex) return true;
                if (l1.measureIndex < l2.measureIndex) return false;
                if (l1.measureIndex > l2.measureIndex) return true;
                if (l1.beat < l2.beat) return false;
                if (l1.beat > l2.beat) return true;
                return l1.beatOffset >= l2.beatOffset;
            }
            public static bool operator <(Location l1, Location l2) {
                return l2 > l1;
            }
            public static bool operator <=(Location l1, Location l2) {
                return l2 >= l1;
            }
            #endregion

            /// <summary>Recomputes Time based on current location (segment / measure).</summary>
            internal void RecomputeTime() {
                time = 0.0d;
                for (int i = 0; i < segmentIndex; i++) {
                    time += chart.Segments[i].Length;
                }
                for (int i = 0; i < measureIndex; i++) {
                    time += segment.LengthOf(i);
                }
                time += beat * segment.BeatLengthOf(measureIndex);
                time += beatOffset;
            }

            /// <summary>Sets `segment` from `measure` from indices.</summary>
            private void ApplyIndices() {
                if (chart.Segments.Count == 0) {
                    segmentIndex = measureIndex = 0;
                    segment = null; measure = null;
                    return;
                }
                ApplySegmentIndex();
                ApplyMeasureIndex();
            }

            private void ApplySegmentIndex() {
                segment = chart.Segments[segmentIndex];
            }

            private void ApplyMeasureIndex() {
                measure = segment.Measures[measureIndex];
            }

            /// <summary>Sets the location from indices, <b>without handling out-of-bound indices</b>.
            /// These four numbers fully determine a location on a chart.</summary>
            private void SetLocation(int segmentIndex, int measureIndex, int beat, double beatOffset) {
                this.segmentIndex = segmentIndex;
                this.measureIndex = measureIndex;
                this.beat = beat;
                this.beatOffset = beatOffset;
                ApplyIndices();
            }

            /// <summary>Moves the location to given value by recomputing indices.</summary>
            /// <param name="time">Time in ms</param>
            protected void SetTime(double time) {
                this.time = time;
                // Edge case: no segments at all...?
                if (chart.Segments.Count == 0) {
                    SetLocation(0, 0, 0, time);
                    return;
                }
                double segmentStartTime = 0.0d;
                double lastStartTime = 0.0d;
                int currSegmentInd = 0;
                foreach (Segment segment in chart.Segments) {
                    lastStartTime = segmentStartTime;
                    double segmentEndTime = segmentStartTime + segment.Length;
                    if (time < segmentEndTime) {
                        // This location is in this segment.
                        double measureStartTime = segmentStartTime;
                        int currMeasureInd = 0;
                        foreach (Measure measure in segment.Measures) {
                            double measureEndTime = measureStartTime + segment.LengthOf(measure);
                            bool isThisLast = currSegmentInd == chart.Segments.Count - 1;
                            isThisLast = isThisLast && currMeasureInd == segment.Measures.Count - 1;
                            // This location is in this measure (or after the last measure)
                            if (isThisLast || time < measureEndTime) {
                                double measureOffset = time - measureStartTime;
                                double numBeats;
                                switch (measure) {
                                    case BeatMeasure bMeasure:
                                        numBeats = (measureOffset / segment.MSPW) * bMeasure.QuantBeat;

                                        SetLocation(currSegmentInd, currMeasureInd, (int) numBeats,
                                            (numBeats - (int) numBeats) / bMeasure.QuantBeat * segment.MSPW);
                                        return;
                                    case OffsetMeasure oMeasure:
                                        numBeats = measureOffset / oMeasure.UnitLength;
                                        SetLocation(currSegmentInd, currMeasureInd, (int) numBeats,
                                            measureOffset - ((int) numBeats) * oMeasure.UnitLength);
                                        return;
                                    default:
                                        throw new NotImplementedException();
                                }
                            }
                            measureStartTime = measureEndTime;
                            currMeasureInd++;
                        }
                    }
                    segmentStartTime = segmentEndTime;
                    currSegmentInd++;
                }

                // The last segment *should* be a BlankSegment
                SetLocation(chart.Segments.Count - 1, 0, 0, time - lastStartTime);
            }

            #region Detecting and handling overflows / underflows

            // Underflow checks are just a thin wrappers to simple sign checks.

            private bool HasOverflow() {
                // First apply the segmentIndex
                ApplySegmentIndex();
                if (HasMeasureOverflow()) return true;
                // Then apply the measureIndex
                ApplyMeasureIndex();
                return HasBeatOverflow() || HasOffsetOverflow();
            }

            private bool HasUnderflow() {
                // First apply the segmentIndex
                ApplySegmentIndex();
                if (HasMeasureUnderflow()) return true;
                // Then apply the measureIndex
                ApplyMeasureIndex();
                return HasBeatUnderflow() || HasOffsetUnderflow();
            }

            private bool HasOffsetOverflow() {
                return beatOffset >= UnitTime;
            }

            private bool HasOffsetUnderflow() {
                return beatOffset < 0.0d;
            }

            private bool HasBeatOverflow() {
                return !IsLastMeasure() && beat >= measure.TotalBeats;
            }

            private bool HasBeatUnderflow() {
                return beat < 0;
            }

            private bool HasMeasureOverflow() {
                return measureIndex >= segment.Measures.Count;
            }

            private bool HasMeasureUnderflow() {
                return measureIndex < 0;
            }

            /// <summary>Normalize beatOffset, beat, and segmentIndex.</summary>
            internal void NormalizeIndices() {
                if (HasUnderflow()) HandleUnderflow();
                if (HasOverflow()) HandleOverflow();
                // measure and segment are applied through above methods.
            }

            /// <summary>Handles beatOffset, beat, and measureIndex overflows.</summary>
            internal void HandleOverflow() {
                // HasOverflow automatically applies segmentIndex and measureIndex.
                while (HasOverflow()) {
                    // Order of these checks are important.
                    if (HasMeasureOverflow()) {
                        segmentIndex++;
                        measureIndex -= segment.Measures.Count;
                        continue;
                    }
                    if (HasBeatOverflow()) {
                        beat -= measure.TotalBeats;
                        measureIndex++;
                        continue;
                    }
                    if (HasOffsetOverflow()) {
                        beatOffset -= UnitTime; beat++;
                        continue;
                    }
                }
            }

            /// <summary>Handles beatOffset, beat, measureIndex, and segmentIndex underflows.</summary>
            internal void HandleUnderflow() {
                // TODO: stop when underflows before 0
                while (HasMeasureUnderflow()) {
                    segmentIndex--; ApplySegmentIndex();
                    measureIndex += segment.Measures.Count;
                    continue;
                }
                while (HasBeatUnderflow()) {
                    while (measureIndex == 0) {
                        segmentIndex--; ApplySegmentIndex();
                        measureIndex += segment.Measures.Count; ApplyMeasureIndex();
                    }
                    measureIndex--; ApplyMeasureIndex();
                    beat += measure.TotalBeats;
                }
                while (HasOffsetUnderflow()) {
                    throw new NotImplementedException();
                }
            }
            #endregion Detecting and handling overflows / underflows
        }
    }
}