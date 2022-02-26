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
    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }

    public class Coin
    {
        private Texture2D texture;
        private double directionTimer;
        private Color color;
        private Game game;
        private SoundEffect coin;

        public Direction Direction;

        public Vector2 Position { get; set; }

        public BoundingCircle Bounds { get; set; }

        public bool Collected { get; set; } = false;

        public Coin(Game game, Color color, Vector2 position)
        {
            this.game = game;
            this.color = color;

            Position = position;
            Bounds = new BoundingCircle(Position + new Vector2(8, 8), 8);
        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("Coin");
            coin = game.Content.Load<SoundEffect>("Pickup_Coin15");
        }

        public void Update(GameTime gameTime)
        {
            //Update direction timer
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (Collected)
            {
                coin.Play();
                Collected = false;
            }

            //switch direction every 2 secs
            if (directionTimer > 2.0)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        Direction = Direction.Down;
                        break;
                    case Direction.Down:
                        Direction = Direction.Right;
                        break;
                    case Direction.Right:
                        Direction = Direction.Left;
                        break;
                    case Direction.Left:
                        Direction = Direction.Up;
                        break;
                }
                directionTimer -= 2.0;
            }

            switch (Direction)
            {
                case Direction.Up:
                    Position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Down:
                    Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Left:
                    Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.Right:
                    Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
