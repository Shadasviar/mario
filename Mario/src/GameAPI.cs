using System;
using System.Collections.Generic;
using System.Drawing;

namespace Global

{
    interface GameAPI
    {
        void nextFrame();
        List<Coordinates> getAllUnitsCoordinates();
        bool playerIsAlive();
        bool playerWon();
        List<Tuple<Coordinates, Image>> getAllUnitsCoordinatesImages();
        Coordinates getPlayerPosition(int playerNumber = 0);
    }
}
