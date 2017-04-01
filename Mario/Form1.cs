using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        List<PictureBox> sprites = new List<PictureBox>();
        int isPressed = 0;
        public Form1()
        {
            

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    if (isPressed == 1) this.SetText("BUTTON IS PRESSED");
                    else this.SetText("_______");
                    Thread.Sleep(100);
                }
            }).Start();

          
        }

        public delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Interlocked.Exchange(ref isPressed, 1);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Interlocked.Exchange(ref isPressed, 0);
        }

        private void loadPictures()
        {
            InitializeComponent();
            GameAPI game = new Game();
            List<Coordinates> crd = game.getAllUnitsCoordinates();

            foreach (Coordinates c in crd)
            {
                PictureBox p = new PictureBox();
            }
        }

    }
}
