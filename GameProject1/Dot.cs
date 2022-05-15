using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GameProject1.StateManagement;
using GameProject1.Collisions;
using Microsoft.Xna.Framework.Audio;

namespace GameProject1
{
    public class Dot
    {
        private Texture2D texture;
        private double directionTimer;
        private Color color;
        private Game game;
        private Random rand;
        double seconds;

        public Direction Direction;
        public int Speed;
        public Vector2 Position { get; set; }

        public BoundingCircle Bounds { get; set; }

        public bool Collected { get; set; } = false;

        public Dot(Game game, Color color, Vector2 position)
        {
            this.game = game;
            this.color = color;
            this.Position = position;

            Bounds = new BoundingCircle(Position + new Vector2(8, 8), 8);
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("Rock");
        }

        public void Update(GameTime gameTime)
        {
            if (Collected)
            {
                Position = new Vector2(0, 0);
            }            
            Bounds = new BoundingCircle(Position + new Vector2(8, 8), 8);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            if (Collected) return;
            spriteBatch.Draw(texture, Position, color);
        }
    }
}
