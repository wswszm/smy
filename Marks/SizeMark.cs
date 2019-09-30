using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rect7.Shapes;
using System.Drawing;
using System.Windows.Forms;

namespace rect7.Marks
{
    public class SizeMark : Mark
    {
        public SizeMark(Shape shape, Cursor cursor)
            : base(shape, cursor)
        { 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            Point cp = this.CenterPoint;
            Rectangle rect = new Rectangle(
                cp.X - MarkSize / 2,
                cp.Y - MarkSize / 2,
                MarkSize, MarkSize);

            g.FillRectangle(Brushes.Aqua, rect);
            g.DrawRectangle(Pens.Blue, rect);
        }
    }
}
