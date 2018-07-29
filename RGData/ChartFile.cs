using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData {
    /// <summary>
    /// A chart with metadata
    /// </summary>
    public class ChartFile {
        protected Chart chart;
        public Chart Chart { get => chart; }
        public double Length { get => chart.Length; }

        /// <summary>Timestamp of the audio file when the chart starts</summary>
        public int Offset = 0;

        public Dictionary<string, string> MetaData;

        public ChartFile() {
            chart = new Chart();
            MetaData = new Dictionary<string, string>();
        }

        public void Simplify() {
            // Simplify every components of chart
            chart.Simplify();

            // The first segment's offset should be zero.
            if(chart.Segments.Count > 0) {
                TimingSegment firstSegment = chart.Segments.First();
                if(firstSegment.Offset > 0) {
                    Offset += (int) firstSegment.Offset;
                    firstSegment.Offset = 0;
                }
            }
        }
        
        public static ChartFile GetTestChartFile() {
            ChartFile f = new ChartFile();
            TimingSegment segment = new TimingSegment();
            segment.BPM = 120;

            Measure measure = new Measure(4);
            measure.Add(new GC.GCDualTapNote(0));
            measure.Add(new GC.GCTapNote(1)).Add(new GC.GCTapNote(2)).Add(new GC.GCTapNote(3));

            segment.Add(measure);

            measure = new Measure(8);
            measure.Add(new GC.GCDualTapNote(0));
            measure.Add(new GC.GCTapNote(1)).Add(new GC.GCTapNote(2)).Add(new GC.GCTapNote(3));
            measure.Add(new GC.GCDualTapNote(4));

            segment.Add(measure);

            f.Chart.Add(segment);
            return f;
        }
    }
}
