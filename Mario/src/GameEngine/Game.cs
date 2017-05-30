using System;
using System.Collections.Generic;
using System.Drawing;
using Global;
using GameEngine;
using Mario.Properties;
using System.Media;

namespace Mario
{
    enum keysNames {
        Right, Left, Down, Space,
        D, A, S, W
    };


    enum keysType { Right, Left, Down, Jump};
   

    class Game : GameAPI
    {
        protected List<World> levels = new List<World>();
        protected List<int> keysStatus = new List<int>();
        protected int currentLevel;
        protected int numberOfPlayers;

        /* Associate key type with concrete key for each player.
         * Player's numbes is ordinary number of it in the list. */
        static List<Dictionary<keysType, keysNames>> keyAssociatedWithPlayer = 
            new List<Dictionary<keysType, keysNames>>
        {
            new Dictionary<keysType, keysNames>{
                {keysType.Down, keysNames.Down},
                {keysType.Jump, keysNames.Space},
                {keysType.Right, keysNames.Right},
                {keysType.Left, keysNames.Left},
            },
            new Dictionary<keysType, keysNames>{
                {keysType.Down, keysNames.S},
                {keysType.Jump, keysNames.W},
                {keysType.Right, keysNames.D},
                {keysType.Left, keysNames.A},
            },
        };


        public Game(ref List<int>a, int nPlayers = 1)
        {
            numberOfPlayers = nPlayers;
            keysStatus = a;
            MapParser m = new MapParser();
            levels.Add(m.parse(Settings.Default.mapsPath + "/Level1.txt"));
            levels.Add(m.parse(Settings.Default.mapsPath + "/Level2.txt"));
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
            int min = Math.Min(Enum.GetNames(typeof(keysType)).Length, levels[currentLevel].players.Count);

            for (int i = 0; i < min; ++i)
            {
                if (keysStatus[(int)keyAssociatedWithPlayer[i][keysType.Right]] == 1)
                {
                    h1 = Settings.Default.standardPlayerSpeed;
                }
                else
                {
                    h1 = 0;
                }

                if (keysStatus[(int)keyAssociatedWithPlayer[i][keysType.Left]] == 1)
                {
                    h2 = -Settings.Default.standardPlayerSpeed;
                }
                else
                {
                    h2 = 0;
                }
                changeHSpeed(levels[currentLevel].players[i], h1, h2);

                if (keysStatus[(int)keyAssociatedWithPlayer[i][keysType.Jump]] == 1)
                {
                    ((Jumpable)levels[currentLevel].players[i]).jump();
                }
            }

            levels[currentLevel].nextFrame();

            if(levels[currentLevel].levelComplete() == true)
            {
                currentLevel++;
            }
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
            throw new NotImplementedException();
        }


        public bool setLevel(int index)
        {
            if(index < levels.Count && index >= 0)
            {
                currentLevel = index;
                return true;
            }
            else return false;
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

        
    }
}
