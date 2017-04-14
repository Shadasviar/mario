using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mario;
using static System.Math;
using System.Drawing;

namespace GameEngine
{
    class World
    {

        private List<Unit> units = new List<Unit>();
        public enum UnitGtroupNames {stat = 0, players = 1, mobs = 2};

        private List<List<Unit>> UnitGroups = new List<List<Unit>>();
        public World()
            {
            UnitGroups.Add(new List<Unit>());
            UnitGroups.Add(new List<Unit>());//обращаться по инлексам 0 и 1 
            UnitGroups.Add(new List<Unit>());//обращаться по инлексам 0 и 1 
        }

        public void  matchCollisions()
        {
            for(int i = 0; i < UnitGroups[0].Count;i++)
            {
                for (int j = 0; j < UnitGroups[1].Count; j++)
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
            if (a.getPriority() > b.getPriority())
            {
                anglePush(a, b);
            }
            else
            {
                anglePush(b,a);
            }
        }

        /* Push unit b from unit a. Direction of pushing depends on
         * angle between b and a centers. Speed of pushing depends on speed
         * of b unit.*/
        void anglePush(Unit a, Unit b)
        {
            double ang = angle(center(a.GetPosition()), center(b.GetPosition()));
            int width = Abs(center(a.GetPosition()).X - center(b.GetPosition()).X);
            int height = Abs(center(a.GetPosition()).Y - center(b.GetPosition()).Y);
            int aw = a.GetPosition().topRight.X - a.GetPosition().bottomLeft.X;
            int bw = b.GetPosition().topRight.X - b.GetPosition().bottomLeft.X;
            int ah = a.GetPosition().topRight.Y - a.GetPosition().bottomLeft.Y;
            int bh = b.GetPosition().topRight.Y - b.GetPosition().bottomLeft.Y;

            /* <- */
            if (ang < -135 || ang >= 135)
            {
                int intersection = (bw / 2 + aw / 2 - width) + b.GetCurrentSpeed().getHorizontalSpeed();
                changePosition(b, new Speed(intersection < 0 ?
                    -intersection :
                    0, 0));
            }
            else
            /* v */
            if (ang <= -45)
            {
                int intersection = -(bh / 2 + ah / 2 - height) + b.GetCurrentSpeed().getVerticalSpeed();
                changePosition(b, new Speed(0, intersection < 0 ?
                    -intersection:
                    0));
            }
            else
            /* -> */
            if (ang <= 45)
            {
                int intersection = -(bw / 2 + aw / 2 - width) + b.GetCurrentSpeed().getHorizontalSpeed();
                changePosition(b, new Speed(intersection > 0 ?
                    -intersection :
                    0, 0));
            }
            /* ^ */
            else
            {
                int intersection = (bh / 2 + ah / 2 - height) + b.GetCurrentSpeed().getVerticalSpeed();
                changePosition(b, new Speed(0, intersection > 0 ?
                    -intersection :
                    0));
            }
        }

        /* Set coordinates of unit according given speed */
        void changePosition(Unit b, Speed s)
        {
            Coordinates tmp = new Coordinates(
                    b.GetPosition().bottomLeft.X + s.getHorizontalSpeed(),
                    b.GetPosition().bottomLeft.Y + s.getVerticalSpeed(),
                    b.GetPosition().topRight.X + s.getHorizontalSpeed(),
                    b.GetPosition().topRight.Y + s.getVerticalSpeed());

            b.SetCoordinates(tmp);
        }

        Point center(Coordinates c)
        {
            return new Point((c.topRight.X + c.bottomLeft.X) / 2, (c.topRight.Y + c.bottomLeft.Y) / 2);
        }

        double angle(Point a, Point b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            return Math.Atan2(dy,dx) * (180 / Math.PI);
        }

        public void nextFrame()
        {
            matchCollisions();
            for (int i = 0; i < units.Count; i++)
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
            UnitGroups[(int)group].Add(unit);
            addUnit(unit);
        }

        public bool remove(Unit unit)
        {
            units.Remove(unit);
            return true;
        }
        public Unit getUnit( int index)
        {
            if (index >= 0 && index < units.Count)
            {
                return units[index];
            }
            else return null;

        }

        
    }
}
