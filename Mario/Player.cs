using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Mario;


namespace GameEngine
{
    class Player : Unit, Jumpble
    {
        int StartPoint;
        const int height = 5;
        bool isInJump;

        public Player(Coordinates position):base(position,1)
        {
            isInJump = true;
        }


        public void jump()
        {
            if (!isInJump)
            {
                setVerticalSpeed(height);
                StartPoint = GetPosition().bottomLeft.Y;
                isInJump = true;
                position.bottomLeft.Y += 1;
                position.topRight.Y += 1;

            }


        }

        public void inJump(bool b)
        {
            isInJump = b;
        }

        public void limitJump()
        {
            if(isInJump)
            {
                if(GetPosition().bottomLeft.Y > StartPoint + (position.topRight.Y - position.bottomLeft.Y) * height)
                {
                    setVerticalSpeed(0);
                }
            }
            else
            {
                setVerticalSpeed(0);
            }
        }
    }
}
