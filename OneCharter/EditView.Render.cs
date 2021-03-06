﻿using System;
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

        /// <summary>Draws the current chart to the graphics buffer.</summary>
        public void Paint() {
            lock (gLock) {
                Graphics graphics = bufferedGraphics.Graphics;
                DrawChartOn(graphics);
            }
            viewPanel.Invalidate();
        }

        /// <summary>Draw the chart on a graphics.</summary>
        /// <param name="g">The graphics which the chart will be drawn on.</param>
        protected void DrawChartOn(Graphics g) {
            float centerX = viewPanel.Width / 2;
            float centerY = viewPanel.Height - PixelPerQuad * LOOKBEHIND_AMOUNT;
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

            foreach (Segment segment in chartFile.Chart.Segments) {
                DrawLine(PEN_SEGMENT, WIDTH_SEGMENT);

                bool isFirstMeasure = true;
                foreach (Measure measure in segment.Measures) {
                    // Draw the beginnng point of the measure
                    if (isFirstMeasure) {
                        DrawLine(PEN_MEASURE, WIDTH_MEASURE);
                        isFirstMeasure = false;
                    }
                    // Draw the beatlines and elemenets
                    float beatInterval;
                    switch (measure) {
                        case BeatMeasure bMeasure:
                            beatInterval = PixelPerQuad * 4.0f / bMeasure.QuantBeat;
                            break;
                        case OffsetMeasure oMeasure:
                            beatInterval = PixelPerQuad * (float)(oMeasure.UnitLength / segment.MSPQ);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    for (int i = 1; i < measure.TotalBeats; i++) {
                        if (i % measure.GroupBeats == 0) {
                            DrawLineAt(PEN_MEASURE, WIDTH_MEASURE, measureStartY - beatInterval * i);
                        } else {
                            DrawLineAt(PEN_BEAT, WIDTH_BEAT, measureStartY - beatInterval * i);
                        }
                    }
                    // Draw the elemenets
                    foreach (var elementTuple in measure.Elements) {
                        GetSprite(elementTuple.Item2).DrawOn(g, 0, measureStartY - beatInterval * elementTuple.Item1);
                    }
                    measureStartY -= PixelPerQuad * (float) segment.QuadLengthOf(measure);
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
