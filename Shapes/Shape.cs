using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rect7.Marks;
using System.Drawing;
using System.Windows.Forms;

namespace rect7.Shapes
{
    public abstract class Shape
    {
        SizeMark topLeftMark;
        SizeMark bottomRightMark;
        Boolean enable;

        public Shape(Boolean enable)
        {
            this.enable = enable;
            marks = new MarkCollection(this);

            topLeftMark = new SizeMark(this, Cursors.SizeNWSE) { X = 0, Y = 0 };
            bottomRightMark = new SizeMark(this, Cursors.SizeNWSE) { X = 1, Y = 1 };

            this.marks.AddRange(topLeftMark, bottomRightMark);
        }
        public Boolean getEnable()
        {
            return this.enable;
        }

        protected virtual Region GetUserDataRegion()
        {
            return new Region(this.rectangle);
        }

        public event EventHandler RectangleChanged = null;

        private Rectangle rectangle = Rectangle.Empty;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set
            {
                if (rectangle != value)
                {
                    rectangle = value;
                    RaiseRectangleChanged();
                }
            }
        }

        protected void RaiseRectangleChanged()
        {
            if (RectangleChanged != null)
                RectangleChanged(this, EventArgs.Empty);
        }

        private MarkCollection marks;
        public MarkCollection Marks
        {
            get { return marks; }
        }


        public virtual void OnMarkMoving(Mark mark,
            ref double newX, ref double newY)
        {
            Rectangle newRect = Rectangle.Empty;
            int x = (int)(newX * Rectangle.Width + 0.5);
            int y = (int)(newY * Rectangle.Height + 0.5);

            if (mark == this.topLeftMark)
            {
                newRect = new Rectangle(
                    Rectangle.Left + x,
                    Rectangle.Top + y,
                    Rectangle.Width - x,
                    Rectangle.Height - y);

                newX = newY = 0;
            }
            else if (mark == this.bottomRightMark)
            {
                newRect = new Rectangle(
                    Rectangle.Left,
                    Rectangle.Top,
                    x,
                    y);
                newX = newY = 1;
            }

            if (newRect.Width < 4 ||
                newRect.Height < 4)
            {
                newX = mark.X;
                newY = mark.Y;
                return;
            }

            this.Rectangle = newRect;
        }

        public void Draw(Graphics g, Pen pen)
        {
            DrawShape(g, pen);

            g.Clip = this.GetUserDataRegion();
            DrawUserData(g);
            g.Clip = new Region();
        }

        //public abstract void DrawShape(Graphics g);

        public abstract void DrawShape(Graphics g,Pen pen);

        public void DrawMark(Graphics g)
        {
            foreach (Mark m in this.marks)
                m.Draw(g);
        }

        public void DrawUserData(Graphics g)
        {
            if (UserData != null)
                UserData.Draw(g, this.Rectangle);
        }

        public Canvas Canvas
        {
            get;
            set;
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (selected && this.Canvas != null)
                {
                    foreach (Shape s in Canvas.Shapes)
                        if (s != this)
                            s.Selected = false;
                }
                if (UserData != null)
                    UserData.Selected = selected;
            }
        }

        public abstract bool In(int x, int y);

        public UserData.IUserData UserData { set; get; }

        public void MoveTo(Point point)
        {
            this.Rectangle = new Rectangle(
                point, Rectangle.Size);
        }
    }
}
