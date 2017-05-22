using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global;
using System.Drawing;

namespace GameEngine
{
    class Coin : GroundUnit
    {
        public Coin(Coordinates position) : base(position)
        {
            texture = new Bitmap(Mario.Properties.Resources.coin);
        }
    }
}
