using System;
using Microsoft.Xna.Framework;

namespace Game3
{
    public interface IParticleEmitter
    {
        public Vector2 Position { get; }

        public Vector2 Velocity { get; }

    }
}
