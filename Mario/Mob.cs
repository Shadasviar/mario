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
        public Mob (Coordinates position, Speed s):base(position,0,s)
        {
            
            this.priority = 50;
        }

    }
}
