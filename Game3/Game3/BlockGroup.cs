using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class BlockGroup : PhysicsObject
    {
        public List<Block> Blocks;

        public int BlockWidth { get; private set; }

        public int BlockHeight { get; private set; }

        public BlockGroup(Vector2 position, int width, int height) : base(position, new BoundingRectangle(position, 128 * width, 128 * height))
        {
            Acceleration.Y = 0;
            Blocks = new List<Block>();

            BlockWidth = width;
            BlockHeight = height;

            Width = BlockWidth * 128;
            Height = BlockHeight * 128;

            for (int i = 1; i <= BlockWidth; i++)
            {
                for (int j = 1; j <= BlockHeight; j++)
                {
                    Blocks.Add(new Block(new Vector2(position.X - Width / 2 + i * 128 - 64, position.Y - Height / 2 + j * 128 - 64)));
                }
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach(Block block in Blocks)
            {
                block.LoadContent(contentManager);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Block block in Blocks)
            {
                block.Draw(gameTime, spriteBatch);
            }
        }
    }
}
