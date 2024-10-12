using System;
using System.Runtime.CompilerServices;
using Game3.Bounds;
using Microsoft.Xna.Framework;

namespace Game3
{
    public class PhysicsObject
    {
        public Vector2 Velocity = new Vector2();

        public float Speed = 300f;

        public Vector2 Acceleration = new Vector2(0, 20f);

        public Vector2 Position = new Vector2();
        public Vector2 LastPosition;

        public IBoundingShape Bounds;

        public int Width;

        public int Height;

        public PhysicsObject(Vector2 initialPosition, BoundingRectangle bounds)
        {
            Position = initialPosition;
            LastPosition = Position;
            Bounds = bounds;
        }

        public PhysicsObject(Vector2 initialPosition, BoundingCircle bounds)
        {
            Position = initialPosition;
            LastPosition = Position;
            Bounds = bounds;
        }

        public virtual void Update(GameTime gameTime)
        {
            float Time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity += Acceleration * Time * new Vector2(Speed,1);

            LastPosition = Position;
            Position += Velocity;
            Bounds.UpdatePosition(Position);
        }

        public virtual void UpdateCollision(PhysicsObject obj, CollisionType collision)
        {
            bool isThisStatic = Position == LastPosition;
            bool isObjStatic = obj.Position == obj.LastPosition;

            if (isObjStatic)
            {
                Vector2 Direction = Position - LastPosition;

                if (collision == CollisionType.Top || collision == CollisionType.Bottom)
                {
                    Position.Y -= Direction.Y;
                    Velocity.Y = 0;
                }
                else if (collision == CollisionType.Left || collision == CollisionType.Right)
                {
                    Position.X -= Direction.X;
                    Velocity.X = 0;
                }
                else
                {
                    Position -= Direction;
                    Velocity -= Velocity;
                }
                Bounds.UpdatePosition(Position);
            }
            else if (isThisStatic)
            {
                Vector2 Direction = obj.Position - obj.LastPosition;

                if (collision == CollisionType.Top || collision == CollisionType.Bottom)
                {
                    obj.Position.Y -= Direction.Y;
                    obj.Velocity.Y = 0;
                }
                else if (collision == CollisionType.Left || collision == CollisionType.Right)
                {
                    obj.Position.X -= Direction.X;
                    obj.Velocity.X = 0;
                }
                else
                {
                    obj.Position -= Direction;
                    obj.Velocity -= obj.Velocity;
                }
                obj.Bounds.UpdatePosition(obj.Position);
            }
            else
            {
                Vector2 ThisDirection = Position - LastPosition;
                Vector2 ObjDirection = obj.Position - obj.LastPosition;

                if (collision == CollisionType.Top || collision == CollisionType.Bottom)
                {
                    Position.Y -= ThisDirection.Y;
                    Velocity.Y = 0;
                    obj.Position.Y -= ObjDirection.Y;
                    obj.Velocity.Y = 0;
                }
                else if (collision == CollisionType.Left || collision == CollisionType.Right)
                {
                    Position.X -= ThisDirection.X;
                    Velocity.X = 0;
                    obj.Position.X -= ObjDirection.X;
                    obj.Velocity.X = 0;
                }
                else
                {
                    Position -= ThisDirection;
                    Velocity -= Velocity;
                    obj.Position -= ObjDirection;
                    obj.Velocity -= Velocity;
                }
                Bounds.UpdatePosition(Position);
                obj.Bounds.UpdatePosition(obj.Position);
            }
        }
    }
}
