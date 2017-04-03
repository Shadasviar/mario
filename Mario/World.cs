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

        public void nextFrame()
        {
            for(int i = 0; i < units.Count; i++)
            {
                int x;
                int y;
                Coordinates c = new Coordinates();
                x = units[i].GetPosition().bottomLeft.X + units[i].GetCurrentSpeed().horizontalSpeed;
                y = units[i].GetPosition().bottomLeft.Y + units[i].GetCurrentSpeed().verticalSpeed;                
                c.bottomLeft = new Point(x, y);
                x = units[i].GetPosition().topRight.X + units[i].GetCurrentSpeed().horizontalSpeed;
                y = units[i].GetPosition().topRight.Y + units[i].GetCurrentSpeed().verticalSpeed;
                c.topRight = new Point(x, y);

                

                units[i].SetCoordinates(c); 
            }
        }

        private List<Unit> units = new List<Unit>();

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
