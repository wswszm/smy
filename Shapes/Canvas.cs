using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using rect7.Marks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace rect7.Shapes
{
    public class Canvas
    {
        public Canvas()
        {
            shapes = new ShapeCollection(this);//形状采集
        }

        ShapeCollection shapes;

        public ShapeCollection Shapes
        {
            get { return shapes; }
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="ctrl"></param>控件
        public void Draw(Control ctrl)
        {
            Graphics g = ctrl.CreateGraphics();
            Draw(g);
            g.Dispose();
        }

        public void Draw(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.HighQuality;

            //g.Clear(Color.Transparent);//wubeijing
            foreach (Shape s in shapes)
            {
                if (s.getEnable())
                {
                    Pen pen = new Pen(Color.Red);
                    pen.Width = 2;
                    s.Draw(g, pen);
                }
                else
                {
                    Pen pen = new Pen(Color.Blue);
                    pen.Width = 2;
                    s.Draw(g, pen);
                }
            }

            foreach (Shape s in shapes)
                if (s.Selected)
                {
                    if (s.getEnable())
                    {
                        s.DrawMark(g);
                    }
                    else
                    {
                        //s.DrawMark(g);
                    }
                }
        }

        public Shape GetShape(int x, int y)
        {
            for (int i = shapes.Count - 1; i >= 0; --i)
                if (shapes[i].In(x, y) && shapes[i].getEnable())
                    return shapes[i];
            return null;
        }

        public void ClearSelection()
        {
            foreach (Shape s in shapes)
                s.Selected = false;
        }

        public Marks.Mark GetMark(int x, int y)
        {
            foreach (Shape s in shapes)
                if (s.Selected)
                    for (int i = s.Marks.Count - 1; i >= 0; i--)
                    {
                        Mark m = s.Marks[i];
                        if (m.In(x, y))
                            return m;
                    }
            return null;
        }

    }
}
