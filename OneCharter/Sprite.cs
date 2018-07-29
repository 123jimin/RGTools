using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OneCharter {
    /// <summary>
    /// Sprite image
    /// </summary>
    public class Sprite {
        protected Bitmap image = null;
        public Bitmap Image { get => image; }

        protected PointF center;
        public PointF Center { get => center; }

        public Sprite() : this(new Bitmap(1, 1)) {}
        public Sprite(Bitmap image): this(image, image.Width/2f, image.Height/2f) {}
        public Sprite(Bitmap image, float cx, float cy) : this(image, new PointF(cx, cy)) {}
        public Sprite(Bitmap image, PointF center) {
            this.center = center;
            this.image = image;
        }

        public void DrawOn(Graphics g, float x, float y) {
            g.DrawImage(image, x - center.X, y - center.Y);
        }

        public static Sprite Empty = new Sprite();
    }
}
