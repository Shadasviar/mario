using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Mario
{
    enum keysNames {Right = 0, Left = 1, Down = 2, Space = 3 };
   

    class Game : GameAPI
    {
        enum Textures { szifer = 0, cegla = 1 };

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
                h1 = 10;
            }
            else
            {
                h1 = 0;
            }

            if (keysStatus[(int)keysNames.Left] == 1)
            {
                h2 = -10;
            }
            else
            {
                h2 = 0;
            }
            changeHSpeed(levels[currentLevel].getUnit(0), h1, h2);
            h1 = 0;
            h2 = 0;
            if (keysStatus[(int)keysNames.Space] == 1)
            {
                h1 = 5;
            }
            else
            {
                h2 = 0;
            }

            if (keysStatus[(int)keysNames.Down] == 1)
            {
                h1 = 1;
            }
            else
            {
                h2 = 0;
            }
            changeVSpeed(levels[currentLevel].getUnit(0), h1, h2);

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
            Coordinates c = new Coordinates();
            c.bottomLeft = new System.Drawing.Point(200, 200);
            c.topRight = new System.Drawing.Point(210, 210);
            result.addUnit(new Unit(c, 1, new Speed(1, 0)), World.UnitGtroupNames.players);
            result.addUnit(new Unit(new Coordinates(100, 100,110, 110),1, new Speed(0,0)),  World.UnitGtroupNames.players);

            for (int i = 0; i < 10; ++i)
            {
                result.addUnit(new GroundUnit(new Coordinates(i*100,0, i*100+100, 100), 1), World.UnitGtroupNames.stat);
            }

            Coordinates c2 = new Coordinates();
            c2.bottomLeft = new Point(350, 111);
            c2.topRight = new Point(450, 211);
            result.addUnit(new GroundUnit(c2, 1),World.UnitGtroupNames.stat);
            
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
                else
                {
                    result.Add(new Tuple<Coordinates, Image>(units[i].GetPosition(), images[(int)Textures.szifer]));
                }
            }
            return result;
        }
    }
}
