using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mario;

namespace GameEngine
{
    class World
    {
        private SortedList<Coordinates, Unit> units;
        public void addUnit(Unit unit)
        {

        }

        public Unit getUnit(Coordinates coord)
        {
            return new Unit();
        }

        public bool remove(Unit unit)
        {
            return true;
        }
    }
}
