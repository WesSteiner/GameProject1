using System;
using System.Collections.Generic;
using System.Text;
using GameProject1.Screens;
using GameProject1.StateManagement;
using GameProject1.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject1.Screens
{
    public class ColorRunScreen : GameScreen
    {
        public InputManager inputManager;
        
        private ContentManager _content;
        private SpriteFont _gameFont;
        private Game _game;
        private IParticleEmitter _gameEmitter;
        private Texture2D _foreground;

        public Tilemap _tilemap;

        public Person _player;
        public bool Win { get; set; } = false;

        public ColorRunScreen(Game game, IParticleEmitter gameEmitter)
        {
            _game = game;
            _gameEmitter = gameEmitter;
        }

        public override void Activate()
        {
            base.Activate();

            PixieParticalSystem pixie = new PixieParticalSystem(_game, _gameEmitter);
            _game.Components.Add(pixie);

            if (_content == null) _content = new ContentManager(ScreenManager.Game.Services, "Content");
           
            _gameFont = _content.Load<SpriteFont>("PixelFont");
            _player.LoadContent();
            _foreground = _content.Load<Texture2D>("foreground");
            _tilemap.LoadContent(_game.Content);
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

            _player.Update(gameTime);

            MouseState currentMouse = Mouse.GetState();
            Vector2 mousePosition = new Vector2(currentMouse.X, currentMouse.Y);
            
            _gameEmitter.Velocity = mousePosition - _gameEmitter.Position;
            _gameEmitter.Position = mousePosition;
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
            _player.Draw(gameTime, spriteBatch);
            spriteBatch.End();


            float playerY = MathHelper.Clamp(_player.position.Y, 300, 9800);
            float offsetY = 300 - playerY;

            Matrix transform = Matrix.CreateTranslation(0, offsetY, 0);
            // Foreground

            spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(23, 23, 0));
            _tilemap.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            transform = Matrix.CreateTranslation(0, offsetY, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
            _player.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
    }
}
