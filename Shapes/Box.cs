using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace rect7.Shapes
{
    public class Box : Shape
    {
        public Box(bool enable) : base(enable)
        {
        }

        /*public override void DrawShape(Graphics g)
        {
            g.FillRectangle(Brushes.Transparent, this.Rectangle);
            Pen pen = new Pen(Color.Red);
            pen.Width = 2;
            g.DrawRectangle(pen, this.Rectangle);
        }*/

        public override void DrawShape(Graphics g, Pen pen)
        {
            g.DrawRectangle(pen, this.Rectangle);
        }

        public override bool In(int x, int y)
        {
            return this.Rectangle.Contains(x, y);
        }
    }
}
