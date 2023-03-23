using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan
{
    internal class PointDot
    {
        public int size;
        public int x, y, numDots;
        public bool horizontal;

        public PointDot(int _x, int _y)
        {
            x = _x;
            y = _y;
            size = 1;
        }

        public PointDot(int _x, int _y, int _numDots, bool _horizontal)
        {
            x = _x;
            y = _y;
            numDots = _numDots;
            horizontal = _horizontal;
        }
    }
}
