using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mario
{
    class Coordinates
    {
        int a, b, c, d;
        int cord_x, cord_y;

        public Point bottomLeft = new Point();

        public Point topRight = new Point();

        public Coordinates(int cord_x, int cord_y)
            {
            this.cord_x = cord_x;
            this.cord_y = cord_y;

        }

        public Coordinates(int a, int b, int c, int d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;

        }

    }
}
