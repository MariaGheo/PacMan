using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    internal class TurnPoint
    {
        public int size = 5;
        public int x, y;
        public bool up, down, left, right;

        public TurnPoint(int _x, int _y, bool _up, bool _down, bool _left, bool _right)
        {
            x = _x;
            y = _y;
            up = _up;
            down = _down;
            left = _left;
            right = _right;
        }
    }
}
