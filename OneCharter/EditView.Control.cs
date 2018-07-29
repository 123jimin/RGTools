using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGData;

namespace OneCharter {
    public partial class EditView {
        /// <summary>Adds an element to current cursor location.</summary>
        /// <param name="element">The element to be added. Its BeatTime will be modified.</param>
        public void AddElement(Element element) {
            Snap();
            element.BeatTime = currentLocation.Beat;
            currentLocation.Measure.Add(element);
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

        /// <summary>Removes all elemented located at current cursor.</summary>
        public void RemoveElementsAtCursor() {
            Snap(); currentLocation.Measure.RemoveAt(currentLocation.Beat); Paint();
        }

        /// <summary>Draws the current chart to the graphics buffer.</summary>
        public void Paint() {
            lock (gLock) {
                Graphics graphics = bufferedGraphics.Graphics;
                Draw(graphics);
            }
            view.Invalidate();
        }
    }
}
