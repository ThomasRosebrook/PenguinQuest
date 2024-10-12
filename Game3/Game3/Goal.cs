using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace Game3
{
    public class Goal : PhysicsObject
    {
        Texture2D texture;

        public SparkleParticleSystem Sparks;

        public Goal (Vector2 position) : base(position, new BoundingRectangle(position, 256, 72))
        {
            Acceleration.Y = 0;
            Width = 256;
            Height = 72;
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Boat");
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float offsetX)
        {
            Sparks.PlaceSpark(Position + new Vector2(offsetX,0) + new Vector2(RandomHelper.NextFloat(- Width / 2, Width / 2), RandomHelper.NextFloat(-Height / 2, Height / 2)));
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, Width, Height), Color.White, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
