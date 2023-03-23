using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class GameScreen : UserControl
    {
        Player hero;
        int biteTimer;
        bool mouthOpen;

        int score;

        //Random randGen = new Random();

        string newDirection;

        Pen bluePen = new Pen(Color.Blue, 1);
        Pen yellowPen = new Pen(Color.Yellow, 1);
        Pen whitePen = new Pen(Color.White, 1);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Point[][] walls1 =
        {
            new Point[] { new Point(-1, 210), new Point(60, 210), new Point(60, 175), new Point(0, 175), new Point(0, 55), new Point(335, 55), new Point(335, 175), new Point(275, 175), new Point(275, 210), new Point(336, 210), new Point(336, 215), new Point(270, 215), new Point(270, 170), new Point(330, 170), new Point(330, 60), new Point(173, 60), new Point(173, 110), new Point(162, 110), new Point(162, 60), new Point(5, 60), new Point(5, 170), new Point(65, 170), new Point(65, 215), new Point(-1, 215) },
            new Point[] { new Point(-1, 245), new Point(60, 245), new Point(60, 280), new Point(0, 280), new Point(0, 425), new Point(335, 425), new Point(335, 280), new Point(275, 280), new Point(275, 245), new Point(336, 245), new Point(336, 240), new Point(270, 240), new Point(270, 285), new Point(330, 285), new Point(330, 347), new Point(305, 347), new Point(305, 358), new Point(330, 358), new Point(330, 420), new Point(5, 420), new Point(5, 358), new Point(30, 358), new Point(30, 347), new Point(5, 347), new Point(5, 285), new Point(65, 285), new Point(65, 240), new Point(-1, 240) },
            new Point[] { new Point(90, 135), new Point(100, 135), new Point(100, 170), new Point(137, 170), new Point(137, 180), new Point(100, 180), new Point(100, 215), new Point(90, 215) },
            new Point[] { new Point(90, 347), new Point(100, 347), new Point(100, 383), new Point(137, 383), new Point(137, 393), new Point(30, 393), new Point(30, 383), new Point(90, 383) },
            new Point[] { new Point(245, 135), new Point(235, 135), new Point(235, 170), new Point(198, 170), new Point(198, 180), new Point(235, 180), new Point(235, 215), new Point(245, 215) },
            new Point[] { new Point(245, 347), new Point(235, 347), new Point(235, 383), new Point(198, 383), new Point(198, 393), new Point(305, 393), new Point(305, 383), new Point(245, 383) },
            new Point[] { new Point(125, 135), new Point(210, 135), new Point(210, 145), new Point(173, 145), new Point(173, 180), new Point(162, 180), new Point(162, 145), new Point(125, 145) },
            new Point[] { new Point(125, 275), new Point(210, 275), new Point(210, 285), new Point(173, 285), new Point(173, 320), new Point(162, 320), new Point(162, 285), new Point(125, 285) },
            new Point[] { new Point(125, 347), new Point(210, 347), new Point(210, 358), new Point(173, 358), new Point(173, 393), new Point(162, 393), new Point(162, 358), new Point(125, 358) },
            new Point[] { new Point(30, 310), new Point(65, 310), new Point(65, 358), new Point(55, 358), new Point(55, 320), new Point(30, 320) },
            new Point[] { new Point(305, 310), new Point(270, 310), new Point(270, 358), new Point(280, 358), new Point(280, 320), new Point(305, 320) }
        };

        Rectangle[] walls2 =
        {
            new Rectangle(30, 85, 35, 25), new Rectangle(90, 85, 47, 25), new Rectangle(30, 135, 35, 10), new Rectangle(90, 240, 10, 45), new Rectangle(90, 310, 47, 10), new Rectangle(198, 85, 47, 25), new Rectangle(270, 85, 35, 25), new Rectangle(270, 135, 35, 10), new Rectangle(235, 240, 10, 45), new Rectangle(198, 310, 47, 10), new Rectangle(125, 205, 85, 45)
        };

        TurnPoint[] turnPoints = 
        {
            new TurnPoint(15, 70, false, true, false, true), new TurnPoint(75, 70, false, true, true, true), new TurnPoint(147, 70, false, true, true, false), new TurnPoint(183, 70, false, true, false, true), new TurnPoint(255, 70, false, true, true, true), new TurnPoint(315, 70, false, true, true, false), new TurnPoint(15, 120, true, true, false, true), new TurnPoint(75, 120, true, true, true, true), new TurnPoint(110, 120, false, true, true, true), new TurnPoint(147, 120, true, false, true, true), new TurnPoint(183, 120, true, false, true, true), new TurnPoint(220, 120, false, true, true, true), new TurnPoint(255, 120, true, true, true, true), new TurnPoint(315, 120, true, true, true, false), new TurnPoint(15, 155, true, false, false, true), new TurnPoint(75, 155, true, true, true, false), new TurnPoint(110, 155, true, false, false, true), new TurnPoint(147, 155, false, true, true, false), new TurnPoint(183, 155, false, true, false, true), new TurnPoint(220, 155, true, false, true, false), new TurnPoint(255, 155, true, true, false, true), new TurnPoint(315, 155, true, false, true, false), new TurnPoint(110, 190, false, true, false, true), new TurnPoint(147, 190, true, false, true, true), new TurnPoint(183, 190, true, false, true, true), new TurnPoint(220, 190, false, true, true, false), new TurnPoint(75, 225, true, true, true, true), new TurnPoint(110, 225, true, true, true, false), new TurnPoint(220, 225, true, true, false, true), new TurnPoint(255, 225, true, true, true, true), new TurnPoint(110, 260, true, true, false, true), new TurnPoint(220, 260, true, true, true, false), new TurnPoint(15, 295, false, true, false, true), new TurnPoint(75, 295, true, true, true, true), new TurnPoint(110, 295, true, false, true, true), new TurnPoint(147, 295, false, true, true, false), new TurnPoint(183, 295, false, true, false, true), new TurnPoint(220, 295, true, false, true, true), new TurnPoint(255, 295, true, true, true, true), new TurnPoint(315, 295, false, true, true, false), new TurnPoint(15, 332, true, false, false, true), new TurnPoint(40, 332, false, true, true, false), new TurnPoint(75, 332, true, true, false, true), new TurnPoint(110, 332, false, true, true, true), new TurnPoint(147, 332, true, false, true, true), new TurnPoint(183, 332, true, false, true, true), new TurnPoint(220, 332, false, true, true, true), new TurnPoint(255, 332, true, true, true, false), new TurnPoint(290, 332, false, true, false, true), new TurnPoint(315, 332, true, false, true, false), new TurnPoint(15, 368, false, true, false, true), new TurnPoint(40, 368, true, false, true, true), new TurnPoint(75, 368, true, false, true, false), new TurnPoint(110, 368, true, false, false, true), new TurnPoint(147, 368, false, true, true, false), new TurnPoint(183, 368, false, true, false, true), new TurnPoint(220, 368, true, false, true, false), new TurnPoint(255, 368, true, false, false, true), new TurnPoint(290, 368, true, false, true, true), new TurnPoint(315, 368, false, true, true, false), new TurnPoint(15, 405, true, false, false, true), new TurnPoint(147, 405, true, false, true, true), new TurnPoint(183, 405, true, false, true, true), new TurnPoint(315, 405, true, false, true, false), new TurnPoint(165, 332, false, false, true, true)
        };

        List<List<PointDot>> pointDotsList = new List<List<PointDot>>();

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            gameEngine.Enabled = true;
            hero = new Player(160, 325, "none");
            biteTimer = 0;
            mouthOpen = false;
            newDirection = "none";
            score = 0;
            scoreLabel.Text = "0";

            pointDotsList.Clear();

            List<PointDot> pointDotStorage = new List<PointDot>(new PointDot[] {
                new PointDot(17, 72, 12, true), new PointDot(185, 72, 12, true), new PointDot(17, 122, 26, true), new PointDot(17, 157, 6, true), new PointDot(112, 157, 4, true), new PointDot(185, 157, 4, true), new PointDot(257, 157, 6, true), new PointDot(17, 297, 12, true), new PointDot(185, 297, 12, true), new PointDot(17, 335, 3, true), new PointDot(77, 335, 7, true), new PointDot(185, 335, 7, true), new PointDot(292, 335, 3, true), new PointDot(17, 370, 6, true), new PointDot(112, 370, 4, true), new PointDot(185, 370, 4, true), new PointDot(257, 370, 6, true), new PointDot(17, 405, 26, true), new PointDot(17, 85, 3, false), new PointDot(77, 85, 3, false),new PointDot(149, 85, 3, false), new PointDot(185, 85, 3, false), new PointDot(257, 85, 3, false), new PointDot(317, 85, 3, false), new PointDot(17, 135, 2, false), new PointDot(77, 135, 2, false), new PointDot(112, 135, 2, false), new PointDot(222, 135, 2, false), new PointDot(257, 135, 2, false), new PointDot(317, 135, 2, false), new PointDot(77, 170, 11, false), new PointDot(257, 170, 11, false), new PointDot(17, 310, 2, false), new PointDot(77, 310, 2, false), new PointDot(149, 310, 2, false), new PointDot(185, 310, 2, false), new PointDot(257, 310, 2, false), new PointDot(317, 310, 2, false), new PointDot(42, 347, 2, false), new PointDot(77, 347, 2, false), new PointDot(112, 347, 2, false), new PointDot(222, 347, 2, false), new PointDot(257, 347, 2, false), new PointDot(292, 347, 2, false), new PointDot(17, 383, 2, false), new PointDot(149, 383, 2, false), new PointDot(185, 383, 2, false), new PointDot(317, 383, 2, false)
            });

            foreach (PointDot pointDot in pointDotStorage)
            {
                CreatePointDots(pointDotStorage.IndexOf(pointDot), pointDot.x, pointDot.y, pointDot.numDots, pointDot.horizontal);
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            if (newDirection != "none")
            {
                //if the direction the player is trying to move to is the same or opposite of the current movement...
                if (newDirection == hero.direction || ((newDirection == "up" || hero.direction == "up") && (newDirection == "down" || hero.direction == "down")) || ((newDirection == "left" || hero.direction == "left") && (newDirection == "right" || hero.direction == "right")))
                {
                    hero.direction = newDirection;

                    if (!CheckTurnPoints(true, hero.direction))
                    {
                        hero.direction = "none";
                        biteTimer = 0;
                        mouthOpen = false;
                    }
                }
                else
                {
                    if (CheckTurnPoints(false, newDirection))
                    {
                        hero.direction = newDirection;
                    }
                }
            }
            else
            {
                if (!CheckTurnPoints(true, hero.direction))
                {
                    hero.direction = "none";
                    biteTimer = 0;
                    mouthOpen = false;
                }
            }

            if (hero.direction != "none")
            {
                biteTimer++;

                if (biteTimer == 5)
                {
                    mouthOpen = !mouthOpen;
                    biteTimer = 0;
                }
            }

            hero.Move();

            foreach (List<PointDot> pointDots in pointDotsList)
            {
                foreach (PointDot pointDot in pointDots)
                {
                    if (hero.IntersectsWith(pointDot))
                    {
                        pointDots.Remove(pointDot);
                        score += 10;
                        scoreLabel.Text = $"{score}";

                        if (score == 2440)
                        {
                            Form1.ChangeScreen(this, new GameOverScreen());
                        }

                        break;
                    }
                }
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Point[] wall in walls1)
            {
                e.Graphics.DrawPolygon(bluePen, wall);
            }

            foreach (Rectangle wall in walls2)
            {
                e.Graphics.DrawRectangle(bluePen, wall);
            }

            foreach (List<PointDot> pointDots in pointDotsList)
            {
                foreach (PointDot pointDot in pointDots)
                {
                    e.Graphics.DrawRectangle(whitePen, pointDot.x, pointDot.y, pointDot.size, pointDot.size);
                    e.Graphics.FillRectangle(whiteBrush, pointDot.x, pointDot.y, pointDot.size, pointDot.size);
                }
            }

            if (mouthOpen)
            {
                switch (hero.direction)
                {
                    case "up":
                        e.Graphics.DrawPie(yellowPen, hero.x, hero.y, hero.size, hero.size, 315, 270);
                        e.Graphics.FillPie(yellowBrush, hero.x, hero.y, hero.size, hero.size, 315, 270);
                        break;
                    case "down":
                        e.Graphics.DrawPie(yellowPen, hero.x, hero.y, hero.size, hero.size, 135, 270);
                        e.Graphics.FillPie(yellowBrush, hero.x, hero.y, hero.size, hero.size, 135, 270);
                        break;
                    case "left":
                        e.Graphics.DrawPie(yellowPen, hero.x, hero.y, hero.size, hero.size, 225, 270);
                        e.Graphics.FillPie(yellowBrush, hero.x, hero.y, hero.size, hero.size, 225, 270);
                        break;
                    case "right":
                        e.Graphics.DrawPie(yellowPen, hero.x, hero.y, hero.size, hero.size, 45, 270);
                        e.Graphics.FillPie(yellowBrush, hero.x, hero.y, hero.size, hero.size, 45, 270);
                        break;
                }
            }
            else
            {
                e.Graphics.DrawEllipse(yellowPen, hero.x, hero.y, hero.size, hero.size);
                e.Graphics.FillEllipse(yellowBrush, hero.x, hero.y, hero.size, hero.size);
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    newDirection = "up";
                    break;
                case Keys.S:
                    newDirection = "down";
                    break;
                case Keys.A:
                    newDirection = "left";
                    break;
                case Keys.D:
                    newDirection = "right";
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            string direction = "none";

            switch (e.KeyCode)
            {
                case Keys.W:
                    direction = "up";
                    break;
                case Keys.S:
                    direction = "down";
                    break;
                case Keys.A:
                    direction = "left";
                    break;
                case Keys.D:
                    direction = "right";
                    break;
            }

            if (direction == newDirection)
            {
                newDirection = "none";
            }
        }

        public void CreatePointDots(int index, int x, int y, int numDots, bool horizontal)
        {
            pointDotsList.Add(new List<PointDot>());
            
            if (horizontal)
            {
                for (int i = 0; i < numDots; i++)
                {
                    pointDotsList[index].Add(new PointDot(x + 12 * i, y));
                }
            }
            else
            {
                for (int i = 0; i < numDots; i++)
                {
                    pointDotsList[index].Add(new PointDot(x, y + 12 * i));
                }
            }
        }

        public bool CheckTurnPoints(bool b, string direction)
        {
            bool b2 = b;

            foreach (TurnPoint turnPoint in turnPoints)
            {
                if (hero.IntersectsWith(turnPoint))
                {
                    switch (direction)
                    {
                        case "up":
                            b2 = turnPoint.up;
                            break;
                        case "down":
                            b2 = turnPoint.down;
                            break;
                        case "left":
                            b2 = turnPoint.left;
                            break;
                        case "right":
                            b2 = turnPoint.right;
                            break;
                    }

                    break;
                }
            }

            return b2;
        }
    }
}
