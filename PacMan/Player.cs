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
        
        // Check if it intersects with a TurnPoint
        public bool IntersectsWith(TurnPoint turnPoint)
        {
            Rectangle turnArea = new Rectangle(turnPoint.x, turnPoint.y, turnPoint.size, turnPoint.size);
            Rectangle midPoint = new Rectangle(size / 2 + x, size / 2 + y, 1, 1);

            return turnArea.Contains(midPoint);
        }

        // Check if it intersects with a PointDot
        public bool IntersectsWith(PointDot pointDot)
        {
            Rectangle point = new Rectangle(pointDot.x, pointDot.y, pointDot.size, pointDot.size);
            Rectangle playerArea = new Rectangle(x, y, size, size);

            return playerArea.Contains(point);
        }

        // Check if it intersects with a Ghost
        public bool IntersectsWith(Ghost ghost)
        {
            Rectangle playerArea = new Rectangle(x, y, size, size);
            Rectangle ghostArea = new Rectangle(ghost.x, ghost.y, ghost.size, ghost.size);

            return playerArea.IntersectsWith(ghostArea);
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
