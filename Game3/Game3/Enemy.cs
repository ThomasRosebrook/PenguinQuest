using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game3
{
    public class Enemy : PhysicsObject
    {
        Texture2D texture;

        public Enemy(Vector2 position) : base(position, new BoundingRectangle(position, 128, 76))
        {
            Width = 128;
            Height = 76;
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Enemy");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Velocity.X -= 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, Width, Height), Color.White, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
        }

        public void Jump(GameTime gameTime)
        {
            Velocity.Y = -800 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
