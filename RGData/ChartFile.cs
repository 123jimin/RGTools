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
        
        public static ChartFile GetTestChartFile() {
            ChartFile f = new ChartFile();
            Segment segment = new Segment();
            segment.BPM = 120;

            BeatMeasure measure = new BeatMeasure(4);
            measure.Add(new GC.GCDualTapNote(), 0);
            measure.Add(new GC.GCTapNote(), 1);
            measure.Add(new GC.GCTapNote(), 2);
            measure.Add(new GC.GCTapNote(), 3);

            segment.Append(measure);

            measure = new BeatMeasure(8);
            measure.Add(new GC.GCDualTapNote(), 0);
            measure.Add(new GC.GCTapNote(), 1);
            measure.Add(new GC.GCTapNote(), 2);
            measure.Add(new GC.GCTapNote(), 3);
            measure.Add(new GC.GCDualTapNote(), 4);

            segment.Append(measure);

            f.Chart.Append(segment);
            return f;
        }
    }
}
