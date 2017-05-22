using Global;
using Mario.Properties;

using System.Drawing;

namespace GameEngine
{
    class Player : Unit, Jumpable
    {
        int StartPoint;
        bool isInJump;


        public Player(Coordinates position):base(position)
        {
            isInJump = true;
            this.priority = Settings.Default.playerPriority;
            this.texture = new Bitmap(Resources.szifer);
        }


        public void jump()
        {
            if (!isInJump)
            {
                setVerticalSpeed(Settings.Default.speedOfJump);
                StartPoint = GetPosition().bottomLeft.Y;
                isInJump = true;
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
                if(GetPosition().bottomLeft.Y > StartPoint + (position.topRight.Y - position.bottomLeft.Y) * Settings.Default.heightOfJump)
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
