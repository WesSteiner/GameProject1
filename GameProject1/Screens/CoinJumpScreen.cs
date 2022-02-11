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
        private Person _player;
        private Coin[] _coins;

        public InputManager inputManager;

        private readonly Random _random = new Random();
        private readonly ScreenManager _screenManager;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public CoinJumpScreen(ScreenManager sM)
        {
            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape },
                true
                );
            _screenManager = sM;
        }
    }
}
