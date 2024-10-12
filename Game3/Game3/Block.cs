using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game3
{
    public class Block : PhysicsObject
    {
        Texture2D texture;

        public Block(Vector2 position) : base(position, new BoundingRectangle(position, 128, 128))
        {
            Acceleration.Y = 0;
            Width = 128;
            Height = 128;
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Cube");
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, Width, Height), Color.White, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
