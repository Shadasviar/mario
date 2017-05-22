using System.Drawing;
using Mario.Properties;
using Global;

namespace GameEngine
{
    class DieUnitBlock : GroundUnit
    {
        public DieUnitBlock(Coordinates position) : base(position)
        {
            texture = new Bitmap(Resources.empty);
        }
    }
}
