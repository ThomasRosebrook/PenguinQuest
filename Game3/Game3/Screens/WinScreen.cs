using Microsoft.Xna.Framework;
using Game3.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game3.Screens
{
    public class WinScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _menuFont;
        private SpriteFont _gameFont;
        private int width;
        private int height;

        Texture2D Background;
        Texture2D Midground;
        Texture2D Foreground;

        Texture2D Penguini;

        public WinScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Activate()
        {
            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            if (width <= 0) width = ScreenManager.GraphicsDevice.Viewport.Width;
            if (height <= 0) height = ScreenManager.GraphicsDevice.Viewport.Height;

            Background = _content.Load<Texture2D>("Background");
            Midground = _content.Load<Texture2D>("Midground");
            Foreground = _content.Load<Texture2D>("Foreground");
            _menuFont = _content.Load<SpriteFont>("LusitanaMenu");
            _gameFont = _content.Load<SpriteFont>("Lusitana");
            Penguini = _content.Load<Texture2D>("Penguin");
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
            spriteBatch.Draw(Foreground, new Vector2(0, 533), Color.White);

            spriteBatch.Draw(Penguini, new Vector2(width / 2 - 64, height / 2), new Rectangle(0, 0, 128, 128), Color.White);

            string currentText = "YOU WON!";
            Vector2 size = _menuFont.MeasureString(currentText);
            spriteBatch.DrawString(_menuFont, currentText, new Vector2(width / 2 - size.X / 2, 20), Color.HotPink);

            currentText = "Press SPACE or A Button to Restart";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, 200), Color.DarkBlue);

            currentText = "Exit: ESC or Back";
            size = _gameFont.MeasureString(currentText);
            spriteBatch.DrawString(_gameFont, currentText, new Vector2(width / 2 - size.X / 2, height - size.Y), Color.DarkBlue);

            spriteBatch.End();
        }
    }
}
