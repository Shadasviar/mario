using System;
using System.Collections.Generic;
using System.Drawing;
using Global;
using GameEngine;
using Mario.Properties;

namespace Mario
{
    enum keysNames {Right = 0, Left = 1, Down = 2, Space = 3 };
   

    class Game : GameAPI
    {
        protected List<World> levels = new List<World>();
        protected List<int> keysStatus = new List<int>();
        protected int currentLevel;


        public Game(ref List<int>a)
        {
            keysStatus = a;
            MapParser m = new MapParser();
            levels.Add(m.parse("../../Levels/Level1.txt"));
            levels.Add(m.parse("../../Levels/Level2.txt"));
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

            if (keysStatus[(int)keysNames.Right] == 1)
            {
                h1 = Settings.Default.standardPlayerSpeed;
            }
            else
            {
                h1 = 0;
            }

            if (keysStatus[(int)keysNames.Left] == 1)
            {
                h2 = -Settings.Default.standardPlayerSpeed;
            }
            else
            {
                h2 = 0;
            }
            changeHSpeed((Unit)levels[currentLevel].player, h1, h2);

            if (keysStatus[(int)keysNames.Space] == 1)
            {
                ((Jumpable)levels[currentLevel].player).jump();
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


        public Coordinates getPlayerPosition()
        {
            return levels[currentLevel].player.GetPosition();
        }
    }
}
