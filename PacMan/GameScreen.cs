using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace PacMan
{
    public partial class GameScreen : UserControl
    {
        Player hero;
        int biteTimer;
        bool mouthOpen;

        List<Ghost> ghosts = new List<Ghost>();

        public static int score;

        Random randGen = new Random();

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
            new TurnPoint(15, 70, false, true, false, true), new TurnPoint(75, 70, false, true, true, true), new TurnPoint(147, 70, false, true, true, false), new TurnPoint(183, 70, false, true, false, true), new TurnPoint(255, 70, false, true, true, true), new TurnPoint(315, 70, false, true, true, false), new TurnPoint(15, 120, true, true, false, true), new TurnPoint(75, 120, true, true, true, true), new TurnPoint(110, 120, false, true, true, true), new TurnPoint(147, 120, true, false, true, true), new TurnPoint(183, 120, true, false, true, true), new TurnPoint(220, 120, false, true, true, true), new TurnPoint(255, 120, true, true, true, true), new TurnPoint(315, 120, true, true, true, false), new TurnPoint(15, 155, true, false, false, true), new TurnPoint(75, 155, true, true, true, false), new TurnPoint(110, 155, true, false, false, true), new TurnPoint(147, 155, false, true, true, false), new TurnPoint(183, 155, false, true, false, true), new TurnPoint(220, 155, true, false, true, false), new TurnPoint(255, 155, true, true, false, true), new TurnPoint(315, 155, true, false, true, false), new TurnPoint(110, 190, false, true, false, true), new TurnPoint(147, 190, true, false, true, true), new TurnPoint(183, 190, true, false, true, true), new TurnPoint(220, 190, false, true, true, false), new TurnPoint(75, 225, true, true, true, true), new TurnPoint(110, 225, true, true, true, false), new TurnPoint(220, 225, true, true, false, true), new TurnPoint(255, 225, true, true, true, true), new TurnPoint(110, 260, true, true, false, true), new TurnPoint(220, 260, true, true, true, false), new TurnPoint(15, 295, false, true, false, true), new TurnPoint(75, 295, true, true, true, true), new TurnPoint(110, 295, true, false, true, true), new TurnPoint(147, 295, false, true, true, false), new TurnPoint(183, 295, false, true, false, true), new TurnPoint(220, 295, true, false, true, true), new TurnPoint(255, 295, true, true, true, true), new TurnPoint(315, 295, false, true, true, false), new TurnPoint(15, 332, true, false, false, true), new TurnPoint(40, 332, false, true, true, false), new TurnPoint(75, 332, true, true, false, true), new TurnPoint(110, 332, false, true, true, true), new TurnPoint(147, 332, true, false, true, true), new TurnPoint(183, 332, true, false, true, true), new TurnPoint(220, 332, false, true, true, true), new TurnPoint(255, 332, true, true, true, false), new TurnPoint(290, 332, false, true, false, true), new TurnPoint(315, 332, true, false, true, false), new TurnPoint(15, 368, false, true, false, true), new TurnPoint(40, 368, true, false, true, true), new TurnPoint(75, 368, true, false, true, false), new TurnPoint(110, 368, true, false, false, true), new TurnPoint(147, 368, false, true, true, false), new TurnPoint(183, 368, false, true, false, true), new TurnPoint(220, 368, true, false, true, false), new TurnPoint(255, 368, true, false, false, true), new TurnPoint(290, 368, true, false, true, true), new TurnPoint(315, 368, false, true, true, false), new TurnPoint(15, 405, true, false, false, true), new TurnPoint(147, 405, true, false, true, true), new TurnPoint(183, 405, true, false, true, true), new TurnPoint(315, 405, true, false, true, false), new TurnPoint(165, 332, false, false, true, true), new TurnPoint(165, 192, false, false, true, true)
        };

        List<List<PointDot>> pointDotsList = new List<List<PointDot>>();

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            // Set all values
            gameEngine.Enabled = true;
            hero = new Player(160, 325, "none");
            ghosts = new List<Ghost>(new Ghost[] {
                new Ghost(160, 185, "none", new Pen(Color.Red), new SolidBrush(Color.Red)), new Ghost(160, 185, "none", new Pen(Color.Turquoise), new SolidBrush(Color.Turquoise)), new Ghost(160, 185, "none", new Pen(Color.Pink), new SolidBrush(Color.Pink)), new Ghost(160, 185, "none", new Pen(Color.Orange), new SolidBrush(Color.Orange))
            });
            biteTimer = 0;
            mouthOpen = false;
            newDirection = "none";
            score = 0;
            scoreLabel.Text = "0";

            pointDotsList.Clear();

            // This list and the following foreach loop just makes it so I don't have to write out the coordinates of each point dot. Instead, they are created in sections
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
            // Update the player's direction
            if (newDirection != "none") // if the player is pressing a move button
            {
                // If the direction the player is trying to move to is the same or opposite of the current movement...
                if (((newDirection == "up" || newDirection == "down") && (hero.direction == "up" || hero.direction == "down")) || ((newDirection == "left" || newDirection == "right") && (hero.direction == "left" || hero.direction == "right")))
                {
                    if (!CheckTurnPoints(true, hero.direction, hero)) // If the player is at a turn point where they can't go their desired direction...
                    {
                        hero.direction = "none"; // Stop moving the player
                        mouthOpen = false; // Close pacman's mouth
                        biteTimer = 0; // Reset the bite timer
                    }
                    else // Otherwise, set the hero to move in the new direction
                    {
                        hero.direction = newDirection;
                    }
                }
                else if (CheckTurnPoints(false, newDirection, hero)) // If the player is trying to move in a new direction that isn't opposite to where they are currently moving, and if the player can go that way, set the player's direction to the new direction
                {
                    hero.direction = newDirection;
                }
            }
            else if (!CheckTurnPoints(true, hero.direction, hero))
            {
                hero.direction = "none";
                biteTimer = 0;
                mouthOpen = false;
            }

            // Move player
            hero.Move();

            // If the player is now off the screen (by travelling through the tunnel), move the player to the other end of the tunnel
            if (hero.x < 0 - hero.size)
            {
                hero.x = 336;
            }
            else if (hero.x > 336)
            {
                hero.x = 0 - hero.size;
            }

            // Mouth animation (only when the player is moving)
            if (hero.direction != "none")
            {
                biteTimer++; // Increase bite timer

                if (biteTimer == 5) // Open/close mouth
                {
                    mouthOpen = !mouthOpen;
                    biteTimer = 0;
                }
            }

            // Determine ghost direction and move the ghost accordingly, then check if the ghost got to the player
            foreach (Ghost ghost in ghosts)
            {
                // To track the index of the turn points
                int index = 0;

                // Go through turn points and figure out if it should change direction
                foreach (TurnPoint turnPoint in turnPoints)
                {
                    if (ghost.IntersectsWith(turnPoint) && !ghost.onTurnPoint) // If it intersects with this turn point and it wasn't already on a turn point
                    {
                        ghost.onTurnPoint = true;
                        ghost.onTurnPointIndex = index;

                        List<String> possibleDirections = new List<String>();

                        if (turnPoint.up)
                        {
                            possibleDirections.Add("up");
                        }

                        if (turnPoint.down)
                        {
                            possibleDirections.Add("down");
                        }

                        if (turnPoint.left)
                        {
                            possibleDirections.Add("left");
                        }

                        if (turnPoint.right)
                        {
                            possibleDirections.Add("right");
                        }

                        // Variable to store what direction is opposite of where the ghost's current direction
                        String oppositeDirection = "";

                        // Store the direction opposite of the ghost's current direction
                        switch (ghost.direction)
                        {
                            case "up":
                                oppositeDirection = "down";
                                break;
                            case "down":
                                oppositeDirection = "up";
                                break;
                            case "left":
                                oppositeDirection = "right";
                                break;
                            case "right":
                                oppositeDirection = "left";
                                break;
                        }

                        // Remove the opposite direction (so that the ghost doesn't go backwards)
                        possibleDirections.Remove(oppositeDirection);

                        // Randomly set the ghost's direction to one of the possible directions
                        ghost.direction = possibleDirections[randGen.Next(0, possibleDirections.Count())];

                        break;
                    }
                    else if (ghost.onTurnPoint && !ghost.IntersectsWith(turnPoint) && ghost.onTurnPointIndex == index) // Update if this is the turn point it should be on, but this is no longer true
                    {
                        ghost.onTurnPoint = false;
                    }

                    index++;
                }

                // Move the ghosts
                ghost.Move();

                // If the ghost is now off the screen (by travelling through the tunnel), move the ghost to the other end of the tunnel
                if (ghost.x < 0 - ghost.size)
                {
                    ghost.x = 336;

                    ghost.points = new Point[] { new Point(ghost.x + 1, ghost.y + 6), new Point(ghost.x + 1, ghost.y + ghost.size), new Point(ghost.x + 3, ghost.y + ghost.size - 2), new Point(ghost.x + 5, ghost.y + ghost.size), new Point(ghost.x + 7, ghost.y + ghost.size - 2), new Point(ghost.x + 9, ghost.y + ghost.size), new Point(ghost.x + 11, ghost.y + ghost.size - 2), new Point(ghost.x + 13, ghost.y + ghost.size), new Point(ghost.x + 13, ghost.y + 6) };
                }
                else if (ghost.x > 336)
                {
                    ghost.x = 0 - ghost.size;

                    ghost.points = new Point[] { new Point(ghost.x + 1, ghost.y + 6), new Point(ghost.x + 1, ghost.y + ghost.size), new Point(ghost.x + 3, ghost.y + ghost.size - 2), new Point(ghost.x + 5, ghost.y + ghost.size), new Point(ghost.x + 7, ghost.y + ghost.size - 2), new Point(ghost.x + 9, ghost.y + ghost.size), new Point(ghost.x + 11, ghost.y + ghost.size - 2), new Point(ghost.x + 13, ghost.y + ghost.size), new Point(ghost.x + 13, ghost.y + 6) };
                }

                // If the ghost reached the player, end game
                if (hero.IntersectsWith(ghost))
                {
                    gameEngine.Enabled = false;
                    GameOverScreen.won = false;

                    SoundPlayer player = new SoundPlayer(Properties.Resources.death);
                    player.Play();

                    Form1.ChangeScreen(this, new GameOverScreen());
                }
            }

            // Check if the player collected any points
            foreach (List<PointDot> pointDots in pointDotsList)
            {
                foreach (PointDot pointDot in pointDots)
                {
                    if (hero.IntersectsWith(pointDot))
                    {
                        pointDots.Remove(pointDot);
                        score += 10;
                        scoreLabel.Text = $"{score}";

                        if (score == 2440) // End game if the player collects all point dots 2440
                        {
                            gameEngine.Enabled = false;
                            GameOverScreen.won = true;

                            SoundPlayer player = new SoundPlayer(Properties.Resources.win);
                            player.Play();

                            Form1.ChangeScreen(this, new GameOverScreen());
                        }

                        break;
                    }
                }
            }

            Refresh();
        }

        // Method for painting objects to screen
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Paint the walls
            foreach (Point[] wall in walls1)
            {
                e.Graphics.DrawPolygon(bluePen, wall);
            }

            foreach (Rectangle wall in walls2)
            {
                e.Graphics.DrawRectangle(bluePen, wall);
            }

            // Paint the white point dots
            foreach (List<PointDot> pointDots in pointDotsList)
            {
                foreach (PointDot pointDot in pointDots)
                {
                    e.Graphics.DrawRectangle(whitePen, pointDot.x, pointDot.y, pointDot.size, pointDot.size);
                    e.Graphics.FillRectangle(whiteBrush, pointDot.x, pointDot.y, pointDot.size, pointDot.size);
                }
            }

            // Paint the player
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

            // Paint the ghosts
            foreach(Ghost ghost in ghosts)
            {
                e.Graphics.DrawEllipse(ghost.pen, ghost.x + 1, ghost.y, ghost.size - 2, ghost.size - 2);
                e.Graphics.FillEllipse(ghost.solidBrush, ghost.x + 1, ghost.y, ghost.size - 2, ghost.size - 2);
                e.Graphics.DrawPolygon(ghost.pen, ghost.points);
                e.Graphics.FillPolygon(ghost.solidBrush, ghost.points);
            }
        }

        // Method for tracking when the player presses a key
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

        // Method for tracking when the player releases a key
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

        // Method for creating all the point dots
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

        // Check if the object intersects with any of the turn points. If it does, it returns whether or not it can move in the desired direction. If it doesn't, it returns the set default value (bool b)
        public bool CheckTurnPoints(bool b, string direction, object obj)
        {
            bool b2 = b; // Set it to given default value
            
            if (obj is Player) // Check if it's the player. If not, then it's a ghost.
            {
                Player player = (Player)obj;

                foreach (TurnPoint turnPoint in turnPoints) // Go through turnPoints and check if the player intersects with one of them
                {
                    if (player.IntersectsWith(turnPoint))
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
            }
            else
            {
                Ghost ghost = (Ghost)obj;

                foreach (TurnPoint turnPoint in turnPoints) // Go through turnPoints and check if the ghost intersects with one of them
                {
                    if (ghost.IntersectsWith(turnPoint))
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
            }

            return b2;
        }
    }
}
