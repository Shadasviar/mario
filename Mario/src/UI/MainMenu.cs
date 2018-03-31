using System;
using System.Windows.Forms;
using Mario.Properties;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Global

{
    public partial class MainMenu : Form
    {
        MainWindow[] windows;
        private Dictionary<Keys, int> keys = new Dictionary<Keys, int> {
            { Keys.Down,    0 },
            { Keys.Space,   0 },
            { Keys.Left,    0 },
            { Keys.Right,   0 },
            { Keys.W,       0 },
            { Keys.A,       0 },
            { Keys.S,       0 },
            { Keys.D,       0 },
        };
        GameAPI game;
        bool run = false;


        public MainMenu()
        {
            InitializeComponent();
            gameoverLabel.Visible = false;
        }


        private void new_game_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            startGame();
        }


        private void startGame()
        {
            keys = keys.ToDictionary(x => x.Key, x => 0);
            game = new Game(ref keys);

            windows = new MainWindow[Settings.Default.players_number];
            for(int i = 0; i < Settings.Default.players_number; ++i)
            {
                windows[i] = new MainWindow(game, ref keys, this, i);
                windows[i].Show();
            }

            new Thread(sync).Start();
        }


        public void pauseGame()
        {
            run = false;
        }


        public void stopGame()
        {
            pauseGame();
            if (game.playerWon()) gameoverLabel.Text = "YOU WIN!";
            gameoverLabel.Visible = true;
            for (int i = 0; i < Settings.Default.players_number; ++i)
            {
                windows[i].Dispose();
                windows[i].Close();
            }
            this.Show();
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

                if (!game.playerIsAlive() || game.playerWon())
                {
                    if (InvokeRequired)
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                stopGame();
                            }));
                            return;
                        }
                        catch (Exception) { };
                    }
                }
                Thread.Sleep(1000 / Settings.Default.fps);
            }
        }


        private void settings_button_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(this);
            this.Hide();
            settings.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
