using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3
{
    public class SparkleParticleSystem : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.White,
            Color.SkyBlue,
            Color.Yellow
        };

        Color color;

        public SparkleParticleSystem(Game game, int maxSparks) : base(game, maxSparks)
        {

        }

        protected override void InitializeConstants()
        {
            textureFilename = "Spark";

            minNumParticles = 15;
            maxNumParticles = 20;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            float angle = RandomHelper.NextFloat(0, MathHelper.TwoPi);
            var velocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * RandomHelper.NextFloat(40, 200);
            var lifetime = RandomHelper.NextFloat(0.5f, 0.8f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var scale = RandomHelper.NextFloat(1, 2);

            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            particle.Scale = .1f + .25f * normalizedLifetime;
        }

        public void PlaceSpark(Vector2 where)
        {
            color = colors[RandomHelper.Next(colors.Length)];
            AddParticles(where);
        }
    }
}
