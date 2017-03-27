using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mario
{
    interface GameAPI
    {
        bool move(Coordinates src, Coordinates dst);
        bool playerIsLaive();
        World getWorld(int index);
        bool addWorld(World world);

    }
}
