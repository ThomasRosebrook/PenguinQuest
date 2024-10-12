using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.Bounds
{
    public interface IBoundingShape
    {
        public CollisionType CollidesWith(IBoundingShape other);

        public Vector2 GetPosition();

        public float GetWidth();

        public float GetHeight();

        public Vector2 DistBetween(IBoundingShape other);

        public void UpdatePosition(float x, float y);

        public void UpdatePosition(Vector2 position);

        public void UpdateWidth(float width);

        public void UpdateHeight(float height);
    }
}
