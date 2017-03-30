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

        bool rp2;
        bool lp2;


        bool jump;
        int G = 30;
        int force;

        public Form1()
        {
            InitializeComponent();
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



            if (rp2 == true) { player2.Left += 5; }
            if (lp2 == true) { player2.Left -= 5; }
            if (jump == true)
            {
                player2.Top -= force;
                force -= 1;
            }
            if (player2.Top + player2.Height >= field.Height)
            {
                player2.Top = field.Height - player2.Height;
                jump = false;
            }
            else
            {
                player2.Top += 5;

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right) { right = true; }
            if(e.KeyCode == Keys.Left) { left = true; }

            if (e.KeyCode == Keys.D) { rp2 = true; }
            if (e.KeyCode == Keys.A) { lp2 = true; }

            if (e.KeyCode == Keys.Escape) { this.Close(); } //Escape => Exit

            if (jump != true)
            {
                if (e.KeyCode == Keys.Space)
                    jump = true;
                force = 30;
                        }
            if (jump != true)
            {
                if (e.KeyCode == Keys.W)
                    jump = true;
                force = 30;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right) { right = false; }
            if(e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D) { rp2 = false; }
            if (e.KeyCode == Keys.A) { lp2 = false; }
        }
    }
}
