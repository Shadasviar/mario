using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine
{
    class Animator
    {
        List<Image> fragmens;
        public void reset ()
        {
            return;
        }
        public Image getNextFrame()
        {
            return new Image();
        }
        public Animator (List<Image> frames, int game_fps = 0)
        {

        } 

    }
}
