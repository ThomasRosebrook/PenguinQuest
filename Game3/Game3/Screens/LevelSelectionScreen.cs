using Game3.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.Screens
{
    public class LevelSelectionScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _menuFont;
        private SpriteFont _gameFont;
        private int width;
        private int height;

        Texture2D Background;
        Texture2D Midground;
        Texture2D Foreground;

        ArrowButton BackWorldArrow;
        ArrowButton ForwardWorldArrow;
        ArrowButton BackLevelArrow;
        ArrowButton ForwardLevelArrow;

        public LevelSelectionScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            BackWorldArrow = new ArrowButton(ArrowType.Left, new Vector2(400, 220), Color.White);
            ForwardWorldArrow = new ArrowButton(ArrowType.Right, new Vector2(width - 400, 220), Color.White);
            BackLevelArrow = new ArrowButton(ArrowType.Left, new Vector2(400, 500), Color.White);
            ForwardLevelArrow = new ArrowButton(ArrowType.Right, new Vector2(width - 400, 500), Color.White);

            Background = _content.Load<Texture2D>("Background");
            Midground = _content.Load<Texture2D>("Midground");
            Foreground = _content.Load<Texture2D>("Foreground");
            _menuFont = _content.Load<SpriteFont>("LusitanaMenu");
            _gameFont = _content.Load<SpriteFont>("Lusitana");

            BackWorldArrow.LoadContent(_content);
            ForwardWorldArrow.LoadContent(_content);
            BackLevelArrow.LoadContent(_content);
            ForwardLevelArrow.LoadContent(_content);
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

            }
        }

        public override void HandleInput(GameTime gameTime, InputManager input)
        {
            base.HandleInput(gameTime, input);

            if (input.Click)
            {
                if (BackWorldArrow.IsColliding(input.MousePosition)) ScreenManager.IncWorld(-1);
                if (ForwardWorldArrow.IsColliding(input.MousePosition)) ScreenManager.IncWorld(1);
                if (BackLevelArrow.IsColliding(input.MousePosition)) ScreenManager.IncLevel(-1);
                if (ForwardLevelArrow.IsColliding(input.MousePosition)) ScreenManager.IncLevel(1);
            }

            if (input.Exit)
            {
                ScreenManager.AddScreen(new MainMenu(), null);
                this.ExitScreen();
            }
            if (input.JumpStart)
            {
                ScreenManager.AddScreen(ScreenManager.GetCurrentLevel(), null);
                this.ExitScreen();
            }

        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            //192, 108
            //1536, 864
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(Midground, new Vector2(0, 341), Color.White);

            string currentText = "Level Select";
            Vector2 size = _menuFont.MeasureString(currentText);
            spriteBatch.DrawString(_menuFont, currentText, new Vector2(width / 2 - size.X / 2, 20), Color.Black);

            currentText = $"{ScreenManager.GetCurrentWorld().WorldName}";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, 200), Color.Black);

            currentText = $"{ScreenManager.GetCurrentLevel().LevelName}";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, 500), Color.Black);

            currentText = ScreenManager.GetCurrentLevel().GetBestTime();
            if (currentText != "") currentText = $"Best: {currentText} Seconds";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, 550), Color.Black);

            currentText = "Space or A to select level";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, 700), Color.Black);

            currentText = "ESC to Return to Menu";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, height - size.Y), Color.Black);

            BackWorldArrow.Draw(gameTime, spriteBatch);
            ForwardWorldArrow.Draw(gameTime, spriteBatch);
            BackLevelArrow.Draw(gameTime, spriteBatch);
            ForwardLevelArrow.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
