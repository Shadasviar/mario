using Mario.Properties;
using System.Collections.Generic;
using System.Drawing;


namespace GameEngine
{
    class AsyncAnimator : Animator
    {
        public AsyncAnimator(List<Image> frames, int fps = 0):base(frames, fps)
        {

        }

        override public Image getNextFrame()
        {
            if (fps_cnt++ >= (Settings.Default.fps * Settings.Default.players_number) / fps) { ++current; fps_cnt = 0; }
            if (current == frames.Count) reset();
            return frames[current];
        }

    }
}
