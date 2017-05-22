using System;
using System.Collections.Generic;
using GameEngine;
using Mario.Properties;

namespace Global
{
    using group = World.UnitGroupNames;
    class MapParser
    {
        class UnitData
        {
            public Type type;
            public group group;
            public int size;

            public UnitData(Type t, group grp, int s):this(t, grp)
            {
                size = s;
            }

            public UnitData(Type t, group grp)
            {
                type = t;
                group = grp;
                size = Settings.Default.standardSizeOfUnit;
            }
        }


        static Dictionary<char, UnitData> charUnitMap = new Dictionary<char, UnitData>
        {
            {'g', new UnitData(typeof(GroundUnit),  group.stat)},
            {'p', new UnitData(typeof(Player),      group.players,  Settings.Default.standardSizeOfPlayer)},
            {'m', new UnitData(typeof(Mushroom),    group.mobs,     Settings.Default.standardSizeOfPlayer)},
            {'d', new UnitData(typeof(DieUnitBlock),group.stat)},
            {'e', new UnitData(typeof(Door),        group.stat)},
            {'c', new UnitData(typeof(Coin),        group.stat,     Settings.Default.coinSize)},
        };


        public World parse(string name)
        {
            World result = new World();

            string[] lines = System.IO.File.ReadAllLines(name);
            int height = lines.Length*Settings.Default.standardSizeOfUnit;
            for (int i = 0; i < lines.Length;i++)
            {
                for(int j = 0; j <lines[i].Length;j++)
                {
                    if(charUnitMap.ContainsKey(lines[i][j]))
                    {
                        result.addUnit((Unit)Activator.CreateInstance (charUnitMap[lines[i][j]].type,new Coordinates(
                             j * Settings.Default.standardSizeOfUnit,
                            height - i * Settings.Default.standardSizeOfUnit,
                             j * Settings.Default.standardSizeOfUnit + charUnitMap[lines[i][j]].size,
                            height - i * Settings.Default.standardSizeOfUnit + charUnitMap[lines[i][j]].size)
                            
                            ), charUnitMap[lines[i][j]].group);
                    }
                }
            }
            result.initPlayer();
            return result;
        }
    }
}
