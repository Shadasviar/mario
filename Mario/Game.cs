using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace Mario
{
    class Game : GameAPI
    {
        protected List<World> levels;
        protected int currentLevel;
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
            throw new NotImplementedException();
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
    }
}
