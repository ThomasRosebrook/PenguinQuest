using Microsoft.Xna.Framework;
using Game3.Bounds;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game3
{
    public class Obstacle : PhysicsObject
    {
        Texture2D texture;
        string textureName;

        public Obstacle (Vector2 position, int width, int height, string textureName): base(position, new BoundingRectangle(position, width, height))
        {
            this.textureName = textureName;
            Width = width;
            Height = height;
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>(textureName);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, new Rectangle(0, 0, Width, Height), Color.White, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
