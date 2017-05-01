using System;
using System.Collections.Generic;
using System.Drawing;

namespace Global

{
    interface GameAPI
    {
        void nextFrame();
        List<Coordinates> getAllUnitsCoordinates();
        bool setLevel(int index);
        bool playerIsAlive();
        List<Tuple<Coordinates, Image>> getAllUnitsCoordinatesImages();
        Coordinates getPlayerPosition();
    }
}
