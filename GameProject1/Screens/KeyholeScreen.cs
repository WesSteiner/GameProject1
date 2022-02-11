using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameProject1.StateManagement;

namespace GameProject1.Screens
{
    public class KeyholeScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private Vector2 _mousePosition;
        private Vector2[] keyholePositions;

        private readonly Random _random = new Random();

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public KeyholeScreen()
        {
            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, 
                true
                );
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("PixelFont");
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
