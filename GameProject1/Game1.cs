using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameProject1.Screens;
using GameProject1.StateManagement;
using GameProject1.Collisions;

namespace GameProject1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenManager _screenManager;
        private InputManager inputManager;

        private SoundEffect coin;
        private Song backgroundMusic;

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

            AdditionalScreens();
        }

        private void AdditionalScreens()
        {
            inputManager = new InputManager();
            System.Random rand = new System.Random();

            _screenManager.AddScreen(new ColorRunScreen(this)
            {
                inputManager = inputManager,
                _player = new Person(this, Color.White)
            },            
            null);

            /*
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
            */

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
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            inputManager = new InputManager();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundMusic = Content.Load<Song>("8-bit-beebop");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
