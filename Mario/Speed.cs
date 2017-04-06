using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Speed
    {
        private int horizontalSpeed;
        private int verticalSpeed;

        public int getVerticalSpeed()
        {
            return verticalSpeed;
        }

        public int getHorizontalSpeed()
        {
            return horizontalSpeed;
        }

        public void setVerticalSpeed(int v)
        {
            verticalSpeed = v;
        }

        public void sethorizontalSpeed(int h)
        {
            horizontalSpeed = h;
        }

        public Speed(int h = 0, int v = 0)
        {
            verticalSpeed = v;
            horizontalSpeed = h;
        }

    }
}
