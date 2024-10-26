using Game3.Screens;
using System.Collections.Generic;

namespace Game3
{
    public struct World
    {
        public int WorldIndex;

        public string WorldName;

        public List<Level> Levels;

        public World (int index, string name, List<Level> levels)
        {
            WorldIndex = index;
            WorldName = name;
            Levels = levels;
        }
    }
}
