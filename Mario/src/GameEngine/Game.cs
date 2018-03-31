using System;
using System.Collections.Generic;
using System.Drawing;
using GameEngine;
using Mario.Properties;
using System.Media;
using System.IO;
using System.Windows.Forms;

namespace Global
{
    enum keysType { Right, Left, Down, Jump};
   
    class Game : GameAPI
    {
        protected List<World> levels = new List<World>();
        protected Dictionary<Keys, int> keysStatus = new Dictionary<Keys, int>();
        protected int currentLevel;
        protected bool won = false;
        protected int[] coins; 

        /* Associate key type with concrete key for each player.
         * Player's numbes is ordinary number of it in the list. */
        public static List<Dictionary<keysType, Keys>> keyAssociatedWithPlayer = 
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


        public Game(ref Dictionary<Keys, int> a)
        {
            keysStatus = a;
            MapParser m = new MapParser();

            /* Test if levels exists in the root of exe */
            if (Directory.Exists("Levels"))
            {
                Settings.Default.mapsPath = "Levels";
            }

            levels.Add(m.parse(Settings.Default.mapsPath + "/Level1.txt"));
            levels.Add(m.parse(Settings.Default.mapsPath + "/Level2.txt"));
            coins = new int[Settings.Default.players_number];
        }


        public List<Coordinates> getAllUnitsCoordinates()
        {
            List<Coordinates> result = new List<Coordinates>();
            foreach(Unit u in levels[currentLevel].getAllUnits())
            {
                result.Add(u.GetPosition());
            }
            return result;
        }


        public void nextFrame()
        {
            int h1 = 0;
            int h2 = 0;
            int min = Math.Min(Enum.GetNames(typeof(keysType)).Length, levels[currentLevel].players.Length);
            Settings.Default.players_number = min;

            for (int i = 0; i < min; ++i)
            {
                if (keysStatus[keyAssociatedWithPlayer[i][keysType.Right]] == 1)
                {
                    h1 = Settings.Default.standardPlayerSpeed;
                }
                else
                {
                    h1 = 0;
                }

                if (keysStatus[keyAssociatedWithPlayer[i][keysType.Left]] == 1)
                {
                    h2 = -Settings.Default.standardPlayerSpeed;
                }
                else
                {
                    h2 = 0;
                }
                changeHSpeed(levels[currentLevel].players[i], h1, h2);

                if (keysStatus[keyAssociatedWithPlayer[i][keysType.Jump]] == 1)
                {
                    ((Jumpable)levels[currentLevel].players[i]).jump();
                    if (!Settings.Default.Sound_Off) new SoundPlayer(Resources.smb_jump_small).Play();
                }
            }

            levels[currentLevel].nextFrame();

            if(levels[currentLevel].levelComplete() == true)
            {
                if (currentLevel + 1 < levels.Count) currentLevel++;
                else won = true;
            }

            coins = levels[currentLevel].getCoins();
        }


        private void changeHSpeed(Unit u, int h1, int h2)
        {
            u.setHorizontalSpeed(h1 + h2);
        }
        
        
        private void changeVSpeed(Unit u, int h1, int h2)
        {
            u.setVerticalSpeed(h1 + h2);
        }


        public bool playerIsAlive()
        {
            return levels[currentLevel].playerIsAlive();
        }


        public List<Tuple<Coordinates, Image>> getAllUnitsCoordinatesImages()
        {
            List<Tuple<Coordinates, Image>> result = new List<Tuple<Coordinates, Image>>();
            IList<Unit> units = levels[currentLevel].getAllUnits();
            for(int i = 0; i < units.Count; i++)
            {
                result.Add(new Tuple<Coordinates, Image>(units[i].GetPosition(), units[i].getTexture()));
            }
            return result;
        }


        public Coordinates getPlayerPosition(int playerNumber = 0)
        {
            return levels[currentLevel].players[playerNumber].GetPosition();
        }


        public bool playerWon()
        {
            return won;
        }

        public int numberCoin(int playerNumber = 0)
        {
            return coins[playerNumber]; 
        }
    }
}
