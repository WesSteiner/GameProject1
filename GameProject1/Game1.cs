using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameProject1.Screens;
using GameProject1.StateManagement;
using GameProject1.Collisions;
using System.Collections.Generic;
using System;

namespace GameProject1
{
    public class Game1 : Game, IParticleEmitter
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private ScreenManager _screenManager;
        private InputManager _inputManager;

        //private SoundEffect coin;
        private Song backgroundMusic;

        private double deaths = 0;
        private double o2Collected = 0;
        private double rocksHit = 0;
        private double oxygen = 100;
        private double timer;
        private double timer2;
        Color o2TankColor = Color.CornflowerBlue;
        Color rockColor = Color.RosyBrown;

        private Dot restart;
        private Dot exit;

        private SpriteFont _gameFont;
        private Texture2D _foreground;
        public List<Coin> _coins = new List<Coin>();
        public List<Rock> _rocks = new List<Rock>();
        private SoundEffect coinSound;

        public Person _player;

        public bool Win { get; set; } = false;

        public bool Lose { get; set; } = false;

        public ColorRunScreen CRN;

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            _player = new Person(this, Color.White);
            restart = new Dot(this, o2TankColor, new Vector2(620, 9700));
            exit = new Dot(this, rockColor, new Vector2(320, 9700));

            AdditionalScreens();
        }

        private void AdditionalScreens()
        {
            _screenManager.AddScreen(new MainMenuScreen(), null);              
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here            
            base.Initialize();

            _inputManager = new InputManager();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundMusic = Content.Load<Song>("8-bit-beebop");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            _gameFont = Content.Load<SpriteFont>("PixelFont");
            _player.LoadContent();
            _foreground = Content.Load<Texture2D>("foreground");

            Random rand = new Random();            
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            _coins.Add(new Coin(this, o2TankColor, rand));
            foreach (Coin coin in _coins) { coin.LoadContent(); }
                        
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            _rocks.Add(new Rock(this, rockColor, rand));
            foreach (Rock rock in _rocks) { rock.LoadContent(); }

            restart.LoadContent();
            exit.LoadContent();

            coinSound = Content.Load<SoundEffect>("Pickup_Coin15");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);

            if (_player.position.Y >= 9680) Win = true;
            else if (oxygen <= 0) Lose = true;
            else
            {
                Win = false;
                Lose = false;
            }

            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 1 && !Win) 
            { 
                oxygen -= 2;
                if (oxygen == 0) Lose = true;
                timer -= 1;                
            }
            
            foreach(Coin coin in _coins)
            {
                if(CollisionHelper.Collides(coin.Bounds, _player.Bounds))
                {
                    oxygen += 30;
                    coin.Collected = true;
                    coinSound.Play();
                    if (oxygen > 100) oxygen = 100;
                    o2Collected++;
                }
            }

            foreach(Rock rock in _rocks)
            {
                if(CollisionHelper.Collides(rock.Bounds, _player.Bounds))
                {
                    oxygen -= 20;
                    if (oxygen == 0) Lose = true;
                    rock.Collected = true;
                    //rock sound
                    rocksHit++;
                }
            }

            if (CollisionHelper.Collides(restart.Bounds, _player.Bounds))
            {
                _player.position = new Vector2(330, 300);
                oxygen = 100;
                deaths = 0;
                rocksHit = 0;
                o2Collected = 0; 
            }

            if (CollisionHelper.Collides(exit.Bounds, _player.Bounds))
            {
                this.Exit();
            }

            _player.Update(gameTime);
            foreach (Coin coin in _coins) { coin.Update(gameTime); }
            foreach (Rock rock in _rocks) { rock.Update(gameTime); }
            restart.Update(gameTime);
            exit.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            float playerY = MathHelper.Clamp(_player.position.Y, 300, 9800);
            float offsetY = 300 - playerY;

            Matrix transform = Matrix.CreateTranslation(0, offsetY, 0);
            // Foreground

            transform = Matrix.CreateTranslation(0, offsetY, 0);
            _spriteBatch.Begin(transformMatrix: transform);
            _spriteBatch.Draw(_foreground, Vector2.Zero, Color.White);
            _player.Draw(gameTime, _spriteBatch);
            foreach (Coin coin in _coins) { coin.Draw(_spriteBatch); }
            foreach (Rock rock in _rocks) { rock.Draw(_spriteBatch); }
            restart.Draw(_spriteBatch);
            exit.Draw(_spriteBatch);
            _spriteBatch.End();

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_gameFont, "Oxygen:" + oxygen + "%", new Vector2(10, 10), o2TankColor);
            timer2 += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer2 < 15)
            {
                oxygen = 100;
                _spriteBatch.DrawString(_gameFont, "The ISS has been torn to shreds by space", new Vector2(10, 50), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "rocks!", new Vector2(10, 80), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "The oxygen tanks have scattered!", new Vector2(10, 120), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "Avoid rocks and collect more oxygen to", new Vector2(10, 160), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "survive the decent to earth!", new Vector2(10, 190), o2TankColor);
            }
            if (Lose)
            {
                deaths++;
                _player.position = new Vector2(330, 300);
                oxygen += 100;
            }
            else if (Win)
            {
                _spriteBatch.DrawString(_gameFont, "Deaths:" + deaths, new Vector2(10, 50), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "O2 Collected:" + o2Collected + "/10", new Vector2(10, 90), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "Rocks Hit:" + rocksHit, new Vector2(10, 130), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "Collect the blue dot to restart!", new Vector2(10, 200), o2TankColor);
                _spriteBatch.DrawString(_gameFont, "Collect the red dot to exit.", new Vector2(10, 240), o2TankColor);
            }
            _spriteBatch.End();
        }
    }
}
