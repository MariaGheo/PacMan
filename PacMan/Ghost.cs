using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    internal class Ghost
    {
        public int x, y;
        public int size = 14;
        public int speed = 2;
        public int onTurnPointIndex = 0;
        public bool onTurnPoint = false;
        public string direction;
        public Pen pen;
        public SolidBrush solidBrush;
        public Point[] points = new Point[9];

        public Ghost (int _x, int _y, string _direction, Pen _pen, SolidBrush _solidBrush)
        {
            x = _x;
            y = _y;
            direction = _direction;
            pen = _pen;
            solidBrush = _solidBrush;
            points = new Point[] { new Point(x + 1, y + 6), new Point(x + 1, y + size), new Point(x + 3, y + size - 2), new Point(x + 5, y + size), new Point(x + 7, y + size - 2), new Point(x + 9, y + size), new Point(x + 11, y + size - 2), new Point(x + 13, y + size), new Point(x + 13, y + 6) };
        }

        public bool IntersectsWith(TurnPoint turnPoint)
        {
            bool intersection = false;

            Rectangle turnArea = new Rectangle(turnPoint.x, turnPoint.y, turnPoint.size, turnPoint.size);
            Rectangle midPoint = new Rectangle(size / 2 + x, size / 2 + y, 1, 1);

            if (turnArea.Contains(midPoint))
            {
                intersection = true;
            }

            return intersection;
        }

        public void Move()
        {
            switch (direction)
            {
                case "up":
                    y -= speed;
                    break;
                case "down":
                    y += speed;
                    break;
                case "left":
                    x -= speed;
                    break;
                case "right":
                    x += speed;
                    break;
            }

            // Update points array
            points = new Point[] { new Point(x + 1, y + 6), new Point(x + 1, y + size), new Point(x + 3, y + size - 2), new Point(x + 5, y + size), new Point(x + 7, y + size - 2), new Point(x + 9, y + size), new Point(x + 11, y + size - 2), new Point(x + 13, y + size), new Point(x + 13, y + 6) };
        }
    }
}
