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
        Game game;

        private Color color;
        public Color Color { get; set; }

        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private bool flipped;

        public GameTime GameTime { get; set; }

        public Vector2 position = new Vector2(330, 300);

        public Rectangle personBounds = new Rectangle(0, 0, 64, 64);

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(330 - 55, 300 - 55), 50, 50);
        public BoundingRectangle Bounds => bounds;

        public Person(Game game, Color color)
        {
            this.game = game;
            this.color = color;
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("OrangePerson");
        }

        public void Update(GameTime gameTime)
        {
            GameTime = gameTime;            

            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
            if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
            if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;

            // Apply keyboard movement
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.A)) 
            {
                position -= Vector2.UnitX * 100 * t;
                flipped = true;
            } 
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position += Vector2.UnitX * 100 * t;
                flipped = false;
            }
            if (keyboardState.IsKeyDown(Keys.W)) position -= Vector2.UnitY * 200 * t;
            if (keyboardState.IsKeyDown(Keys.S)) position += Vector2.UnitY * 200 * t;

            bounds.X = position.X - 55;
            bounds.Y = position.Y - 55;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            SpriteEffects spriteEffects = (!flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, position, personBounds, color, 0, new Vector2(64, 64), 1, spriteEffects, 0);
        }
    }
}
