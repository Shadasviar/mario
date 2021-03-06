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
    public partial class MainWindow : Form
    {
        delegate void updateStateDelegate();
        GameAPI game;
        private Dictionary<Keys, int> keys = new Dictionary<Keys, int>();
        int offset = 0;
        protected int playerIndex;
        MainMenu parent;


        public MainWindow(Object game, ref Dictionary<Keys, int> keys, MainMenu parent, int player = 0)
        {
            this.playerIndex = player;
            this.game = (GameAPI)game;
            this.keys = keys;
            this.parent = parent;
            InitializeComponent();

            /* For disable flicking*/
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic,
               null, this, new object[] { true });
        }


        void Stop_Game()
        {
            parent.pauseGame();
        }

        private void MaunWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (keys.ContainsKey(e.KeyCode))
            {
                keys[e.KeyCode] = 1;
            }
        }


        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (keys.ContainsKey(e.KeyCode))
            {
                keys[e.KeyCode] = 0;
            }
        }


        private void MainWindow_Load(object sender, EventArgs e)
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
            lock (game)
            {
                label1.Text = "Coins: " + game.numberCoin(playerIndex).ToString();
            }
            label1.Refresh();
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


        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            parent.resumeGame();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop_Game();
        }

        private void exit(object sender, EventArgs e)
        {
            parent.stopGame();
        }


        private void MainWindow_Close(object sender, FormClosingEventArgs e)
        {
            exit(sender, e);
        }
    }
}
