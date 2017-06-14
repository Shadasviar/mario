using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Mario.Properties;

namespace GameEngine
{
    class Animator
    {
        protected List<Image> frames;
        protected int fps;
        protected int fps_cnt;
        protected int current;

        public void reset ()
        {
            current = 0;
        }


        virtual public Image getNextFrame()
        {
            if (fps_cnt++ >= Settings.Default.fps / fps) { ++current; fps_cnt = 0; }
            if (current == frames.Count) reset();
            return frames[current];
        }


        public Image getCurrentFrame()
        {
            return frames[current];
        }


        public Animator (List<Image> frames, int game_fps = 0)
        {
            this.frames = frames;
            this.fps = game_fps <= 0 ? Settings.Default.fps : game_fps;
        } 

    }
}
