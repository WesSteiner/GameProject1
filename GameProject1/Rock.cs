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
    public class Rock
    {
        private Texture2D texture;
        private double directionTimer;
        private Color color;
        private Game game;
        private Random rand;
        double seconds;

        public Direction Direction;
        public int Speed;
        public Vector2 Position;

        public BoundingCircle Bounds { get; set; }

        public bool Collected { get; set; } = false;

        public Rock(Game game, Color color, Random rand)
        {
            this.game = game;
            this.color = color;
            this.rand = rand;
            
            Bounds = new BoundingCircle(Position + new Vector2(8, 8), 8);
            
            Speed = rand.Next(200, 800);
            Position.X = rand.Next(50, 750);
            Position.Y = rand.Next(500, 9500);
            seconds = rand.Next(2, 5);

            if (Position.X > 375) Direction = Direction.Left;
            else Direction = Direction.Right;
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

            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;
            
            if (directionTimer > seconds)
            {
                switch (Direction)
                {                                        
                    case Direction.Right:
                        Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        Direction = Direction.Right;
                        break;
                }
                directionTimer -= seconds;
            }
            
            switch (Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
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
