using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace Game3.Bounds
{
    public struct BoundingRectangle : IBoundingShape
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;

        public float Left => X - Width / 2;

        public float Right => X + Width / 2;

        public float Top => Y - Height / 2;

        public float Bottom => Y + Height / 2;

        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        public CollisionType CollidesWith(IBoundingShape other)
        {
            if (other is BoundingRectangle rect)
            {
                return CollisionHelper.Collides(this, rect);
            }
            if (other is BoundingCircle circle)
            {
                return CollisionHelper.Collides(this, circle);
            }
            return CollisionType.None;
        }

        public Vector2 DistBetween(IBoundingShape other)
        {
            if (other is BoundingRectangle rect)
            {
                float distY = 0;
                float distX = 0;
                if (Y > rect.Y) distY = Top - rect.Bottom;
                else distY = rect.Top - Bottom;

                if (X > rect.X) distX = Left - rect.Right;
                else distX = rect.Left - Right;

                return new Vector2(distX, distY);
            }
            if (other is BoundingCircle circle)
            {
                float nearestX = MathHelper.Clamp(circle.Center.X, Left, Right);
                float nearestY = MathHelper.Clamp(circle.Center.Y, Top, Bottom);

                float distY = 0;
                float distX = 0;
                if (Y > circle.Center.Y) distY = Top - nearestY;
                else distY = nearestY - Bottom;

                if (X > circle.Center.X) distX = Left - nearestX;
                else distX = nearestX - Right;

                return new Vector2(distX, distY);
            }
            return new Vector2(0, 0);
        }

        public Vector2 GetPosition() => new Vector2(X, Y);

        public float GetWidth() => Width;

        public float GetHeight() => Height;

        public void UpdatePosition(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void UpdatePosition(Vector2 position)
        {
            X = position.X;
            Y = position.Y;
        }

        public void UpdateWidth(float width)
        {
            Width = width;
        }

        public void UpdateHeight(float height)
        {
            Height = height;
        }
    }
}
