using System;
using System.Collections.Generic;
using Global;
using static System.Math;
using Mario.Properties;
using System.Drawing;

namespace GameEngine
{
    class World
    {
        public Unit[] players;
        private List<Unit> units = new List<Unit>();
        public enum UnitGroupNames {stat, players, mobs, staff};
        private List<List<Unit>> UnitGroups = new List<List<Unit>>();
        private int playersAlive = Settings.Default.players_number;
        private int _levelComplete = 0;
        int [] countCoins;
        System.Media.SoundPlayer deathWB = new System.Media.SoundPlayer("../../Resources/deathWB.wav");
        System.Media.SoundPlayer killedByMob = new System.Media.SoundPlayer("../../Resources/killedByMob.wav");
  
        public bool playerIsAlive()
        {
            return playersAlive > 0;
        } 


        public World()
        {
            for(int i = 0; i < Enum.GetNames(typeof(UnitGroupNames)).Length; ++i)
            {
                UnitGroups.Add(new List<Unit>());
            }
            players = null;
            
        }


        public void initPlayer()
        {
            for(int i = UnitGroups[(int)UnitGroupNames.players].Count; i < Settings.Default.players_number; ++i)
            {
                this.addUnit(new Player(UnitGroups[(int)UnitGroupNames.players][0].GetPosition()), UnitGroupNames.players);
            }

            players = new Unit[UnitGroups[(int)UnitGroupNames.players].Count];
            for(int i = 0; i < UnitGroups[(int)UnitGroupNames.players].Count; ++i)
            {
                players[i] = UnitGroups[(int)UnitGroupNames.players][i];
            }
            countCoins = new int[players.Length];
        }


        public void  matchCollisions()
        {
            /* Check collisions between three basic groups: stat, players and mobs*/
            for (int currentGroup = 0; currentGroup < (int)UnitGroupNames.staff; currentGroup++)
            {
                for (int iGroup = currentGroup + 1; iGroup < (int)UnitGroupNames.staff; iGroup++)
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

            /* Check collisions between players and staff. Staff shoul only interact with players.*/
            for (int i = 0; i < UnitGroups[(int)UnitGroupNames.staff].Count; i++)
            {
                for (int j = 0; j < UnitGroups[(int)UnitGroupNames.players].Count; j++)
                {
                    if (UnitGroups[(int)UnitGroupNames.staff].Count <= i) break;
                    if (UnitGroups[(int)UnitGroupNames.staff][i].GetPosition() == 
                        UnitGroups[(int)UnitGroupNames.players][j].GetPosition())
                    {
                        resolveCollision(UnitGroups[(int)UnitGroupNames.staff][i],
                            UnitGroups[(int)UnitGroupNames.players][j]);
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

            sideIndependentAction(a, b);

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


        void sideIndependentAction(Unit a, Unit b)
        {
            if (a.GetType() == typeof(DieUnitBlock))
            {
                if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    --playersAlive;
                }
                remove(b);
            }

            if (a.GetType() == typeof(Door))
            {
                if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    ++_levelComplete;
                    remove(b);
                }
            }

            if (a.GetType() == typeof(Coin))
            {
                if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
                {
                    countCoins[Array.IndexOf(players, b)]++;
                    remove(a);
                }
            }
        }


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
                killedByMob.Play();
                --playersAlive;
            }
        }


        void bumpToUp(Unit a, Unit b)
        {
            /*If player bumped to block from bottom its jumping stops*/
            if (UnitGroups[(int)UnitGroupNames.players].Contains(b))
            {
                b.setVerticalSpeed(0);
            }

            /* Bird kill player if it bumped from bottom*/
            if(UnitGroups[(int)UnitGroupNames.players].Contains(b) &&
                UnitGroups[(int)UnitGroupNames.mobs].Contains(a))
            {
                remove(b);
                killedByMob.Play();
                --playersAlive;
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
                ((Jumpable)b).inJump(false);
            }
        }


        /***********************************************************************************************/
        public bool levelComplete()
        {
            return (_levelComplete > 0) && (_levelComplete == playersAlive);
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
            foreach(Jumpable player in players) player.limitJump();
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
             //   Label1.Text = countCoin;
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

        public int[]getCoins()
        {
            return countCoins;
        }

    }
}
