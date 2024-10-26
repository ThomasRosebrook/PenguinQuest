using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace Game3
{
    public class Powerup : PhysicsObject
    {
        PowerupType PowerupType;

        Texture2D texture;

        public Powerup(Vector2 position, PowerupType type) : base(position, new BoundingRectangle(position, 88, 128))
        {
            Width = 128;
            Height = 128;
            PowerupType = type;
        }

        public override void Update(GameTime gameTime)
        {
            //Velocity.X -= 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Enemy");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, Width, Height), Color.White, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
        }


    }

    public enum PowerupType
    {
        DoubleJump
    }
}
