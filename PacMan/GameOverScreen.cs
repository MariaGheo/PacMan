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
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new MenuScreen());
        }
    }
}
