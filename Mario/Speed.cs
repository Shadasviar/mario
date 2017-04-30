namespace GameEngine
{
    class Speed
    {
        private int horizontalSpeed;
        private int verticalSpeed;

        public int getVerticalSpeed()
        {
            return verticalSpeed;
        }


        public int getHorizontalSpeed()
        {
            return horizontalSpeed;
        }


        public void setVerticalSpeed(int v)
        {
            verticalSpeed = v;
        }


        public void setHorizontalSpeed(int h)
        {
            horizontalSpeed = h;
        }


        public Speed(int h = 0, int v = 0)
        {
            verticalSpeed = v;
            horizontalSpeed = h;
        }


        public static Speed operator+ (Speed a, Speed b)
        {
            return new Speed(a.horizontalSpeed + b.horizontalSpeed, a.verticalSpeed+b.verticalSpeed);
        }
    }
}
