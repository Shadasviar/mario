using Global;
using System.Drawing;
using Mario.Properties;
using static System.Math;

namespace GameEngine
{
    class GroundPlatform : GroundUnit
    {
        Coordinates startPos;
        public GroundPlatform(Coordinates position):base(position)
        {
            currentSpeed = new Speed(0, 0);
            this.priority = Settings.Default.groundPriority;
            texture = new Bitmap(Resources.cegla);
            startPos = position;
            this.currentSpeed.setHorizontalSpeed(Settings.Default.platformSpeed);
        }


        override public Coordinates GetPosition()
        {
            if (Abs(startPos.bottomLeft.X - position.bottomLeft.X) >= 
                Settings.Default.platformDistance*Settings.Default.standardSizeOfUnit)
            {
                currentSpeed.setHorizontalSpeed(-currentSpeed.getHorizontalSpeed());
                startPos = position;
            }
            return position;
        }
    }
}
