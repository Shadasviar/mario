using Global;
using Mario.Properties;

namespace GameEngine
{
    class Unit
    {
        protected Coordinates position;
        protected int priority;
        protected Speed currentSpeed = new Speed(0, Settings.Default.gravitation);


        virtual public void setHorizontalSpeed(int h)
        {
            currentSpeed.setHorizontalSpeed(h);
        }


        virtual public void setVerticalSpeed(int v)
        {
            currentSpeed.setVerticalSpeed(v + Settings.Default.gravitation);
        }


        public Unit(Coordinates position, int priority = 0)
        {
            this.currentSpeed = currentSpeed + new Speed(0, Settings.Default.gravitation);
            this.position = position;
            this.priority = priority;
        }


        public Unit(Coordinates position, Speed speed, int priority = 0)
        {
            this.position = position;
            this.priority = priority;
            this.currentSpeed = speed;
            this.currentSpeed = currentSpeed + new Speed(0, Settings.Default.gravitation);
        }


        virtual public Coordinates GetPosition()
        {
            return position;
        }


        virtual public Speed GetCurrentSpeed()
        {
            return currentSpeed;
        }


        virtual public void SetCoordinates(Coordinates x)
        {
            this.position = x;
        }


        public int getPriority()
        {
            return priority;
        }
    }
}
