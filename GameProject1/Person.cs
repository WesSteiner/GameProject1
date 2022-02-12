using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameProject1.StateManagement;
using GameProject1.Collisions;

namespace GameProject1
{
    public class Person
    {
        Texture2D texture;
        Color color;
        Game game;

        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private bool flipped;

        public GameTime GameTime { get; set; }

        public Vector2 position = new Vector2(200, 200);

        public BoundingRectangle Bounds { get; set; }

        public Person(Game game, Color color)
        {
            this.game = game;
            this.color = color;
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("OrangePerson");
        }

        public void Update(GameTime gameTime, List<Coin> coins)
        {
            GameTime = gameTime;            
            Bounds = new BoundingRectangle(new Vector2(position.X, position.Y), 50, 50);

            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
            if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
            if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;

            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -1);
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 1);
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                flipped = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, position, color);
        }
    }
}
