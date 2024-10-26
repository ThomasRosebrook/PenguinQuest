﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Game3.Screens;

namespace Game3.StateManagement
{
    public class ScreenManager : DrawableGameComponent
    {
        private readonly List<GameScreen> _screens = new List<GameScreen>();
        private readonly List<GameScreen> _tmpScreensList = new List<GameScreen>();

        private readonly ContentManager _content;
        private readonly LevelManager _levels;
        private readonly InputManager _input = new InputManager();

        private bool _isInitialized;

        public SpriteBatch SpriteBatch { get; private set; }

        public SpriteFont Font { get; private set; }

        public ScreenManager (Game game, LevelManager levels) : base(game)
        {
            _content = new ContentManager(game.Services, "Content");
            _levels = levels;
        }

        public override void Initialize()
        {
            base.Initialize();
            _isInitialized = true;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = _content.Load<SpriteFont>("Lusitana");

            foreach (var screen in _screens)
            {
                screen.Activate();
            }
        }

        protected override void UnloadContent()
        {
            foreach (var screen in _screens)
            {
                screen.Unload();
            }
        }

        public override void Update(GameTime gameTime)
        {
            _input.Update(gameTime);

            _tmpScreensList.Clear();
            _tmpScreensList.AddRange(_screens);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            while (_tmpScreensList.Count > 0)
            {
                var screen = _tmpScreensList[_tmpScreensList.Count - 1];
                _tmpScreensList.RemoveAt(_tmpScreensList.Count - 1);

                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
                {
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(gameTime, _input);
                        otherScreenHasFocus = true;
                    }

                    if (!screen.IsPopup) coveredByOtherScreen = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in _screens)
            {
                if (screen.ScreenState == ScreenState.Hidden) continue;

                screen.Draw(gameTime);
            }
        }

        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            if (_isInitialized) screen.Activate();
            _screens.Add(screen);
        }

        public void RemoveScreen(GameScreen screen)
        {
            if (screen is Level) _levels.SaveLevelData();
            if (_isInitialized) screen.Unload();

            _screens.Remove(screen);
            _tmpScreensList.Remove(screen);
        }

        public GameScreen[] GetScreens()
        {
            return _screens.ToArray();
        }

        public World GetCurrentWorld() => _levels.GetCurrentWorld();

        public int GetCurrentWorldIndex() => _levels.CurrentWorld;

        public Level GetCurrentLevel() => _levels.GetCurrentLevel();

        public Level GetNextLevel()
        {
            _levels.CurrentLevel++;
            return _levels.GetCurrentLevel();
        }

        public void SetCurrentLevel(int levelIndex)
        {
            _levels.CurrentLevel = levelIndex;
        }

        public void IncWorld(int incAmount)
        {
            if (incAmount + _levels.CurrentWorld < 0) SetCurrentWorld(_levels.NumWorlds - 1);
            else if (incAmount + _levels.CurrentWorld > _levels.NumWorlds - 1) SetCurrentWorld(0);
            else SetCurrentWorld(incAmount + _levels.CurrentWorld);
        }

        public void IncLevel(int incAmount)
        {
            if (incAmount + _levels.CurrentLevel < 0) SetCurrentLevel(_levels.NumLevels - 1);
            else if (incAmount + _levels.CurrentLevel > _levels.NumLevels - 1) SetCurrentLevel(0);
            else SetCurrentLevel(incAmount + _levels.CurrentLevel);
        }

        public void SetCurrentWorld(int worldIndex)
        {
            _levels.SetCurrentWorld(worldIndex);
        }

        public bool HasNextLevel() => _levels.HasNextLevel;

        public void FadeBackBufferToBlack(float alpha)
        {
            SpriteBatch.Begin();
            SpriteBatch.End();
        }

        public void Deactivate()
        {
        }

        public bool Activate()
        {
            return false;
        }

    }
}
