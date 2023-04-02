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
    public partial class GameOverScreen : UserControl
    {
        public static bool won;

        public GameOverScreen()
        {
            InitializeComponent();

            if (won)
            {
                titleLabel.Text = "You won!";
            }
            else
            {
                titleLabel.Text = "Try again";
            }

            scoreLabel.Text = $"Score: {GameScreen.score}";
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer(Properties.Resources.beep);
            player.Play();

            Form1.ChangeScreen(this, new MenuScreen());
        }
    }
}
