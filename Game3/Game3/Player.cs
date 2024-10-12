using Game3.Bounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game3
{
    public class Player : PhysicsObject
    {
        const float IDLE_ANIMATION_TIME = 35;
        const float MOVING_ANIMATION_TIME = 5;
        const float WING_ANIMATION_TIME = 10;

        Texture2D texture;

        int maxJumps = 2;
        int numJumps = 2;

        short animationFrame = 0;
        short wingFrame = 0;
        float animationTimer = 0;
        float wingTimer = 0;
        float animationTimerMovement = 0;
        bool animateForwards = true;

        public Player(Vector2 position) : base(position, new BoundingRectangle(position, 88, 128))
        {
            Width = 128;
            Height = 128;
        }

        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("Penguin");
        }

        public override void Update(GameTime gameTime)
        {
            if (Position.X <= 44 && Velocity.X < 0) Velocity.X = 0;
            base.Update(gameTime);

            if (Velocity.X == 0) animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 100;
            if (Velocity.X != 0)
            {
                animationTimerMovement += (float)gameTime.ElapsedGameTime.TotalSeconds * 100;
                wingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 100;
            }

            if ((Velocity.X == 0 && animationTimer >= IDLE_ANIMATION_TIME) || (Velocity.X != 0 && animationTimerMovement >= MOVING_ANIMATION_TIME))
            {
                if (animationFrame == 2) animateForwards = false;
                else if (animationFrame == 0) animateForwards = true;
                
                if (animateForwards) animationFrame++;
                else animationFrame--;

                if (Velocity.X == 0) animationTimer -= IDLE_ANIMATION_TIME;
                if (Velocity.X != 0) animationTimerMovement -= MOVING_ANIMATION_TIME;
            }

            if (Velocity.X != 0 && wingTimer >= WING_ANIMATION_TIME)
            {
                if (wingFrame == 2) wingFrame = 0;
                else wingFrame = 2;

                wingTimer -= WING_ANIMATION_TIME;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            short y = 0;
            if (Velocity.X < 0) y = (short)(2 + wingFrame);
            else if (Velocity.X > 0) y = (short)(1 + wingFrame);

            spriteBatch.Draw(texture, Position, new Rectangle(animationFrame * Width, y * Height, Width, Height), Color.White, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(texture, Position, new Rectangle(animationFrame * Width + Width - (int)Bounds.GetWidth(), y * (int)Bounds.GetHeight(), (int)Bounds.GetWidth(), (int)Bounds.GetHeight()), Color.Red, 0, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0);
        }

        public void Jump(GameTime gameTime)
        {
            if (numJumps > 0)
            {
                Velocity.Y = -800 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                numJumps--;
            }
        }

        public override void UpdateCollision(PhysicsObject obj, CollisionType collision)
        {
            if (collision == CollisionType.Bottom) numJumps = maxJumps;
            base.UpdateCollision(obj, collision);
        }
    }
}
