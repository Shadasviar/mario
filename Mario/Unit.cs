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
        public const int gravition = -1;
        protected Coordinates position;
        protected int priority;
        protected Speed currentSpeed = new Speed(0, gravition);

        public void setHorizontalSpeed(int h)
        {
            currentSpeed.setHorizontalSpeed(h);
        }

        public void setVerticalSpeed(int v)
        {
            currentSpeed.setVerticalSpeed(v + gravition);
        }

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
            this.currentSpeed = currentSpeed + new Speed(0, gravition);
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
