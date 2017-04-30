using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mario;

namespace GameEngine
{
    class GroundUnit : Unit
    {
        public GroundUnit(Coordinates position):base(position, 0)
        {
            currentSpeed = new Speed(0, 0);
            this.priority = 100;
        }


        override public void setHorizontalSpeed(int h)
        {
            currentSpeed.setHorizontalSpeed(0);
        }


        override public void setVerticalSpeed(int v)
        {
            currentSpeed.setVerticalSpeed(0);
        }
    }
}
