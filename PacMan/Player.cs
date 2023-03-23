using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacMan
{
    internal class Player
    {
        public int x, y;
        public int size = 14;
        public int speed = 2;
        public string direction;

        public Player(int _x, int _y, string _direction)
        {
            x = _x;
            y = _y;
            direction = _direction;
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

        public bool IntersectsWith(PointDot pointDot)
        {
            bool intersection = false;

            Rectangle point = new Rectangle(pointDot.x, pointDot.y, pointDot.size, pointDot.size);
            Rectangle playerArea = new Rectangle(x, y, size, size);

            if (playerArea.Contains(point))
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
        }
    }
}
