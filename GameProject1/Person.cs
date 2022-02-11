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

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, Position, color);
        }
    }
}
