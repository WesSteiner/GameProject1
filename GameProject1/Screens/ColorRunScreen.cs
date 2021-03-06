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
        private Game1 _game;
        private IParticleEmitter _gameEmitter;
        private Texture2D _foreground;

        public List<Cube> _cubes = new List<Cube>();
        public Person _player;

        public bool Win { get; set; } = false;

        public ColorRunScreen(Game1 game, IParticleEmitter gameEmitter)
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

            foreach(Cube cube in _cubes) { cube.Update(gameTime); }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            base.HandleInput(gameTime, input);
        }

        public override void Draw(GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            
            var spriteBatch = ScreenManager.SpriteBatch;

            float playerY = MathHelper.Clamp(_player.position.Y, 300, 9800);
            float offsetY = 300 - playerY;

            Matrix transform = Matrix.CreateTranslation(0, offsetY, 0);
            // Foreground

            transform = Matrix.CreateTranslation(0, offsetY, 0);
            spriteBatch.Begin(transformMatrix: transform);
            spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
            _player.Draw(gameTime, spriteBatch);
            foreach (Cube cube in _cubes) { cube.Draw(); }
            spriteBatch.End();
        }
    }
}
