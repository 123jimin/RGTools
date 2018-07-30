using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGData;
using System.Windows.Forms;
using System.Drawing;
using OneCharter.Timing;
using System.Diagnostics;

namespace OneCharter {
    partial class EditView {
        // Graphics
        private BufferedGraphicsContext gContext;
        private BufferedGraphics bufferedGraphics;
        private object gLock = new object();
        protected Control viewPanel;

        // Chart
        protected ChartFile chartFile;

        // View of the chart
        protected ChartView chartView;

        // Cursors
        protected ChartLocation cursorLocation;

        // Timer for playback
        protected HexaTimer timer;
        /// <summary>End time for playback</summary>
        private double maxTime = 0;

        /// <summary>The chart file which is currently viewed</summary>
        public ChartFile ChartFile {
            get => chartFile;
            set {
                chartFile = value;
                chartView = new ChartView(value.Chart);
                cursorLocation = chartView.CreateLocation();
            }
        }
        /// <summary>The chart which is currently viewed</summary>
        public Chart Chart { get => chartFile.Chart; }

        /// <summary>Display scale (pixel for a 4th note)</summary>
        public float PixelPerQuad = 80;

        // protected ChartTime currentLocation = new ChartTime(null);
        /// <summary>Current time (relative to the chart, not the audio)</summary>
        public double CurrentTime { get => cursorLocation.Time; } // TODO: make a setter
        // public ChartTime CurrentLocation { get => currentLocation; }

        /// <summary>Desired FPS to draw</summary>
        public double FPS = 45;

        protected bool isPlaying = false;
        /// <summary>Whether the editor is currently playing (scrolling)</summary>
        public bool IsPlaying { get => isPlaying; } // TODO: make a setter (play / pause)

        /// <summary>The y coordinate of the start of the chart, relative to the cursor.</summary>
        public float CurrentScrollY { get {
                return PixelPerQuad * (float) chartFile.Chart.GetQuadCount(CurrentTime);
        }}

        public EditView(Control view) {
            this.viewPanel = view;
            InitView();

            timer = new HexaTimer(1000d / FPS);
            timer.Tick += Tick;

            ChartFile = ChartFile.GetTestChartFile();

            Paint();
        }

        private void InitView() {
            gContext = BufferedGraphicsManager.Current;
            gContext.MaximumBuffer = new Size(viewPanel.Width + 1, viewPanel.Height + 1);

            viewPanel.Paint += new PaintEventHandler(PaintView);
            viewPanel.Resize += new EventHandler(OnResizeView);
            bufferedGraphics = gContext.Allocate(viewPanel.CreateGraphics(), new Rectangle(0, 0, viewPanel.Width, viewPanel.Height));
        }

        private void PaintView(object sender, PaintEventArgs e) {
            lock (gLock) {
                bufferedGraphics.Render(e.Graphics);
            }
        }

        private void OnResizeView(object sender, EventArgs e) {
            gContext.MaximumBuffer = new Size(viewPanel.Width+1, viewPanel.Height+1);
            lock (gLock) {
                bufferedGraphics?.Dispose();
                bufferedGraphics = gContext.Allocate(viewPanel.CreateGraphics(), new Rectangle(0, 0, viewPanel.Width, viewPanel.Height));
            }
            Paint();
        }

        private void Tick(object sender, HexaTickEventArgs e) {
            if (!isPlaying) {
                // Should not happen
                timer.Stop(); return;
            }

            bool stopTimer = false;
            if (e.CurrentTime > maxTime) {
                cursorLocation.Time = 0;
                stopTimer = true;
            } else {
                cursorLocation.Time = e.CurrentTime;
            }
            
            Paint();

            if (stopTimer) {
                Pause();
            }
        }

        public event EventHandler StartPlaying, StopPlaying;

    }
}
