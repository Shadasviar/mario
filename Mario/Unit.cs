using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mario;

namespace GameEngine
{
    class Unit
    {
        protected Coordinates position;
        protected int priority;
        protected Speed currentSpeed;

        public Unit(Coordinates position, int priority)
        {
            this.position = position;
            this.priority = priority;
        }
        public Coordinates GetPosition()
        {
            return position;
        }
        public Speed GetCurrentSpeed()
        {
            return currentSpeed;
        }
    }

}
