using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGData;

/**
 * EditView.Control.cs - Parts of EditView class related to editor behaviors.
 * In the future, all of these operations will be done with EditAction classes (to support undo/redo).
 **/

namespace OneCharter {
    public partial class EditView {
        #region Undoable actions
        /// <summary>Adds an element to current cursor location.</summary>
        /// <param name="element">The element to be added. Its BeatTime will be modified.</param>
        public void AddElement(Element element) {
            Snap();
            Chart.InsertAt(cursorLocation, element);
            Paint();
        }
        
        public void InsertMeasure(Measure measure) {
            Snap();
            Chart.InsertAt(cursorLocation, measure);
            Paint();
        }

        /// <summary>Move the cursor (+t)</summary>
        public void NextBeat() {
            Pause(); cursorLocation.GoNextBeat(); Paint();
        }

        /// <summary>Move the cursor (-t)</summary>
        public void PrevBeat() {
            Pause(); cursorLocation.GoPrevBeat(); Paint();
        }

        /// <summary>Removes all elemented located at current cursor.</summary>
        public void RemoveElementsAtCursor() {
            Snap(); cursorLocation.Measure.RemoveAt(cursorLocation.Beat); Paint();
        }
        #endregion

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

        /// <summary>Snaps the cursor to nearest beat</summary>
        protected void Snap() {
            cursorLocation.RemoveBeatOffset();
        }
    }
}
