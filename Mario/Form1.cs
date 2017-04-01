using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        bool right;
        bool left;


        bool jump;
        int G = 30;
        int force;

        public Form1()
        {
            InitializeComponent();
            GameAPI game = new Game();
            List<Coordinates> crd = game.getAllUnitsCoordinates();
            int x = crd.Count;
            Console.WriteLine("x = {0}", x);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(right == true) { player.Left += 5; }
            if (left == true) { player.Left -= 5; }

            if(jump == true )
            {
                player.Top -= force;
                force -= 1;
            }
            if(player.Top + player.Height >= field.Height)
                {
                    player.Top = field.Height - player.Height;
                    jump = false;
                }
            else
                {
                     player.Top += 5;

                }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) { right = true; }
            if (e.KeyCode == Keys.Left) { left = true; }

            if (e.KeyCode == Keys.Escape) { this.Close(); } //Escape => Exit

            if (jump != true)
            {
                if (e.KeyCode == Keys.Space)
                    jump = true;
                force = 40;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right) { right = false; }
            if(e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D) { right = false; }
            if (e.KeyCode == Keys.A) { left = false; }
        }


    }
}
