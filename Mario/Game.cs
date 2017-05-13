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
        enum Textures { szifer = 0, cegla = 1, empty = 2 };

        protected List<World> levels = new List<World>();
        protected List<int> keysStatus = new List<int>();
        protected int currentLevel;
        protected List<Image> images = new List<Image>();


        public Game(ref List<int>a)
        {
            keysStatus = a;
            levels.Add(init_test_world());
            images.Add(new Bitmap(Mario.Properties.Resources.szifer));
            images.Add(new Bitmap (Mario.Properties.Resources.cegla));
            images.Add(new Bitmap(Mario.Properties.Resources.empty));
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


        private World init_test_world()
        {
            World result = new World();
            MapParser m = new MapParser();
            result = m.parse("../../Level1.txt");
            return result;
        }


        public List<Tuple<Coordinates, Image>> getAllUnitsCoordinatesImages()
        {
            List<Tuple<Coordinates, Image>> result = new List<Tuple<Coordinates, Image>>();
            IList<Unit> units = levels[currentLevel].getAllUnits();
            for(int i = 0; i < units.Count; i++)
            {
                if(units[i].GetType() == typeof(GroundUnit))
                {
                    result.Add(new Tuple<Coordinates, Image>(units[i].GetPosition(), images[(int)Textures.cegla]));
                }
                else if(units[i].GetType() == typeof(Mushroom))
                {
                    result.Add(new Tuple<Coordinates, Image>(units[i].GetPosition(), images[(int)Textures.szifer]));
                }
                else if (units[i].GetType() == typeof(Player))
                {
                    result.Add(new Tuple<Coordinates, Image>(units[i].GetPosition(), images[(int)Textures.szifer]));
                }
                else
                {
                    result.Add(new Tuple<Coordinates, Image>(units[i].GetPosition(), images[(int)Textures.empty]));
                }
                
            }
            return result;
        }


        public Coordinates getPlayerPosition()
        {
            return levels[currentLevel].player.GetPosition();
        }
    }
}
