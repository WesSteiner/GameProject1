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

        private SpriteFont _gameFont;
        private Texture2D _foreground;
        public List<Cube> _cubes = new List<Cube>();
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
            _cubes.Add(new Cube(this));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            base.Update(gameTime);

            _player.Update(gameTime);
            foreach (Cube cube in _cubes) { cube.Update(gameTime); }
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
            _spriteBatch.End();

            _spriteBatch.Begin();
            foreach (Cube cube in _cubes) { cube.Draw(); }
            _spriteBatch.End();
        }
    }
}
