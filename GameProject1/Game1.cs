using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameProject1.Screens;
using GameProject1.StateManagement;
using GameProject1.Collisions;
using System.Collections.Generic;

namespace GameProject1
{
    public class Game1 : Game, IParticleEmitter
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private ScreenManager _screenManager;
        private InputManager _inputManager;

        private SoundEffect coin;
        private Song backgroundMusic;

        private double oxygen = 100;
        private double timer;

        private SpriteFont _gameFont;
        private Texture2D _foreground;
        public List<Coin> _coins = new List<Coin>();
        public List<Rock> _rocks = new List<Rock>();
        private SoundEffect coinSound;

        public Person _player;

        public bool Win { get; set; } = false;

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

            AdditionalScreens();
        }

        private void AdditionalScreens()
        {
            _inputManager = new InputManager();
            System.Random rand = new System.Random();
            /*
            CRN = new ColorRunScreen(this, this) 
            {
                inputManager = inputManager,
                _player = new Person(this, Color.White),
                _cubes = { new Cube(this) }
            };

            _screenManager.AddScreen(CRN, null);

            
            _screenManager.AddScreen(new CoinJumpScreen(_screenManager, this)
            {
                _coins =
                {
                    new Coin(this, Color.White, new Vector2(150, 200)) { Direction = Direction.Down}, 
                    new Coin(this, Color.White, new Vector2(250, 200)) { Direction = Direction.Up},
                    new Coin(this, Color.White, new Vector2(350, 200)) { Direction = Direction.Down}, 
                    new Coin(this, Color.White, new Vector2(450, 200)) { Direction = Direction.Right}, 
                    new Coin(this, Color.White, new Vector2(550, 200)) { Direction = Direction.Left}
                },
                inputManager = inputManager,
                _player = new Person(this, Color.White)
            }, null);
            

            _screenManager.AddScreen(new KeyholeScreen(_screenManager)
            {
                keyholePositions =
                {
                    new Keyhole(this, Color.White) {Position = new Vector2(150, 200), Bounds = new BoundingRectangle(new Vector2(150, 200), 64, 64), Action = "None"},
                    new Keyhole(this, Color.White) {Position = new Vector2(250, 200), Bounds = new BoundingRectangle(new Vector2(250, 200), 64, 64), Action = "None"},
                    new Keyhole(this, Color.White) {Position = new Vector2(350, 200), Bounds = new BoundingRectangle(new Vector2(350, 200), 64, 64), Action = "Play"},
                    new Keyhole(this, Color.White) {Position = new Vector2(450, 200), Bounds = new BoundingRectangle(new Vector2(450, 200), 64, 64), Action = "Options"},
                    new Keyhole(this, Color.White) {Position = new Vector2(550, 200), Bounds = new BoundingRectangle(new Vector2(550, 200), 64, 64), Action = "Exit"}
                },
                inputManager = inputManager,
                key = new Key(this)
            }, null);  
            */
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

            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(50, 1000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(400, 2000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(500, 3000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(600, 4000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(100, 5000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(200, 6000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(300, 7000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(400, 8000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(100, 9000)));
            _coins.Add(new Coin(this, Color.LightBlue, new Vector2(100, 10)));
            foreach (Coin coin in _coins) { coin.LoadContent(); }

            _rocks.Add(new Rock(this, Color.Red, new Vector2(100, 10)));
            foreach(Rock rock in _rocks) { rock.LoadContent(); }

            coinSound = Content.Load<SoundEffect>("Pickup_Coin15");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);

            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 1) 
            { 
                oxygen--;
                timer -= 1;                
            }
            
            foreach(Coin coin in _coins)
            {
                if(CollisionHelper.Collides(coin.Bounds, _player.Bounds))
                {
                    oxygen += 3;
                    coin.Collected = true;
                    coinSound.Play();
                }
            }

            _player.Update(gameTime);
            foreach (Coin coin in _coins) { coin.Update(gameTime); }
            foreach (Rock rock in _rocks) { rock.Update(gameTime); }
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
            _spriteBatch.End();

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_gameFont, "Oxygen:" + oxygen + "%", new Vector2(10, 10), Color.LightBlue);
            _spriteBatch.End();
        }
    }
}
