using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rect7.Shapes;
using System.Drawing;
using System.Windows.Forms;

namespace rect7.Marks
{
    public abstract class Mark
    {
        public Mark(Shape shape, Cursor cursor)
        {
            // TODO: Complete member initialization
            this.shape = shape;
            this.cursor = cursor;
        }

        private Shape shape;

        private Cursor cursor;

        public Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }

        public Shape Shape
        {
            get { return shape; }
        }


        public void MoveTo(Point positionInCanvas)
        {
            if (shape != null)
            {
                double newX = shape.Rectangle.Width == 0 ? 0 :
                    (positionInCanvas.X - shape.Rectangle.Left) * 1.0 / shape.Rectangle.Width;
                double newY = shape.Rectangle.Height == 0 ? 0 :
                    (positionInCanvas.Y - shape.Rectangle.Top) * 1.0 / shape.Rectangle.Height;

                bool cancel = false;
                shape.OnMarkMoving(this, ref newX, ref newY);

                if (!cancel)
                {
                    this.X = newX;
                    this.Y = newY;
                }
            }
        }

        private int markSize = 8;

        public int MarkSize
        {
            get { return markSize; }
            protected set { markSize = value; }
        }



        public virtual bool In(int x, int y)
        {
            Point cp = this.CenterPoint;
            if (Math.Abs(x - cp.X) < markSize / 2 &&
                Math.Abs(y - cp.Y) < markSize / 2)
                return true;
            return false;
        }


        public double Y { get; internal set; }
        public double X { get; internal set; }


        public Point CenterPoint
        {
            get
            {
                Rectangle rect = shape.Rectangle;
                return new Point
                    ((int)(this.X * rect.Width + rect.Left + 0.5),
                    (int)(this.Y * rect.Height + rect.Top + 0.5));
            }
        }

        public abstract void Draw(Graphics g);
    }
}
