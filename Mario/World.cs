using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    class World
    {
        private SortedList<Coordinates, Unit> units;
        public enum Side { Top, Left, up, Down}; //Temp enum

        public void addUnit(Unit unit)
        {

        }

        public Unit getUnit(Coordinates coord)
        {
            return new Unit();
        }

        public bool move(Unit unit, Coordinates dst)
        {
            return true;
        }

        public bool move(Coordinates src, Coordinates dst)
        {
            return true;
        }

        public bool remove(Unit unit)
        {
            return true;
        }

        public bool interaction(Unit unit1, Side side1, Unit unit2, Side side2)
        {
            return true;
        }
    }
}
