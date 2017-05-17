using Global;
using Mario.Properties;

namespace GameEngine
{
    class GroundUnit : Unit
    {
        public GroundUnit(Coordinates position):base(position)
        {
            currentSpeed = new Speed(0, 0);
            this.priority = Settings.Default.groundPriority;
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
