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

        public Point bottomLeft = new Point();

        public Point topRight = new Point();


        public Coordinates()
        {

        }

        public Coordinates(Point bottomLeft, Point topRight)
        {
            this.bottomLeft = bottomLeft;
            this.topRight = topRight;
        }

        public Coordinates(int bottonLeft_x, int bottonLeft_y,int topRight_x, int topRight_y)
        {
            this.bottomLeft.X = bottonLeft_x;
            this.bottomLeft.Y = bottonLeft_y;
            this.topRight.X = topRight_x;
            this.topRight.Y = topRight_y;

        }

    }
}
