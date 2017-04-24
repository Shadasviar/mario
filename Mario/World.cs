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
        private enum Orientation { vertical, horizontal};
        private enum PushTo { top = -1, down = 1, right = 1, left = -1};
        private List<List<Unit>> UnitGroups = new List<List<Unit>>();
        public World()
            {
            UnitGroups.Add(new List<Unit>());
            UnitGroups.Add(new List<Unit>());//обращаться по инлексам 0 и 1 
            UnitGroups.Add(new List<Unit>());//обращаться по инлексам 0 и 1 
        }

        public void  matchCollisions()
        {
            for (int currentGroup = 0; currentGroup < UnitGroups.Count; currentGroup++)
            {
                for (int iGroup = currentGroup; iGroup < UnitGroups.Count; iGroup++)
                {
                    if (currentGroup == iGroup) continue;
                    for (int i = 0; i < UnitGroups[currentGroup].Count; i++)
                    {
                        for (int j = 0; j < UnitGroups[iGroup].Count; j++)
                        {
                            if (UnitGroups[currentGroup][i].GetPosition() == UnitGroups[iGroup][j].GetPosition())
                            {
                                resolveCollision(UnitGroups[currentGroup][i], UnitGroups[iGroup][j]);
                            }
                        }
                    }
                }
            }
        }

        public void resolveCollision(Unit a, Unit b)
        {
            if (a.getPriority() > b.getPriority())
            {
                resolveCollisionDependOnSide(a, b);
            }
            else
            {
                resolveCollisionDependOnSide(b,a);
            }
        }

        /* Do actions with units in collision dependent on side of it.*/
        void resolveCollisionDependOnSide(Unit a, Unit b)
        {
            double ang = angle(center(a.GetPosition()), center(b.GetPosition()));

            /* <- */
            if (ang < -135 || ang >= 135)
            {
                push(a, b, PushTo.right, Orientation.horizontal);
                if (UnitGroups[(int)UnitGtroupNames.mobs].Contains(b)) b.setHorizontalSpeed(-b.GetCurrentSpeed().getHorizontalSpeed());
            }
            else
            /* v */
            if (ang <= -45)
            {
                push(a, b, PushTo.top, Orientation.vertical);
            }
            else
            /* -> */
            if (ang <= 45)
            {
                push(a, b, PushTo.left, Orientation.horizontal);
                if (UnitGroups[(int)UnitGtroupNames.mobs].Contains(b)) b.setHorizontalSpeed(-b.GetCurrentSpeed().getHorizontalSpeed());
            }
            /* ^ */
            else
            {
                push(a, b, PushTo.down, Orientation.vertical);
            }
        }

        /* Push unit b from unit a. Direction of pushing depends on
        * PushTo side parameter. Speed of pushing depends on speed
        * of b unit.*/
        void push(Unit a, Unit b, PushTo side, Orientation orientation)
        {
            int al, bl, lenght, speed;
            if(orientation == Orientation.horizontal)
            {
                al = a.GetPosition().topRight.X - a.GetPosition().bottomLeft.X;
                bl = b.GetPosition().topRight.X - b.GetPosition().bottomLeft.X;
                lenght = Abs(center(a.GetPosition()).X - center(b.GetPosition()).X);
                speed = b.GetCurrentSpeed().getHorizontalSpeed();
            }
            else
            {
                al = a.GetPosition().topRight.Y - a.GetPosition().bottomLeft.Y;
                bl = b.GetPosition().topRight.Y - b.GetPosition().bottomLeft.Y;
                lenght = Abs(center(a.GetPosition()).Y - center(b.GetPosition()).Y);
                speed = b.GetCurrentSpeed().getVerticalSpeed();
            }

            int intersection = (int)side * (bl / 2 + al / 2 - lenght) + speed;
            if(orientation == Orientation.horizontal)
            {
                changePosition(b, new Speed((int)side * Abs(intersection), 0));
            }
            else
            {
                changePosition(b, new Speed(0, -(int)side * Abs(intersection)));
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
