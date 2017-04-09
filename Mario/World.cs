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
        public enum UnitGtroupNames {stat = 0, players = 1, mobs = 2};

        private List<List<Unit>> UnitSgr = new List<List<Unit>>();
        World()
            {
            UnitSgr.Add(new List<Unit>());
            UnitSgr.Add(new List<Unit>());//обращаться по инлексам 0 и 1 
        }

        //public void  matchCollisions(Unit l1, Unit l2)
        //{
        //    for(int i = 0; i <= )
        //}
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

        public void addUnit(Unit unit, UnitGtroupNames group)
        {
            
        }

        public bool remove(Unit unit)
        {
            units.Remove(unit);
            return true;
        }

        
    }
}
