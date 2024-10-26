using System;
using Microsoft.Xna.Framework;

namespace Game3.Bounds
{
    public static class CollisionHelper
    {
        public static int Width;
        public static int Height;

        public static CollisionType Collides(BoundingRectangle a, BoundingRectangle b)
        {
            bool xPreviouslyColliding;
            bool yPreviouslyColliding;
            CollisionType xCollision = CollisionType.None;
            CollisionType yCollision = CollisionType.None;

            if (!(a.Right < b.Left || a.Left > b.Right || a.Top > b.Bottom || a.Bottom < b.Top))
            {
                if (a.X < b.X)
                {
                    xCollision = CollisionType.Right;
                    xPreviouslyColliding = a.lastPos.X + a.Width / 2 > b.lastPos.X - b.Width / 2;
                }
                else
                {
                    xCollision = CollisionType.Left;
                    xPreviouslyColliding = a.lastPos.X - a.Width / 2 < b.lastPos.X + b.Width / 2;
                }

                if (a.Y < b.Y)
                {
                    yCollision = CollisionType.Bottom;
                    yPreviouslyColliding = a.lastPos.Y + a.Height / 2 > b.lastPos.Y - b.Height / 2;
                }
                else
                {
                    yCollision = CollisionType.Top;
                    yPreviouslyColliding = a.lastPos.Y - a.Height / 2 < b.lastPos.Y + b.Height / 2;
                }

                if (xPreviouslyColliding) return yCollision;
                if (yPreviouslyColliding) return xCollision;
                if (Math.Abs(a.lastPos.X - a.X) < Math.Abs(a.lastPos.Y - a.Y)) return xCollision;
                return yCollision;


            }

            return CollisionType.None;
        }

        public static CollisionType Collides(BoundingRectangle r, BoundingCircle c)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);

            //return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) + Math.Pow(c.Center.Y - nearestY, 2);
            return CollisionType.None;
        }

        public static CollisionType Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);

            //return Math.Pow(c.Radius, 2) >= Math.Pow(c.Center.X - nearestX, 2) + Math.Pow(c.Center.Y - nearestY, 2);
            return CollisionType.None;
        }

        public static CollisionType Collides(BoundingCircle a, BoundingCircle b)
        {
            //return Math.Pow(a.Radius + b.Radius, 2) >= Math.Pow(a.Center.X - b.Center.X, 2) + Math.Pow(a.Center.Y - b.Center.Y, 2);
            return CollisionType.None;
        }

        public static bool Collides(BoundingRectangle r, Vector2 point)
        {
            return r.Left < point.X && point.X < r.Right && r.Top < point.Y && point.Y < r.Bottom;
        }
    }

    public enum CollisionType 
    { 
        None,
        Top,
        Bottom,
        Left,
        Right
    }
}
