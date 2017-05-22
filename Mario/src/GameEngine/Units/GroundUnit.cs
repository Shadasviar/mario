using Global;
using Mario.Properties;
using System.Drawing;

namespace GameEngine
{
    class GroundUnit : Unit
    {
        public GroundUnit(Coordinates position):base(position)
        {
            currentSpeed = new Speed(0, 0);
            this.priority = Settings.Default.groundPriority;
            texture = new Bitmap(Resources.cegla);
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
