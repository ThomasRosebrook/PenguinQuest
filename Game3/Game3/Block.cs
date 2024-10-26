using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public struct Block
    {
        public Rectangle Tile;

        public Vector2 Position;

        public Block (Rectangle index, Vector2 position)
        {
            Tile = index;
            Position = position;
        }
    }
}
