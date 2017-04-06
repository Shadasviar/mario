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

        public Game()
        {
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
            levels[currentLevel].nextFrame();           
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
            c.bottomLeft = new System.Drawing.Point(100, 100);
            c.topRight = new System.Drawing.Point(200, 200);
            result.addUnit(new Unit(c, 1, new Speed(1, 0)));
            
            Coordinates c1 = new Coordinates();
            c1.bottomLeft = new System.Drawing.Point(0, 0);
            c1.topRight = new System.Drawing.Point(400, 100);
            result.addUnit(new Unit(c1, 1, new Speed(3, 0)));
            
            Coordinates c2 = new Coordinates();
            c2.bottomLeft = new System.Drawing.Point(350, 111);
            c2.topRight = new System.Drawing.Point(500, 361);
            result.addUnit(new Unit(c2, 1, new Speed(0,0)));

            Coordinates c3 = new Coordinates();
            c3.bottomLeft = new System.Drawing.Point(0, 0);
            c3.topRight = new System.Drawing.Point(220, 330);
            result.addUnit(new GroundUnit(c3, 1));

            return result;
        }


        public void initGame(List<int> a)
        {
            keysStatus = a; 
        }

    }
}
