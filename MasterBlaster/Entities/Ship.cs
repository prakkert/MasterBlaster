
using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Entities
{
    public class Ship : BaseComponent, IEntityComponent, ICollidableComponent, IDrawableComponent, IMovableComponent
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float MaxSpeed { get; set; }
        public float Rotation { get; set; }

        public bool Destroyed { get; set; }

        public Rectangle CollisionBoundaries { get; set; }

        public Ship(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Speed = 0;
            Direction = new Vector2(0, 0);
            Rotation = (MathHelper.Pi / 2);
            MaxSpeed = 10;

            CollisionBoundaries = new Rectangle((int)(Position.X - Texture.Width / 2), (int)(Position.Y - Texture.Height / 2), Texture.Width, Texture.Height);
        }

        public void CollidedWith(ICollidableComponent component)
        {
            if (component is Asteroid)
            {
                Destroyed = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(50, 50), 1.0f, SpriteEffects.None, 0f);
        }


        public void Move(GameTime gameTime)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

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
            CollisionBoundaries = new Rectangle((int)(Position.X - Texture.Width / 2), (int)(Position.Y - Texture.Height / 2), Texture.Width, Texture.Height);
        }

        public override void Destroy()
        {
            Destroyed = true;
        }
    }
}