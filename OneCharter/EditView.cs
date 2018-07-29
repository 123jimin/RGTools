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
        protected Control view;

        // Chart
        protected ChartFile chartFile;

        // Timer for playback
        protected HexaTimer timer;
        /// <summary>End time for playback</summary>
        private double maxTime = 0;

        /// <summary>The chart file which is currently viewed</summary>
        public ChartFile ChartFile {
            get => chartFile;
            set {
                chartFile = value;
                currentLocation = new ChartTime(value.Chart);
            }
        }
        /// <summary>The chart which is currently viewed</summary>
        public Chart Chart { get => chartFile.Chart; }

        /// <summary>Display scale (pixel for a 4th note)</summary>
        public float PixelPerQuad = 80;

        protected ChartTime currentLocation = new ChartTime(null);
        /// <summary>Current time (relative to the chart, not the audio)</summary>
        public double CurrentTime { get => currentLocation.Time; } // TODO: make a setter

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
            this.view = view;
            InitView();

            timer = new HexaTimer(1000d / FPS);
            timer.Tick += Tick;

            ChartFile = ChartFile.GetTestChartFile();

            Paint();
        }

        /// <summary>Snaps the cursor to nearest beat</summary>
        protected void Snap() {
            currentLocation.RemoveBeatOffset();
        }

        /// <summary>Starts playing</summary>
        public void Play() {
            if (isPlaying) return;
            isPlaying = true;
            maxTime = chartFile.Length + 1000;
            timer.Start(CurrentTime);
            
            StartPlaying?.Invoke(this, new EventArgs());
        }

        /// <summary>Stops playing</summary>
        public void Pause() {
            if (!isPlaying) return;
            isPlaying = false;
            timer.Stop();

            StopPlaying?.Invoke(this, new EventArgs());
        }

        /// <summary>Move the cursor (+t)</summary>
        public void MoveFuture() {
            Pause(); currentLocation.GoNextBeat(); Paint();
        }

        /// <summary>Move the cursor (-t)</summary>
        public void MovePast() {
            Pause(); currentLocation.GoPrevBeat(); Paint();
        }

        public void Paint() {
            lock (gLock) {
                Graphics graphics = bufferedGraphics.Graphics;
                Draw(graphics);
            }
            view.Invalidate();
        }

        /// <summary>Adds an element to current cursor location.</summary>
        /// <param name="element">The element to be added. Its BeatTime will be modified.</param>
        public void AddElement(Element element) {
            Snap();
            element.BeatTime = currentLocation.Beat;
            currentLocation.Measure.Add(element);
            Paint();
        }

        private void InitView() {
            gContext = BufferedGraphicsManager.Current;
            gContext.MaximumBuffer = new Size(view.Width + 1, view.Height + 1);

            view.Paint += new PaintEventHandler(PaintView);
            view.Resize += new EventHandler(OnResizeView);
            bufferedGraphics = gContext.Allocate(view.CreateGraphics(), new Rectangle(0, 0, view.Width, view.Height));
        }

        private void PaintView(object sender, PaintEventArgs e) {
            lock (gLock) {
                bufferedGraphics.Render(e.Graphics);
            }
        }

        private void OnResizeView(object sender, EventArgs e) {
            gContext.MaximumBuffer = new Size(view.Width+1, view.Height+1);
            lock (gLock) {
                bufferedGraphics?.Dispose();
                bufferedGraphics = gContext.Allocate(view.CreateGraphics(), new Rectangle(0, 0, view.Width, view.Height));
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
                currentLocation.Time = 0;
                stopTimer = true;
            } else {
                currentLocation.Time = e.CurrentTime;
            }
            
            Paint();

            if (stopTimer) {
                Pause();
            }
        }

        public event EventHandler StartPlaying, StopPlaying;

    }
}
