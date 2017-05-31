﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using Mario.Properties;

namespace Global
{
    struct Picture
    {
        public Image img;
        public Size size;
        public Point location; 
    }
    public partial class Form1 : Form
    {
        delegate void updateStateDelegate();
        List<Picture> sprites = new List<Picture>();
        GameAPI game;
        private List<int> keys = new List<int>(new int [8]);
        int offset = 0;
        System.Media.SoundPlayer startGame = new System.Media.SoundPlayer("../../Resources/start.wav");
        static bool run = true;
        protected int playerIndex;

        static List<Dictionary<keysType, Keys>> keyAssociatedWithPlayer =
            new List<Dictionary<keysType, Keys>>
        {
            new Dictionary<keysType, Keys>{
                {keysType.Down, Keys.Down},
                {keysType.Jump, Keys.Space},
                {keysType.Right, Keys.Right},
                {keysType.Left, Keys.Left},
            },
            new Dictionary<keysType, Keys>{
                {keysType.Down, Keys.S},
                {keysType.Jump, Keys.W},
                {keysType.Right, Keys.D},
                {keysType.Left, Keys.A},
            },
        };


        public Form1(Object game, ref List<int> keys, int player = 0)
        {
            this.playerIndex = player;
            this.game = (GameAPI)game;
            this.keys = keys;
            InitializeComponent();

            /* For disable flicking*/
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic,
               null, this, new object[] { true });
        }

        void Start_Game()
        {
            run = true;
            while (run)
            {
                game.nextFrame();
                try
                {
                    Invoke(new updateStateDelegate(this.updateState));
                }
                catch (Exception) { };
                Thread.Sleep(1000 / Settings.Default.fps);
            }
        }

        void Stop_Game()
        {
            run = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Down])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Down]] = 1;

            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Right])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Right]] = 1;

            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Left])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Left]] = 1;

            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Jump])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Jump]] = 1;
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Down])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Down]] = 0;

            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Right])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Right]] = 0;

            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Left])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Left]] = 0;

            if (e.KeyCode == keyAssociatedWithPlayer[playerIndex][keysType.Jump])
                keys[(int)Game.keyAssociatedWithPlayer[playerIndex][keysType.Jump]] = 0;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            updateState();
        }


        public void updateState()
        {
            this.Invalidate();
          
            if(game.getPlayerPosition(playerIndex).topRight.X > this.Width/2+offset)
            {
                offset += game.getPlayerPosition(playerIndex).topRight.X - this.Width / 2 -offset;
            }
            if (game.getPlayerPosition(playerIndex).topRight.X < this.Width / 2 + offset)
            {
                offset += game.getPlayerPosition(playerIndex).topRight.X - this.Width / 2 - offset;

                if (offset < 0)
                {
                    offset = 0;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            List<Tuple<Coordinates, Image>> crd = game.getAllUnitsCoordinatesImages();
            foreach (Tuple<Coordinates, Image> c in crd)
            {
                Picture p = new Picture();
                p.size = mapSize(c.Item1);
                p.img = c.Item2;
                p.location = mapPosition(c.Item1, offset);

                /* Lock image from access from other player's thread. It cause exception.*/
                lock (p.img)
                {
                    e.Graphics.DrawImage(p.img, p.location.X, p.location.Y, p.size.Width, p.size.Height);
                }
            }
        }
        
        /* map position from top left to bottom left*/
        private Point mapPosition(Coordinates p, int offset = 0)
        {
            Point res = new Point();
            res.X = p.bottomLeft.X - offset;
            res.Y = this.Height - p.topRight.Y;
            return res;
        }


        private Size mapSize(Coordinates c)
        {
            Size res = new Size();
            res.Width = c.topRight.X - c.bottomLeft.X;
            res.Height= c.topRight.Y - c.bottomLeft.Y;
            return res;
        }



 
        private void button1_Click(object sender, EventArgs e)
        {
          startGame.Play();
            new Thread(Start_Game).Start();
            
            button1.Dispose();
            button2.Dispose();
        }


        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!run)
            {
                new Thread(Start_Game).Start();
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop_Game();
        }

        private void exit(object sender, EventArgs e)
        {
            Stop_Game();
            this.Dispose();
            this.Close();
        }


        private void Form1Close(object sender, FormClosingEventArgs e)
        {
            exit(sender, e);
        }
    }
}
