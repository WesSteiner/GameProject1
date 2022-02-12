using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameProject1.Screens;
using GameProject1.StateManagement;
using GameProject1.Collisions;
using Microsoft.Xna.Framework.Content;

namespace GameProject1.Screens
{
    public class CoinJumpScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;
        private Game _game;

        public Person _player;
        public bool Win { get; set; } = false;

        public List<Coin> _coins = new List<Coin>();
        private int counter;

        public InputManager inputManager;

        private readonly Random _random = new Random();
        private readonly ScreenManager _screenManager;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public CoinJumpScreen(ScreenManager sM, Game game)
        {
            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape },
                true
                );
            _screenManager = sM;
            _game = game;
            counter = 0;
        }

        public override void Activate()
        {
            base.Activate();

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("PixelFont");
            foreach (Coin c in _coins) c.LoadContent();
            _player.LoadContent();
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
            System.Random random = new System.Random();
            _player.Update(gameTime, _coins);

            foreach(Coin c in _coins)
            {
                c.Update(gameTime);
                if(!c.Collected && c.Bounds.CollidesWith(_player.Bounds))
                {
                    c.Collected = true;
                    counter++;
                }
                if(counter == _coins.Count)
                {
                    Win = true;
                }
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

            foreach (Coin c in _coins) c.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            spriteBatch.DrawString(_gameFont, "Coins: " + counter, new Vector2(2, 2), Color.Gold);
            if (Win)
            {
                spriteBatch.DrawString(_gameFont, "You did it!", new Vector2(2, 200), Color.Gold);
                spriteBatch.DrawString(_gameFont, "It's not a lot,", new Vector2(2, 250), Color.Gold);
                spriteBatch.DrawString(_gameFont, "but in the future he'll jump!", new Vector2(2, 300), Color.Gold);
            }

            spriteBatch.End();
        }
    }
}
