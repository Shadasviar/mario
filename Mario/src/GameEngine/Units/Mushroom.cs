using System;
using System.Collections.Generic;
using System.Linq;
using Global;
using Mario.Properties;
namespace GameEngine
{
    class Mushroom : Mob
    {
        public Mushroom(Coordinates position):base(position,new Speed(Settings.Default.standardMoveSpeed))
        {
           
        }
    }
}
