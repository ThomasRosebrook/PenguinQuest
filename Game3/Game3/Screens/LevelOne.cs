using Game3.Bounds;
using Game3.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game3.Screens
{
    public class LevelOne : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;
        private int Width;
        private int Height;

        private Player player;
        private List<BlockGroup> blocks;
        private List<Enemy> enemies;

        double TimerZero = 0;
        double Timer = 0;

        Texture2D Background;
        Texture2D Midground;
        Texture2D Foreground;

        Goal goal;

        public LevelOne(Goal goal)
        {
            this.goal = goal;
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (Width <= 0) Width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (Height <= 0) Height = ScreenManager.GraphicsDevice.Viewport.Height;

            _gameFont = _content.Load<SpriteFont>("Lusitana");

            player = new Player(new Vector2(Width / 2, Height / 2));
            player.LoadContent(_content);

            blocks = new List<BlockGroup>();
            blocks.Add(new BlockGroup(new Vector2(Width / 2, Height - 64), 128, 1));
            blocks.Add(new BlockGroup(new Vector2(1024, 400), 2, 1));
            blocks.Add(new BlockGroup(new Vector2(3000, 400), 5, 1));
            blocks.Add(new BlockGroup(new Vector2(2000, 200), 2, 2));
            foreach (BlockGroup group in blocks)
            {
                group.LoadContent(_content);
            }

            enemies = new List<Enemy>()
            {
                new Enemy(new Vector2(1200, Height - 170)),
                new Enemy(new Vector2(1400, Height - 170)),
                new Enemy(new Vector2(2000, Height - 170)),
                new Enemy(new Vector2(2200, Height - 170)),
                new Enemy(new Vector2(2200, Height - 180)),
                new Enemy(new Vector2(2500, Height - 170)),
                new Enemy(new Vector2(2800, Height - 170)),
                new Enemy(new Vector2(3000, Height - 170)),
                new Enemy(new Vector2(3200, Height - 170)),
                new Enemy(new Vector2(3700, Height - 170))
            };

            foreach (Enemy enemy in enemies)
            {
                enemy.LoadContent(_content);
            }

            goal.Position = new Vector2(4096, Height - 128);
            goal.Bounds.UpdatePosition(goal.Position);

            Background = _content.Load<Texture2D>("Background");
            Midground = _content.Load<Texture2D>("Midground");
            Foreground = _content.Load<Texture2D>("Foreground");
        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (IsActive)
            {
                goal.Update(gameTime);
                player.Update(gameTime);

                foreach (BlockGroup group in blocks)
                {
                    CollisionType collision = player.Bounds.CollidesWith(group.Bounds);
                    if (collision != CollisionType.None)
                    {
                        //Debug.WriteLine($"{collision}");
                        player.UpdateCollision(group, collision);
                    }
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Update(gameTime);
                        if (player.Bounds.CollidesWith(enemy.Bounds) != CollisionType.None)
                        {
                            ExitScreen();
                            ScreenManager.AddScreen(new LevelOne(goal), null);
                        }
                        collision = enemy.Bounds.CollidesWith(group.Bounds);
                        enemy.UpdateCollision(group,collision);
                    }
                }
                if (player.Bounds.CollidesWith(goal.Bounds) != CollisionType.None)
                {
                    ExitScreen();
                    ScreenManager.AddScreen(new WinScreen(goal), null);
                }
                
            }
            if (TimerZero == 0) TimerZero = gameTime.TotalGameTime.TotalSeconds;
            Timer = gameTime.TotalGameTime.TotalSeconds;
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            base.HandleInput(gameTime, input);

            if (input.Exit)
            {
                ExitScreen();
                ScreenManager.AddScreen(new MainMenu(goal), null);
            }

            if (input.JumpStart) player.Jump(gameTime);
            player.Velocity.X = input.Direction.X * player.Speed;

            
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);
            
            var spriteBatch = ScreenManager.SpriteBatch;

            float playerX = MathHelper.Clamp(player.Position.X, Width / 2, 4096);
            float offsetX = Width / 2 - playerX;

            //Background
            Matrix transform = Matrix.CreateTranslation(offsetX * 0.333f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(Background, new Vector2(0,0), Color.White);
            spriteBatch.End();

            //Midground
            transform = Matrix.CreateTranslation(offsetX * 0.666f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(Midground, new Vector2(0, 341), Color.White);
            spriteBatch.Draw(Midground, new Vector2(4096, 341), Color.White);
            spriteBatch.End();

            //Foreground
            transform = Matrix.CreateTranslation(offsetX, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(Foreground, new Vector2(0, 533), Color.White);
            spriteBatch.Draw(Foreground, new Vector2(4096, 533), Color.White);
            spriteBatch.Draw(Foreground, new Vector2(8192, 533), Color.White);
            player.Draw(gameTime, spriteBatch);
            foreach (BlockGroup group in blocks)
            {
                group.Draw(gameTime, spriteBatch);
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            goal.Draw(gameTime, spriteBatch, offsetX);

            spriteBatch.End();

            spriteBatch.Begin();
            string currentText = $"{offsetX} {Math.Round(Timer - TimerZero, 2)}";
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(0, 0), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
