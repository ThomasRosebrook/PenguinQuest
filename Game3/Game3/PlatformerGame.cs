﻿using Game3.Screens;
using Game3.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game3
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        private SpriteBatch _spriteBatch;
        private LevelManager _levelManager;

        Goal goal;
        SparkleParticleSystem _sparks;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Penguin Quest";

            int width = 1536;
            int height = 864;

            goal = new Goal(new Vector2(4096,4096));

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _levelManager = new LevelManager(goal, width, height);
            _screenManager = new ScreenManager(this, _levelManager);
            Components.Add(_screenManager);

            

            AddInitialScreens();

            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new MainMenu(), null);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _sparks = new SparkleParticleSystem(this, 20);
            Components.Add(_sparks);
            goal.Sparks = _sparks;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            goal.LoadContent(Content);
            _levelManager.InitializeLevels(Content);
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}