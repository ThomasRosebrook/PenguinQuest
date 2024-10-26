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
        int BlockWidth = 0;

        int BlockHeight = 0;

        int numTilesWide = 0;

        int numTilesTall = 0;

        Texture2D _tileset;
        Texture2D cube;

        List<Block> blocks;

        public int BlockCount
        {
            get => blocks.Count;
        }

        public BlockType BlockType = BlockType.Default;


        public BlockGroup(Texture2D tileset, Vector2 position, int blockWidth, int blockHeight, BlockType type = BlockType.Default) : base(position, new BoundingRectangle(position, blockWidth, blockHeight))
        {
            Acceleration.Y = 0;

            _tileset = tileset;

            BlockWidth = blockWidth;
            BlockHeight = blockHeight;

            Width = 0;
            Height = 0;

            BlockType = type;
            blocks = new List<Block>();
        }

        public void LoadContent(ContentManager _content)
        {
            cube = _content.Load<Texture2D>("Cube");
        }

        public void AddBlock(Rectangle index, Vector2 position)
        {
            blocks.Add(new Block(index, position));
            if (position.X > Position.X + Width / 2 || position.X < Position.X - Width / 2)
            {
                numTilesWide++;
                Width += BlockWidth;
            }
            if (position.Y > Position.Y + Height / 2 || position.Y < Position.Y - Height / 2)
            {
                numTilesTall++;
                Height += BlockHeight;
            }

            Vector2 AveragePosition = new Vector2((Position.X * (numTilesWide - 1) + position.X) / numTilesWide, (Position.Y * (numTilesTall - 1) + position.Y) / numTilesTall);

            UpdatePosition(AveragePosition);
            //Update Position again to update past positions
            UpdatePosition(AveragePosition);
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(cube, Bounds.GetPosition(), new Rectangle(0, 0, (int)Bounds.GetWidth(), (int)Bounds.GetHeight()), Color.White, 0, new Vector2(Bounds.GetWidth() / 2, Bounds.GetHeight() / 2), 1f, SpriteEffects.None, 0);
            foreach (Block block in blocks)
            {
                spriteBatch.Draw(_tileset, block.Position, block.Tile, Color.White, 0, new Vector2(BlockWidth / 2, BlockHeight / 2), 1f, SpriteEffects.None, 0);
            }
            //spriteBatch.Draw(cube, Bounds.GetPosition(), new Rectangle(0, 0, (int)Bounds.GetWidth(), (int)Bounds.GetHeight()), Color.White, 0, new Vector2(Bounds.GetWidth() / 2, Bounds.GetHeight() / 2), 1f, SpriteEffects.None, 0);
        }
    }

    public enum BlockType
    {
        Default,
        Ice
    }
}
