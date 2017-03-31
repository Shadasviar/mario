using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Unit
    {
        protected Coordinates position;
        protected int priority;
        protected Speed currentSpeed;

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
