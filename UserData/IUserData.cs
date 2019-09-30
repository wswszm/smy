using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace rect7.UserData
{
    public interface IUserData
    {
        bool Selected { set; get; }
        void Draw(Graphics g, Rectangle rect);
    }
}
