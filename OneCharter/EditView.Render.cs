using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGData;

namespace OneCharter {
    partial class EditView {
        private static Sprite GetSprite(Element element) {
            switch (element) {
                case RGData.GC.GCDualTapNote x:
                    return SpGCDualTapNote;
                case RGData.GC.GCTapNote x:
                    return SPGCTapNote;
                default:
                    return Sprite.Empty;
            }
        }

        /// <summary>Draw the chart on a graphics.</summary>
        /// <param name="g">The graphics which the chart will be drawn on.</param>
        protected void Draw(Graphics g) {
            float centerX = view.Width / 2;
            float centerY = view.Height - PixelPerQuad * LOOKBEHIND_AMOUNT;
            g.Clear(Color.White);
            g.TranslateTransform(centerX, centerY);

            float lookahead = centerY + DRAW_MARGIN;
            float lookbehind = PixelPerQuad * LOOKBEHIND_AMOUNT + DRAW_MARGIN;

            // Centerline
            g.DrawLine(PEN_CENTERLINE, 0, -lookahead, 0, lookbehind);

            if (chartFile?.Chart == null) return;

            // The Y coordinate of the first element
            float scrollY = CurrentScrollY;
            // TODO: only draw the measures on the screen
            // Where the current measure being drawn starts
            float measureStartY = scrollY;
            Action<Pen, int, float> DrawLineAt = (Pen pen, int width, float yCoord) => {
                g.DrawLine(pen, -width / 2, yCoord, width / 2, yCoord);
            };
            Action<Pen, int> DrawLine = (Pen pen, int width) => DrawLineAt(pen, width, measureStartY);

            foreach (TimingSegment segment in chartFile.Chart.Segments) {
                DrawLine(PEN_SEGMENT, WIDTH_SEGMENT);

                bool drawFirstMeasure = segment.Offset > 0;
                measureStartY -= (float) (segment.Offset * PixelPerQuad / segment.MSPQ);
                foreach (Measure measure in segment.Measures) {
                    // Draw the beginnng point of the measure
                    if (drawFirstMeasure) {
                        DrawLine(PEN_MEASURE, WIDTH_MEASURE);
                        drawFirstMeasure = false;
                    }
                    // Draw the beatlines
                    float beatInterval = PixelPerQuad * 4.0f / measure.QuantBeat;
                    for (int i = 1; i < measure.Size; i++) {
                        DrawLineAt(PEN_BEAT, WIDTH_BEAT, measureStartY - beatInterval * i);
                    }
                    foreach (Element element in measure.Elements) {
                        GetSprite(element).DrawOn(g, 0, measureStartY - beatInterval * element.BeatTime);
                    }
                    measureStartY -= PixelPerQuad * (float) measure.QuadLength;
                    DrawLine(PEN_MEASURE, WIDTH_MEASURE);
                }
            }

            // Where the current point is
            DrawLineAt(PEN_CURSOR, WIDTH_CURSOR, 0);
            g.TranslateTransform(-centerX, -centerY);
        }

        #region Sprites
        private static readonly Sprite SpGCDualTapNote = new Sprite(Properties.Resources.GCDualTapNote);
        private static readonly Sprite SPGCTapNote = new Sprite(Properties.Resources.GCTapNote);
        #endregion

        #region Drawing constants
        private static readonly int WIDTH_SEGMENT = 100;
        private static readonly int WIDTH_MEASURE = 80;
        private static readonly int WIDTH_BEAT = 40;

        private static readonly int WIDTH_CURSOR = 40;
        private static readonly int DRAW_MARGIN = 100;
        private static readonly float LOOKBEHIND_AMOUNT = 4.0f;
        #endregion

        #region Pens for drawing
        private static readonly Pen PEN_CENTERLINE = new Pen(Color.Gray, 1.0f);

        private static readonly Pen PEN_SEGMENT = new Pen(Color.Green, 2.0f);
        private static readonly Pen PEN_MEASURE = new Pen(Color.Black, 1.0f);
        private static readonly Pen PEN_BEAT = new Pen(Color.Gray, 1.0f);

        private static readonly Pen PEN_CURSOR = new Pen(Color.Red, 0.5f);
        #endregion
    }
}
