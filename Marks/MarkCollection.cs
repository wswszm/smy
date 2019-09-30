using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rect7.Shapes;
using System.Windows.Forms;

namespace rect7.Marks
{
    public class MarkCollection : List<Mark>
    {
        Shape shape;
        public MarkCollection(Shape parentShape)
        {
            shape = parentShape;
        }

        public void AddRange(params SizeMark[] marks)
        {
            base.AddRange(marks);
        }
    }
}
