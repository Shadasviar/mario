using Mario.Properties;
using Global;

namespace GameEngine
{
    class Mob : Unit
    {
        public Mob (Coordinates position, Speed s):base(position,s)
        {
            this.priority = Settings.Default.mobPriority;
        }

    }
}
