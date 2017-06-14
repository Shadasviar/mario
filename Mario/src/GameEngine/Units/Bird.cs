using System;
using Global;
using Mario.Properties;
using System.Drawing;

namespace GameEngine
{
    class Bird : Mob
    {
        private Animator left, right;
        public Bird(Coordinates position):base(position,new Speed(Settings.Default.standardMoveSpeed))
        {
            left = new AsyncAnimator(new System.Collections.Generic.List<Image>
            {
                new Bitmap(Resources.bird1),
                new Bitmap(Resources.bird1),
                new Bitmap(Resources.bird3),
                new Bitmap(Resources.bird4),
            }, 25);

            right = new AsyncAnimator(new System.Collections.Generic.List<Image>
            {
                new Bitmap(Resources.rbird1),
                new Bitmap(Resources.rbird1),
                new Bitmap(Resources.rbird3),
                new Bitmap(Resources.rbird4),
            }, 25);

            this.currentSpeed.setVerticalSpeed(0);
            this.animator = right;
        }


        override public void setVerticalSpeed(int v)
        {
            currentSpeed.setVerticalSpeed(v);
        }


        override public void setHorizontalSpeed(int v)
        {
            currentSpeed.setHorizontalSpeed(v);
            if (currentSpeed.getHorizontalSpeed() > 0) animator = right;
            if (currentSpeed.getHorizontalSpeed() < 0) animator = left;
            animator.reset();
        }


        public override Image getTexture()
        {
            return animator.getNextFrame();
        }
    }
}
