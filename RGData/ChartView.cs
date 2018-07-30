using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGData
{
    /// <summary>A view of a chart, on which the chart can be manipulated.</summary>
    public class ChartView
    {
        protected Chart chart;
        protected ISet<ChartLocation> locations;

        public ChartView(Chart chart) {
            this.chart = chart;
            locations = new HashSet<ChartLocation>();
        }

        public ChartLocation CreateLocation() {
            ChartLocation newLocation = new ChartLocation(chart);
            locations.Add(newLocation);
            return newLocation;
        }
    }
}
