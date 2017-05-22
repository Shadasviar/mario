using System.Drawing;
using Global;
using Mario.Properties;
namespace GameEngine
{
    class Mushroom : Mob
    {
        public Mushroom(Coordinates position):base(position,new Speed(Settings.Default.standardMoveSpeed))
        {
            texture = new Bitmap(Resources.mushroom);
        }
    }
}
