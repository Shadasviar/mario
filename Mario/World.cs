using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mario;
using System.Drawing;

namespace GameEngine
{
    class World
    {

        private List<Unit> units = new List<Unit>();

        private List<List<Unit>> UnitGroups = new List<List<Unit>>();
        World()
            {
            UnitGroups.Add(new List<Unit>());
            UnitGroups.Add(new List<Unit>());//обращаться по инлексам 0 и 1 
        }

        public void  matchCollisions()
        {
            for(int i = 0; i <= UnitGroups[0].Count;i++)
            {
                for (int j = 0; j <= UnitGroups[1].Count; j++)
                {
                    if(UnitGroups[0][i].GetPosition() == UnitGroups[1][j].GetPosition())
                    {
                        resolveCollision(UnitGroups[0][i], UnitGroups[1][j]);
                    }
                }
            }
        }

        public void resolveCollision(Unit a, Unit b)
        {
           // if(a.)
        }


        public void nextFrame()
        {
            for(int i = 0; i < units.Count; i++)
            {
                int x;
                int y;
                Coordinates c = new Coordinates();
                x = units[i].GetPosition().bottomLeft.X + units[i].GetCurrentSpeed().getHorizontalSpeed();
                y = units[i].GetPosition().bottomLeft.Y + units[i].GetCurrentSpeed().getVerticalSpeed();                
                c.bottomLeft = new Point(x, y);
                x = units[i].GetPosition().topRight.X + units[i].GetCurrentSpeed().getHorizontalSpeed();
                y = units[i].GetPosition().topRight.Y + units[i].GetCurrentSpeed().getVerticalSpeed();
                c.topRight = new Point(x, y);

                units[i].SetCoordinates(c); 
            }
        }

        public IList<Unit> getAllUnits()
        {
            return units.AsReadOnly();
        }
        public void addUnit(Unit unit)
        {
            units.Add(unit);
        }

        public bool remove(Unit unit)
        {
            units.Remove(unit);
            return true;
        }

        
    }
}
