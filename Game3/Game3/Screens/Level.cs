using Game3.Bounds;
using Game3.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Game3.Screens
{
    public class Level : GameScreen
    {
        protected ContentManager _content;
        protected SpriteFont _gameFont;
        protected int Width;
        protected int Height;

        int Length = 4096;

        protected Player player;
        protected List<BlockGroup> blocks;
        protected List<Enemy> enemies;
        protected List<Obstacle> obstacles;

        double TimerZero = 0;
        protected double LevelTimer = 0;

        protected Texture2D Background;
        protected Texture2D Midground;
        protected Texture2D Foreground;

        protected Goal goal;
        private Vector2 goalPosition;

        public string LevelName { get; private set; } = "";

        public bool IsLoaded = false;

        public double BestTime = -1;

        Vector2 PlayerDefaultPosition;

        int KillPlane;

        Tilemap tilemap;

        public Level (string name, Goal goal, Tilemap tilemap)
        {
            LevelName = name;
            this.goal = goal;
            enemies = new List<Enemy>();
            obstacles = new List<Obstacle>();
            this.tilemap = tilemap;
            //blocks = this.tilemap.Blocks;
        }

        public override void Activate()
        {
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            

            if (Width <= 0) Width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (Height <= 0) Height = ScreenManager.GraphicsDevice.Viewport.Height;
            if (!tilemap.IsLoaded) tilemap.LoadContent(_content);
            Length = tilemap.Width * tilemap.TileWidth;
            TimerZero = 0;
            LevelTimer = 0;

            _gameFont = _content.Load<SpriteFont>("Lusitana");

            PlayerDefaultPosition = new Vector2(Width / 2, Height / 2);
            player = new Player(PlayerDefaultPosition);

            player.LoadContent(_content);

            /*
            foreach (Enemy enemy in enemies)
            {
                enemy.LoadContent(_content);
            }
            */
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.LoadContent(_content);
            }
            blocks = tilemap.Blocks;
            foreach (BlockGroup group in blocks)
            {
                group.LoadContent(_content);
            }

            goal.UpdatePosition(goalPosition);

            KillPlane = Height;

            Background = _content.Load<Texture2D>("Background");
            Midground = _content.Load<Texture2D>("Midground");
            Foreground = _content.Load<Texture2D>("Foreground");
            IsLoaded = true;
        }

        public override void Unload()
        {
            IsLoaded = false;
            _content.Unload();
            tilemap.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            if (IsActive && IsLoaded && !(otherScreenHasFocus || coveredByOtherScreen))
            {
                goal.Update(gameTime);
                player.Update(gameTime);

                if (player.Position.X + player.Width / 2 > Length) player.Position.X = Length - player.Width / 2;

                if (player.Position.Y > KillPlane)
                {
                    //ExitScreen();
                    Reload();
                    //ScreenManager.AddScreen(ScreenManager.GetCurrentLevel(), null);
                }

                foreach(Obstacle obstacle in obstacles)
                {
                    if (player.Bounds.CollidesWith(obstacle.Bounds) != CollisionType.None)
                    {
                        Reload();
                    }
                }

                foreach (BlockGroup group in blocks)
                {
                    CollisionType collision = player.Bounds.CollidesWith(group.Bounds);
                    if (collision != CollisionType.None)
                    {
                        player.UpdateCollision(group, collision);
                    }
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Update(gameTime);
                        if (player.Bounds.CollidesWith(enemy.Bounds) != CollisionType.None)
                        {
                            //ExitScreen();
                            Reload();
                            //ScreenManager.AddScreen(ScreenManager.GetCurrentLevel(), null);
                        }
                        collision = enemy.Bounds.CollidesWith(group.Bounds);
                        enemy.UpdateCollision(group, collision);
                    }
                }
                if (player.Bounds.CollidesWith(goal.Bounds) != CollisionType.None)
                {
                    //ExitScreen();
                    if (BestTime == -1 || LevelTimer - TimerZero < BestTime) BestTime = LevelTimer - TimerZero;
                    ScreenManager.RemoveScreen(this);
                    if (ScreenManager.HasNextLevel()) ScreenManager.AddScreen(ScreenManager.GetNextLevel(), null);
                    else ScreenManager.AddScreen(new WinScreen(), null);
                }
                if (TimerZero == 0) TimerZero = gameTime.TotalGameTime.TotalSeconds;
                LevelTimer = gameTime.TotalGameTime.TotalSeconds;
            }
            
        }

        public void Reload()
        {
            Activate();
            /*
            TimerZero = 0;
            LevelTimer = 0;
            player.Position = PlayerDefaultPosition;
            player.Velocity.X = 0;
            player.Velocity.Y = 0;
            */
        }


        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            base.HandleInput(gameTime, input);

            if (input.Exit)
            {
                //ExitScreen();
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new MainMenu(), null);
            }

            if (input.JumpStart) player.Jump(gameTime);
            player.Velocity.X = input.Direction.X * player.Speed;


        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            var spriteBatch = ScreenManager.SpriteBatch;

            float playerX = MathHelper.Clamp(player.Position.X, Width / 2, Length - Width / 2);
            float offsetX = Width / 2 - playerX;

            //Background
            Matrix transform = Matrix.CreateTranslation(offsetX * 0.333f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
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
            foreach(Obstacle obstacle in obstacles)
            {
                obstacle.Draw(gameTime, spriteBatch);
            }

            goal.Draw(gameTime, spriteBatch, offsetX);

            spriteBatch.End();

            spriteBatch.Begin();
            string currentText = $"{Math.Round(LevelTimer - TimerZero, 2)}";
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(0, 0), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddBlockGroup(BlockGroup group)
        {
            blocks.Add(group);
            //group.LoadContent(_content);
        }

        public void AddEnemy (Enemy enemy)
        {
            enemies.Add(enemy);
            //enemy.LoadContent(_content);
        }

        public void AddObstacle(Vector2 position, int width, int height, string textureName)
        {
            obstacles.Add(new Obstacle(position, width, height, textureName));
        }

        public void SetGoalPosition(Vector2 position)
        {
            goalPosition = position;
        }

        public string GetBestTime()
        {
            if (BestTime < 0) return "";
            return $"{Math.Round(BestTime, 2)}";
        }
    }
}
