using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Speed
    {
        public int horizontalSpeed;
        public int verticalSpeed;
        public float factorSpeed;

        public static Speed operator*(Speed x, int f)
            {
            return new Speed (x * f);
            }

    }
}
