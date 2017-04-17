using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Mario
{
    interface GameAPI
    {
        void nextFrame();
        List<Coordinates> getAllUnitsCoordinates();
        bool setLevel(int index);
        bool playerIsAlive();
        List<Tuple<Coordinates, Image>> getAllUnitsCoordinatesImages();
    }
}
