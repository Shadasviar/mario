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

        public Unit(Coordinates position, int priority, Speed speed)
        {
            this.position = position;
            this.priority = priority;
            this.currentSpeed = speed;
        }
        public Coordinates GetPosition()
        {
            return position;
        }
        public Speed GetCurrentSpeed()
        {
            return currentSpeed;
        }

        public void SetCoordinates(Coordinates x)
        {
            this.position = x;
        }

    }

}
