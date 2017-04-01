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
        int isPressed = 0;
        public Form1()
        {
            InitializeComponent();
            GameAPI game = new Game();
            List<Coordinates> crd = game.getAllUnitsCoordinates();
            int x = crd.Count;
            Console.WriteLine("x = {0}", x);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Interlocked.Exchange(ref isPressed, 1);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Interlocked.Exchange(ref isPressed, 0);
        }
    }
}
