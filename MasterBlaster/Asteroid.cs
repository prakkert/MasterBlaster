
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster
{
    public class Asteroid
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float Rotation { get; set; }
        public float RotationSpeed { get; set; }

        public Rectangle Boundaries { get; set; }

        public Asteroid(Texture2D texture)
        {
            Random r = new Random();

            Texture = texture;
            Rotation = 0;
            Direction = new Vector2((float)Math.Cos(r.Next(100,100)/0.01f), (float)Math.Sin(r.Next(-100,100)/0.01f));

            Position = new Vector2(r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
            
            Speed = r.Next(10, 1000)*0.01f;
            RotationSpeed = r.Next(-100, 100) * 0.002f;

            Boundaries = new Rectangle((int)(Position.X - Texture.Width / 2), (int)(Position.Y - Texture.Height / 2), Texture.Width, Texture.Height);
        }

        public void Update(GameTime gameTime)
        {

            Position += Direction * (int)(Speed * (gameTime.ElapsedGameTime.TotalMilliseconds/10));

            Rotation += RotationSpeed;


            if (Position.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
            {
                Position = new Vector2(0, Position.Y);
            }
            else if (Position.X< 0)
            {
                Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, Position.Y);
            }
            if (Position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            {
                Position = new Vector2(Position.X,0);
            }
            else if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            }

            Boundaries = new Rectangle((int)(Position.X - Texture.Width / 2), (int)(Position.Y - Texture.Height / 2), Texture.Width, Texture.Height);
            
        }
    }
}
