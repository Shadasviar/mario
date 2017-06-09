using System.Drawing;
using Global;
using Mario.Properties;
namespace GameEngine
{
    class Mushroom : Mob
    {
        public Mushroom(Coordinates position):base(position,new Speed(Settings.Default.standardMoveSpeed))
        {
            this.animator = new AsyncAnimator(new System.Collections.Generic.List<Image>
            {
                new Bitmap(Resources.mushroom1),
                new Bitmap(Resources.mushroom2),
            }, 10);
        }


        public override Image getTexture()
        {

            return animator.getNextFrame();
        }
    }
}
