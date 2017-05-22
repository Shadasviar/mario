using System;
using System.Collections.Generic;
using Global;
using static System.Math;
using System.Drawing;

namespace GameEngine
{
    class World
    {
        public Unit player;
        private List<Unit> units = new List<Unit>();
        public enum UnitGroupNames {stat = 0, players = 1, mobs = 2};
        private List<List<Unit>> UnitGroups = new List<List<Unit>>();
        private bool playerAlive = true;
        private bool _levelComplete = false;
        int countCoin;


        public bool playerIsAlive()
        {
            return playerAlive;
        } 


        public World()
        {
            UnitGroups.Add(new List<Unit>());
            UnitGroups.Add(new List<Unit>()); 
            UnitGroups.Add(new List<Unit>());
            player = null;
        }


        public void initPlayer()
        {
            player = UnitGroups[(int)UnitGroupNames.players][0];
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
                            if (UnitGroups[currentGroup].Count <= i) break;
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
            if (ang < angle(a.GetPosition().bottomLeft, a.GetPosition().topRight) || 
                ang >= angle(new Point(a.GetPosition().bottomLeft.X, a.GetPosition().topRight.Y), 
                             new Point(a.GetPosition().topRight.X, a.GetPosition().bottomLeft.Y)))
            {
                int intersection = (bw / 2 + aw / 2 - width) + b.GetCurrentSpeed().getHorizontalSpeed();
                changePosition(b, new Speed(Abs(intersection), 0));
                bumpToLeft(a, b);
            }
            else
            /* v */
            if (ang <= angle(new Point(a.GetPosition().topRight.X, a.GetPosition().bottomLeft.Y),
                             new Point(a.GetPosition().bottomLeft.X, a.GetPosition().topRight.Y)))
            {
                int intersection = -(bh / 2 + ah / 2 - height) + b.GetCurrentSpeed().getVerticalSpeed();
                changePosition(b, new Speed(0, Abs(intersection)));
                bumpToDown(a, b);
            }
            else
            /* -> */
            if (ang <= angle(a.GetPosition().topRight, a.GetPosition().bottomLeft))
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
        /*                Functions wich do staff with units in different sides of bumping    
         * In these functions we assump that unit with less priority bumps to the unit
         * with higher priority, i.e. b bumps to a                                                     */

        void bumpToLeft(Unit a, Unit b)
        {
            horizontalSpecificAction(a, b);
        }


        void bumpToRight(Unit a, Unit b)
        {
            horizontalSpecificAction(a, b);
        }


        void horizontalSpecificAction(Unit a, Unit b)
        {
            /*Change direction of moving of the mob if it meet obstruction*/
            if (UnitGroups[(int)UnitGroupNames.mobs].Contains(b)) b.setHorizontalSpeed(-b.GetCurrentSpeed().getHorizontalSpeed());
            /*If player bumped to the mob, player dies*/
            if (UnitGroups[(int)UnitGroupNames.mobs].Contains(a) &&
               UnitGroups[(int)UnitGroupNames.players].Contains(b))
            {
                remove(b);
                playerAlive = false;
            }

            if (a.GetType() == typeof(Door))
            {
                if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    _levelComplete = true;
                }
            }
            if (a.GetType() == typeof(Coin))
            {
                if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                        countCoin++;
                    remove(a);
                }
            }
        }


        void bumpToUp(Unit a, Unit b)
        {
            /*If player bumped to block from bottom its jumping stops*/
            if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
            {
                (player).setVerticalSpeed(0);
            }
        }


        void bumpToDown(Unit a, Unit b)
        {
            /* Kill mob if player jump to it*/
            if (UnitGroups[(int)UnitGroupNames.players].Contains(b) &&
                UnitGroups[(int)UnitGroupNames.mobs].Contains(a)) remove(a);
            

            /*If player fell down to some unit, its jumping state finished*/
            if(UnitGroups[(int)UnitGroupNames.players].Contains(b))
            {
                ((Jumpable)player).inJump(false);
            }

            if(a.GetType() == typeof(DieUnitBlock))
            {
                if(UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    playerAlive = false;
                }
                remove(b);
            }

            if(a.GetType() == typeof(Door))
            {
                if(UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    _levelComplete = true;
                }
            }

            if (a.GetType() == typeof(Coin))
            {
                if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    countCoin++;
                    remove(a);
                }
            }

        }


        /***********************************************************************************************/
        public bool levelComplete()
        {
            return _levelComplete;
        }

        /* Set coordinates of unit according given speed */
        void changePosition(Unit b, Speed s)
        {
            b.SetCoordinates(
                new Coordinates(
                    b.GetPosition().bottomLeft.X + s.getHorizontalSpeed(),
                    b.GetPosition().bottomLeft.Y + s.getVerticalSpeed(),
                    b.GetPosition().topRight.X + s.getHorizontalSpeed(),
                    b.GetPosition().topRight.Y + s.getVerticalSpeed()
                )
            );
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
            ((Jumpable)player).limitJump();               
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


        public void addUnit(Unit unit, UnitGroupNames group)
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
