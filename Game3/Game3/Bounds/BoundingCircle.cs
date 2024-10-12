using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;

namespace Game3.Bounds
{
    public struct BoundingCircle : IBoundingShape
    {
        public Vector2 Center;

        public Vector2 PreviousCenter;

        public float Radius;

        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            PreviousCenter = center;
            Radius = radius;
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
                float nearestX = MathHelper.Clamp(Center.X, rect.Left, rect.Right);
                float nearestY = MathHelper.Clamp(Center.Y, rect.Top, rect.Bottom);

                float distY = 0;
                float distX = 0;
                if (rect.Y > Center.Y) distY = rect.Top - nearestY;
                else distY = nearestY - rect.Bottom;

                if (rect.X > Center.X) distX = rect.Left - nearestX;
                else distX = nearestX - rect.Right;

                return new Vector2(distX, distY);
            }
            if (other is BoundingCircle circle)
            {
                float distY = 0;
                float distX = 0;
                if (Center.Y > circle.Center.Y) distY = Center.Y - Radius - (circle.Center.Y + circle.Radius);
                else distY = circle.Center.Y - circle.Radius - (Center.Y + Radius);

                if (Center.X > circle.Center.X) distX = Center.X - Radius - (circle.Center.X + circle.Radius);
                else distX = circle.Center.X - circle.Radius - (Center.X + Radius);

                return new Vector2(distX, distY);
            }
            return new Vector2(0, 0);
        }

        public Vector2 GetPosition() => Center;

        public float GetWidth() => Radius;

        public float GetHeight() => Radius;

        public void UpdatePosition(float x, float y)
        {
            PreviousCenter = Center;
            Center = new Vector2(x, y);
        }

        public void UpdatePosition(Vector2 position)
        {
            Center.X = position.X;
            Center.Y = position.Y;
        }

        public void UpdateWidth(float width) => UpdateRadius(width);

        public void UpdateHeight(float height) => UpdateRadius(height);

        void UpdateRadius(float diameter)
        {
            Radius = diameter / 2;
        }
    }
}
