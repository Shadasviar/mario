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
            UnitGroups.Add(new List<Unit>()); 
            UnitGroups.Add(new List<Unit>()); 
        }

        public void  matchCollisions()
        {
            for (int currentGroup = 0; currentGroup < UnitGroups.Count; currentGroup++)
            {
                for (int iGroup = currentGroup + 1; iGroup < UnitGroups.Count; iGroup++)
                {
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

        /* Push b from a in direction opposit for moving.
         * Do actions with units in collision dependent on side of it.*/
        void resolveCollisionDependOnSide(Unit a, Unit b)
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
                changePosition(b, new Speed(Abs(intersection), 0));
                bumpToLeft(a, b);
            }
            else
            /* v */
            if (ang <= -45)
            {
                int intersection = -(bh / 2 + ah / 2 - height) + b.GetCurrentSpeed().getVerticalSpeed();
                changePosition(b, new Speed(0, Abs(intersection)));
                bumpToDown(a, b);
            }
            else
            /* -> */
            if (ang <= 45)
            {
                int intersection = -(bw / 2 + aw / 2 - width) + b.GetCurrentSpeed().getHorizontalSpeed();
                changePosition(b, new Speed(-Abs(intersection), 0));
                bumpToRight(a, b);
            }
            /* ^ */
            else
            {
                int intersection = (bh / 2 + ah / 2 - height) + b.GetCurrentSpeed().getVerticalSpeed();
                changePosition(b, new Speed(0, -Abs(intersection)));
                bumpToUp(a, b);
            }
        }

        /***********************************************************************************************/
        /*                Functions wich do staff with units in different sides of bumping             */

        void bumpToLeft(Unit a, Unit b)
        {
            /*Change direction of moving of the mob if it meet obstruction*/
            if (UnitGroups[(int)UnitGtroupNames.mobs].Contains(b)) b.setHorizontalSpeed(-b.GetCurrentSpeed().getHorizontalSpeed());
        }

        void bumpToRight(Unit a, Unit b)
        {
            /*Change direction of moving of the mob if it meet obstruction*/
            if (UnitGroups[(int)UnitGtroupNames.mobs].Contains(b)) b.setHorizontalSpeed(-b.GetCurrentSpeed().getHorizontalSpeed());
        }

        void bumpToUp(Unit a, Unit b)
        {

        }

        void bumpToDown(Unit a, Unit b)
        {
            /* Kill mob if player jump to it*/
            if (UnitGroups[(int)UnitGtroupNames.players].Contains(b) &&
                UnitGroups[(int)UnitGtroupNames.mobs].Contains(a)) remove(a);
        }

        /***********************************************************************************************/

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
            UnitGroups[(int)group].Add(unit);
            addUnit(unit);
        }

        public bool remove(Unit unit)
        {
            units.Remove(unit);
            foreach(List<Unit> group in UnitGroups)
            {
                if (group.Contains(unit)) group.Remove(unit);
            }
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
