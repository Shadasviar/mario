using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mario;

namespace GameEngine
{
    class Mob : Unit
    {
        public Mob (Coordinates position):base(position,0)
        {
            this.priority = 50;
        }

    }
}
