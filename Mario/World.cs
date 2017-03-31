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
        private List<Unit> units;

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
