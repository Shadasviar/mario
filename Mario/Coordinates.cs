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
            CheckArguments();
        }


        public Coordinates(int bottonLeft_x, int bottonLeft_y,int topRight_x, int topRight_y)
        {
            this.bottomLeft.X = bottonLeft_x;
            this.bottomLeft.Y = bottonLeft_y;
            this.topRight.X = topRight_x;
            this.topRight.Y = topRight_y;

            CheckArguments();
        }
       

        void CheckArguments()
        {
            if(topRight.X <= bottomLeft.X || topRight.Y <= bottomLeft.Y)
            throw new ArgumentException();
        }


        public static bool operator > (Coordinates A, Coordinates B)
        {
            return ((A.bottomLeft.X > B.topRight.X) || (A.bottomLeft.Y > B.topRight.Y));
        }


        public static bool operator < (Coordinates A, Coordinates B)
        {
            return ((A.topRight.X < B.bottomLeft.X) || (A.topRight.Y < B.bottomLeft.Y));
        }

        public static bool operator == (Coordinates A, Coordinates B)
        {
            return !(A > B || A < B);
        }


        public static bool operator !=(Coordinates A, Coordinates B)
        {
            return (A > B || A < B);
        }
    }
}
