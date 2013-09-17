using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Entities
{
    public class Fireball
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float MaxDistance { get; set; }
        public float Rotation { get; set; }

        public Rectangle Boundaries { get; set; }

        public bool Destroyed = false;

        public Fireball(Texture2D texture, Vector2 position, Vector2 direction, float rotation)
        {
            Texture = texture;
            Position = position;
            Speed = 25;
            Direction = direction;
            Rotation = rotation;
            MaxDistance = 750;

            Boundaries = new Rectangle((int)(Position.X - Texture.Width / 2), (int)(Position.Y - Texture.Height / 2), Texture.Width, Texture.Height);

            if (direction.Equals(new Vector2(0, 0)))
            {
                Destroy();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!Destroyed)
            {
                if (MaxDistance < 0)
                {
                    Destroy();
                    return;
                }

                MaxDistance -= Direction.Length() * (int)(Speed * (gameTime.ElapsedGameTime.TotalMilliseconds / 10));

                Position += Direction * (int)(Speed * (gameTime.ElapsedGameTime.TotalMilliseconds / 10));

                if (Position.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                {
                    Position = new Vector2(0, Position.Y);
                }
                else if (Position.X < 0)
                {
                    Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, Position.Y);
                }
                if (Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                {
                    Position = new Vector2(Position.X, 0);
                }
                else if (Position.Y < 0)
                {
                    Position = new Vector2(Position.X, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
                }

                Boundaries = new Rectangle((int)(Position.X - Texture.Width / 2), (int)(Position.Y - Texture.Height / 2), (int)(Texture.Width), (int)(Texture.Height));
            }

        }

        internal void Destroy()
        {
            Destroyed = true;
        }
    }
}
