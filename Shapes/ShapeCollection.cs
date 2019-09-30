using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rect7.Shapes
{
    public class ShapeCollection : List<Shape>
    {
        Canvas canvas;

        public ShapeCollection(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public new void Add(Shape shape)
        {
            base.Add(shape);
            shape.Canvas = this.canvas;
        }
    }
}
