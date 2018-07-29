﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGData;

namespace OneCharter {
    /// <summary>Represents a specific location in the chart.</summary>
    public sealed class ChartTime {
        private double currentTime = 0.0d;
        /// <summary>Deviation from currentBeat.</summary>
        private double beatOffset = 0.0d;
        private Chart chart = null;
        private int currentSegmentIndex = 0;
        private TimingSegment currentSegment = null;
        /// <summary>Current segment.</summary>
        public TimingSegment Segment { get => currentSegment; }

        /// <summary>Index of current measure.
        /// -1 means that current time is in the offset section.
        /// -2 means that current time is after the last measure of the last segment.</summary>
        private int currentMeasureIndex = 0;
        private Measure currentMeasure = null;
        /// <summary>Current measure.</summary>
        public Measure Measure { get => currentMeasure; }

        private int currentBeat = 0;
        public int Beat { get => currentBeat; }

        /// <summary>Time in milliseconds.</summary>
        public double Time {
            get => currentTime;
            set {
                if (chart == null) {
                    currentTime = beatOffset = value; return;
                }
                if (NeedsRecomputation()) {
                    currentTime = value;
                    RecomputeCurrentMeasure(); return;
                }
                // TODO: adjust currentSegment and currentMeasure instead of doing this,
                // for a better performance.
                // double inc = value - currentTime;
                currentTime = value;
                RecomputeCurrentMeasure();
            }
        }

        public ChartTime(Chart chart) : this(chart, 0.0d) { }

        public ChartTime(Chart chart, double time) {
            this.chart = chart;
            this.Time = time;
        }

        public override string ToString() {
            return $"[ChartTime s{currentSegmentIndex}:m{currentMeasureIndex}:b{currentBeat} + {beatOffset}]";
        }

        /// <summary>Move to the next beat. This should be fast.</summary>
        public void GoNextBeat() {
            // TODO: jump to the next beat, based on the last measure
            if (currentMeasureIndex == LAST_SECTION) return;

            beatOffset = 0.0d;
            if (currentMeasureIndex == OFFSET_SECTION) {
                currentMeasureIndex = 0;
                currentBeat = 0;
            } else {
                currentBeat++;
            }
            HandleBeatOverflow();
            RecomputeCurrentTime();
        }

        /// <summary>Move to the previous beat. This should be fast.</summary>
        public void GoPrevBeat() {
            if (beatOffset > 0.0d) {
                beatOffset = 0.0d;
            } else {
                if (currentMeasureIndex == LAST_SECTION) {
                    currentMeasureIndex = currentSegment.Measures.Count;
                    currentBeat = -1;
                } else if (currentMeasureIndex == OFFSET_SECTION) {
                    currentMeasureIndex = 0;
                    currentBeat = -1;
                } else {
                    currentBeat--;
                }
                HandleBeatUnderflow();
            }
            RecomputeCurrentTime();
        }

        /// <summary>Set the beatOffset to zero.</summary>
        public void RemoveBeatOffset() {
            currentTime -= beatOffset;
            beatOffset = 0.0d;
        }

        /// <summary>Processes overflowed currentBeat or currentMeasureIndex.
        /// Note: this method assumes that beatOffset is set to zero.</summary>
        private void HandleBeatOverflow() {
            if (currentMeasureIndex < 0) return;
            if (currentMeasure != null && currentBeat >= currentMeasure.TotalBeats) {
                // Next measure
                currentBeat = 0;
                currentMeasureIndex++;
            }
            while (currentMeasureIndex >= currentSegment.Measures.Count) {
                currentMeasureIndex = 0;
                currentSegmentIndex++;
                if (currentSegmentIndex >= chart.Segments.Count) {
                    // There's no next segment
                    currentSegmentIndex--;
                    currentMeasureIndex = LAST_SECTION;
                    currentMeasure = null;
                    break;
                }
                currentSegment = chart.Segments[currentSegmentIndex];
                if (currentSegment.Offset > 0.0d) {
                    currentMeasureIndex = OFFSET_SECTION;
                    currentMeasure = null;
                    break;
                }
            }
            if (currentMeasureIndex >= 0) {
                currentMeasure = currentSegment.Measures[currentMeasureIndex];
            }
        }

        /// <summary>Processes underflowed currentBeat.
        /// Note: this method assumes that beatOffset is set to zero.</summary>
        private void HandleBeatUnderflow() {
            if (currentMeasureIndex < 0) return;
            if (currentBeat < 0) {
                // Do not go any further.
                if (currentSegmentIndex == 0 && currentMeasureIndex == 0) {
                    if (currentSegment.Offset > 0.0d) {
                        currentMeasureIndex = OFFSET_SECTION;
                        currentMeasure = null;
                    }
                    currentBeat = 0;
                    return;
                }
                currentMeasureIndex--;
                while (currentMeasureIndex < 0) {
                    if (currentSegment.Offset > 0.0d) {
                        currentMeasureIndex = OFFSET_SECTION;
                        break;
                    }
                    // Do not go any further.
                    if (currentSegmentIndex == 0) {
                        currentBeat = 0;
                        currentMeasureIndex = 0;
                        break;
                    }
                    currentSegmentIndex--;
                    currentSegment = chart.Segments[currentSegmentIndex];
                    currentMeasureIndex = currentSegment.Measures.Count - 1;
                }
                if (currentMeasureIndex >= 0) {
                    currentMeasure = currentSegment.Measures[currentMeasureIndex];
                    currentBeat = currentMeasure.TotalBeats - 1;
                } else {
                    currentMeasure = null;
                    currentBeat = 0;
                }
            }
        }

        /// <summary>Returns whether currentSegment and currentMeasure must be recomputed.</summary>
        /// <returns>Whether currentSegment or currentMeasure is outdated.</returns>
        private bool NeedsRecomputation() {
            if (chart == null) return false;
            if (currentSegment == null) return true;
            if (currentSegmentIndex >= chart.Segments.Count) return true;
            if (!ReferenceEquals(chart.Segments[currentSegmentIndex], currentSegment)) return true;

            if (currentMeasureIndex == LAST_SECTION) {
                if (chart.Segments.Count != currentSegmentIndex + 1) return true;
            }

            if (currentMeasureIndex == OFFSET_SECTION) {
                if (beatOffset >= currentSegment.Offset) return true;
            }

            if (currentMeasureIndex >= 0) {
                if (currentMeasure == null) return true;
                if (currentMeasureIndex >= currentSegment.Measures.Count) return true;
                if (!ReferenceEquals(currentSegment.Measures[currentMeasureIndex], currentMeasure)) return true;
                if (beatOffset >= currentMeasure.TotalBeats) return true;
            }
            return false;
        }

        /// <summary>Recomputes currentSegment and currentMeasure from currentTime.</summary>
        private void RecomputeCurrentMeasure() {
            if (chart.Segments.Count == 0) {
                currentSegment = null;
                currentSegmentIndex = 0;
                currentMeasure = null;
                currentMeasureIndex = beatOffset > 0 ? LAST_SECTION : OFFSET_SECTION;
                currentBeat = 0;
                beatOffset = currentTime;
                return;
            }

            double chartTime = 0.0d; int segmentIndex = 0;
            foreach (TimingSegment segment in chart.Segments) {
                // Current time is in the offset section.
                if (currentTime < chartTime + segment.Offset) {
                    currentSegment = segment;
                    currentSegmentIndex = segmentIndex;
                    currentMeasure = null;
                    currentMeasureIndex = OFFSET_SECTION;
                    currentBeat = 0;
                    beatOffset = currentTime - chartTime;
                    return;
                }
                int measureIndex = 0;
                // To reduce the # of additions (better safe than sorry)
                double localChartTime = chartTime + segment.Offset;
                foreach (Measure measure in segment.Measures) {
                    double len = segment.LengthOf(measure);
                    if (currentTime < localChartTime + len) {
                        currentSegment = segment;
                        currentSegmentIndex = segmentIndex;
                        currentMeasure = measure;
                        currentMeasureIndex = measureIndex;

                        double measureOffset = currentTime - localChartTime;
                        double numBeats = (measureOffset / segment.MSPW) * measure.QuantBeat;

                        currentBeat = (int) numBeats;
                        beatOffset = (numBeats - currentBeat) / measure.QuantBeat * segment.MSPW;
                        return;
                    }
                    localChartTime += len;
                    measureIndex++;
                }
                chartTime += segment.Length;
                segmentIndex++;
            }

            // After the last measure
            TimingSegment lastSegment = chart.Segments.Last();
            currentSegment = lastSegment;
            currentSegmentIndex = chart.Segments.Count - 1;
            currentMeasure = null;
            currentMeasureIndex = LAST_SECTION;
            currentBeat = 0;
            beatOffset = currentTime - chartTime;
        }

        /// <summary>Recomputes the currentTime from beat information.</summary>
        public void RecomputeCurrentTime() {
            if (chart == null || currentSegment == null) {
                currentTime = beatOffset; return;
            }

            if (!ReferenceEquals(currentSegment, chart.Segments[currentSegmentIndex])) {
                throw new NotImplementedException("TODO: recompute currentSegmentIndex");
            }

            currentTime = 0.0d;
            for (int i = 0; i < currentSegmentIndex; i++)
                currentTime += chart.Segments[i].Length;
            if (currentMeasureIndex == OFFSET_SECTION) {
                currentTime += beatOffset; return;
            }
            if (currentMeasureIndex == LAST_SECTION) {
                currentTime += currentSegment.Length + beatOffset; return;
            }


            if (!ReferenceEquals(currentMeasure, currentSegment.Measures[currentMeasureIndex])) {
                throw new NotImplementedException("TODO: recompute currentMeasureIndex");
            }

            currentTime += currentSegment.Offset;
            for (int i = 0; i < currentMeasureIndex; i++)
                currentTime += currentSegment.LengthOf(currentSegment.Measures[i]);
            currentTime += currentBeat * currentSegment.BeatLengthOf(currentMeasure) + beatOffset;
        }

        private const int OFFSET_SECTION = -1;
        private const int LAST_SECTION = -2;
    }
}
