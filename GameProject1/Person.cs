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

        public GameTime GameTime { get; set; }

        public Vector2 Position { get; set; }

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

        public void Update(GameTime gameTime, List<Coin> coins, InputManager inputManager)
        {
            inputManager.Update(gameTime);
            GameTime = gameTime;
            Position = inputManager.Direction;
            Bounds = new BoundingRectangle(new Vector2(inputManager.Direction.X, inputManager.Direction.Y), 50, 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, Position, color);
        }
    }
}
