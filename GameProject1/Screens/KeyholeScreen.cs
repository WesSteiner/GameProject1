using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameProject1.StateManagement;
using GameProject1.Collisions;

namespace GameProject1.Screens
{
    public class KeyholeScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        public InputManager inputManager;
        public List<Keyhole> keyholePositions = new List<Keyhole>();
        public Key key;

        private readonly Random _random = new Random();
        private readonly ScreenManager _screenManager;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public KeyholeScreen(ScreenManager sM)
        {
            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, 
                true
                );
            _screenManager = sM;
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("PixelFont");
            foreach (Keyhole k in keyholePositions) k.LoadContent();
            key.LoadContent();
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

            
            key.Update(gameTime, keyholePositions, inputManager);

            if (inputManager.Exit) ExitScreen();

            if(inputManager.Click && key.Click1)
            {
                _screenManager.AddScreen(new CoinJumpScreen(), null);
            }

            if(inputManager.Click && key.Click2)
            {
                ExitScreen();
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            foreach (Keyhole k in keyholePositions) k.Draw(spriteBatch);
            spriteBatch.DrawString(_gameFont, "Main Menu", new Vector2(150, 100), Color.Gold);
            spriteBatch.DrawString(_gameFont, "Play", new Vector2(350, 150), Color.Gold);
            spriteBatch.DrawString(_gameFont, "Exit", new Vector2(550, 150), Color.Gold);
            key.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
