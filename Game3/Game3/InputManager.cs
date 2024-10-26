﻿using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game3
{
    public class InputManager
    {
        MouseState currentMouseState;
        MouseState previousMouseState;
        
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Vector2 Direction { get; private set; }

        public Vector2 MousePosition { get; private set; }

        public bool JumpStart { get; private set; } = false;
        public bool Jump { get; private set; } = false;

        public bool Click { get; private set; } = false;

        public bool Exit { get; private set; } = false;

        public void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            
            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(0);

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            MousePosition = currentMouseState.Position.ToVector2();
            Click = currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;

            Direction = new Vector2();

            if (currentKeyboardState.IsKeyDown(Keys.Left)
                || currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction = new Vector2(-time, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right)
                || currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(time, 0);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up)
                || currentKeyboardState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -time);
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down)
                || currentKeyboardState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, time);
            }

            if (Direction.X == 0 && Direction.Y == 0) Direction = currentGamePadState.ThumbSticks.Left * time * new Vector2(1, -1);

            JumpStart = false;
            Jump = currentKeyboardState.IsKeyDown(Keys.Space) || currentGamePadState.Buttons.A == ButtonState.Pressed;
            if (Jump && previousKeyboardState.IsKeyUp(Keys.Space) 
                    && previousGamePadState.Buttons.A == ButtonState.Released)
            {
                JumpStart = true;
            }

            Exit = false;
            if (currentGamePadState.Buttons.Back == ButtonState.Pressed && previousGamePadState.Buttons.Back != ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.Escape) && !previousKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit = true;
            }


        }
    }
}
