using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Mario.Properties;

namespace Global
{
    class MapParser
    {
        static Dictionary<char, Type> dictionary = new Dictionary<char, Type>
        {
            {'g', typeof(GroundUnit) },
            {'p', typeof(Player) },
            {'m', typeof(Mushroom) },
            {'d', typeof(DieUnitBlock) }
        };

        static Dictionary<char, World.UnitGroupNames> groups = new Dictionary<char, World.UnitGroupNames>
        {
            {'g', World.UnitGroupNames.stat },
            {'p', World.UnitGroupNames.players },
            {'m', World.UnitGroupNames.mobs },
            {'d', World.UnitGroupNames.stat }
        };

        public World parse(string name)
        {
            World result = new World();

            string[] lines = System.IO.File.ReadAllLines(name);
            int height = lines.Length*Settings.Default.standardSizeOfBlock;
            for (int i = 0; i < lines.Length;i++)
            {
                for(int j = 0; j <lines[i].Length;j++)
                {
                    if(dictionary.ContainsKey(lines[i][j]))
                    {
                        result.addUnit((Unit)Activator.CreateInstance (dictionary[lines[i][j]],new Coordinates(
                             j * Settings.Default.standardSizeOfBlock,
                            height - i * Settings.Default.standardSizeOfBlock,
                             j * Settings.Default.standardSizeOfBlock + Settings.Default.standardSizeOfBlock,
                            height - i * Settings.Default.standardSizeOfBlock + Settings.Default.standardSizeOfBlock)
                            
                            ), groups[lines[i][j]]);
                    }
                }
            }
            result.initPlayer();
            return result;
        }
    }
}
