using System;
using System.Windows.Forms;
using Mario.Properties;
using System.Collections.Generic;
using System.Threading;

namespace Global

{
    public partial class MainMenu : Form
    {
        Form1[] windows;
        List<int> keys = new List<int>(new int[8]);
        GameAPI game;
        bool run = false;


        public MainMenu()
        {
            InitializeComponent();
        }


        private void new_game_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            startGame();
        }


        private void startGame()
        {
            game = new Game(ref keys);

            windows = new Form1[Settings.Default.players_number];
            for(int i = 0; i < Settings.Default.players_number; ++i)
            {
                windows[i] = new Form1(game, ref keys, this, i);
                windows[i].Show();
            }

            new Thread(sync).Start();
        }


        public void pauseGame()
        {
            run = false;
        }


        public void resumeGame()
        {
            if (!run)
            {
                new Thread(sync).Start();
            }
        }


        void sync()
        {
            run = true;
            while (run)
            {
                game.nextFrame();
                try
                {
                    for (int i = 0; i < Settings.Default.players_number; ++i)
                    {
                        windows[i].updateState();
                    }
                }
                catch (Exception) { };
                Thread.Sleep(1000 / Settings.Default.fps);
            }
        }


        private void settings_button_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(this);
            this.Hide();
            settings.Show();
        }
    }
}
