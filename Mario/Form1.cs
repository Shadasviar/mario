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
using System.Reflection;

namespace Mario
{
    public partial class Form1 : Form
    {
        delegate void updateStateDelegate();
        List<PictureBox> sprites = new List<PictureBox>();
        int isPressed = 0;
        GameAPI game = new Game();
        int fps = 1000/25; //ms
        public Form1()
        {
            
            InitializeComponent();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    game.nextFrame();
                   Invoke(new updateStateDelegate(this.updateState));
                    Thread.Sleep(fps); // 25 fps
                }
            }).Start();
            /* For disable flicking*/
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic,
               null, panel1, new object[] { true });

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Interlocked.Exchange(ref isPressed, 1);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Interlocked.Exchange(ref isPressed, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateState();
        }

        private void updateState()
        {
            this.panel1.Controls.Clear();
            List<Coordinates> crd = game.getAllUnitsCoordinates();

            foreach (Coordinates c in crd)
            {
                PictureBox p = new PictureBox();
                p.Size = mapSize(c);
                p.Image = new Bitmap(Mario.Properties.Resources.cegla);
                p.Location = mapPosition(c);
                sprites.Add(p);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                this.panel1.Controls.Add(p);
            }
        }

        /* map position from top left to bottom left*/
        private Point mapPosition(Coordinates p)
        {
            Point res = new Point();
            res.X = p.bottomLeft.X;
            res.Y = this.panel1.Height - p.topRight.Y;
            return res;
        }

        private Size mapSize(Coordinates c)
        {
            Size res = new Size();
            res.Width = c.topRight.X - c.bottomLeft.X;
            res.Height= c.topRight.Y - c.bottomLeft.Y;
            return res;
        }
    }
}
