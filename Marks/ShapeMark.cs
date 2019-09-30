using rect7.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rect7.Marks
{
    public class ShapeMark : Mark
    {
        public ShapeMark(Shape shape)
            : base(shape, Cursors.Hand)
        { 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            Point cp = this.CenterPoint;
            Rectangle rect = new Rectangle(
                cp.X - MarkSize / 2,
                cp.Y - MarkSize / 2,
                MarkSize, MarkSize);
            g.FillEllipse(Brushes.Orange, rect);
            g.DrawEllipse(Pens.Brown, rect);
        }
    }
}
