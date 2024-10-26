using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game3
{
    public class ArrowButton
    {
        public bool IsHighlighted = false;

        public ArrowType Arrow = ArrowType.Left;

        public Vector2 Position;

        public Color Color;

        Texture2D texture;

        BoundingRectangle bounds;

        public ArrowButton(ArrowType type, Vector2 position, Color color)
        {
            Arrow = type;
            Position = position;
            Color = color;
            bounds = new BoundingRectangle(position, 64, 64);
        }

        public bool IsColliding(Vector2 position)
        {
            IsHighlighted = bounds.CollidesWith(position);
            return IsHighlighted;
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("ArrowButton");
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = (Arrow == ArrowType.Right) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int highlight = IsHighlighted ? 64 : 0;
            spriteBatch.Draw(texture, Position, new Rectangle(highlight, 0,64,64), Color, 0, new Vector2(32, 32), 1f, flip, 0);
        }
    }
    public enum ArrowType
    {
        Left,
        Right
    }
}
