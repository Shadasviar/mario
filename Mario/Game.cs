using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

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
            levels.Add(init_test_world());
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
            if (keysStatus[(int) keysNames.Down] == 1)
            {
            }
            else
            {
                levels[currentLevel].getUnit(0).setVerticalSpeed(levels[currentLevel].getUnit(0).GetCurrentSpeed().getVerticalSpeed() - 1);
            }

            int h1 = 0;
            int h2 = 0;

            if (keysStatus[(int)keysNames.Right] == 1)
            {
                h1 = 1;
            }
            else
            {
                h1 = 0;
            }

            if (keysStatus[(int)keysNames.Left] == 1)
            {
                h2 = -1;
            }
            else
            {
                h2 = 0;
            }
            changeHSpeed(levels[currentLevel].getUnit(0), h1, h2);

            if (keysStatus[(int)keysNames.Space] == 1)
            {
                h1 = 2;
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
            result.addUnit(new Unit(c, 1, new Speed(1, 0)), World.UnitGtroupNames.mobs);
            result.addUnit(new Unit(new Coordinates(100, 100,110, 110),1, new Speed(0,0)));

            Coordinates c1 = new Coordinates();
            c1.bottomLeft = new System.Drawing.Point(50, 50);
            c1.topRight = new System.Drawing.Point(10, 10);
           
            
            Coordinates c2 = new Coordinates();
            c2.bottomLeft = new System.Drawing.Point(350, 111);
            c2.topRight = new System.Drawing.Point(500, 361);
            result.addUnit(new Unit(c2, 1, new Speed(0,0)));

            Coordinates c3 = new Coordinates();
            c3.bottomLeft = new System.Drawing.Point(0, 0);
            c3.topRight = new System.Drawing.Point(500, 30);
            result.addUnit(new GroundUnit(c3, 1));


            return result;
        }


    }
}
