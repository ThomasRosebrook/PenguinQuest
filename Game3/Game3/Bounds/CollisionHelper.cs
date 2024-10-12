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
            CollisionType xCollision = CollisionType.Left;
            CollisionType yCollision = CollisionType.Bottom;

            Vector2 Diagonal = new Vector2();
            
            if (!(a.Right < b.Left || a.Left > b.Right || a.Top > b.Bottom || a.Bottom < b.Top))
            {
                if (Math.Abs(a.Right - b.X) <= Math.Abs(a.Left - b.X))
                {
                    Diagonal.X = Math.Abs(a.Right - b.Left);
                    xCollision = CollisionType.Right;
                }
                else
                {
                    Diagonal.X = Math.Abs(a.Left - b.Right);
                    xCollision = CollisionType.Left;
                }

                if (Math.Abs(a.Bottom - b.Y) <= Math.Abs(a.Top - b.Y))
                {
                    Diagonal.Y = Math.Abs(a.Bottom - b.Top);
                    yCollision = CollisionType.Bottom;
                }
                else
                {
                    Diagonal.Y = Math.Abs(a.Top - b.Bottom);
                    yCollision = CollisionType.Top;
                }

                if (Diagonal.X > Diagonal.Y) return yCollision;
                return xCollision;

            }

            //!(a.Right < b.Left || a.Left > b.Right || a.Top > b.Bottom || a.Bottom < b.Top)

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
