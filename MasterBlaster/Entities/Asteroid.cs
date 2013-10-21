
using MasterBlaster.Components;
using MasterBlaster.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Entities
{
    public class Asteroid : BaseComponent, ICollidableComponent, IDrawableComponent, IControllableComponent
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float Rotation { get; set; }
        public float RotationSpeed { get; set; }

        public float Size { get; set; }

        public bool Destroyed { get; set; }

        public Rectangle CollisionBoundaries { get; set; }

        public Asteroid(Texture2D texture)
        {
            Texture = texture;
            Rotation = 0;
            Direction = new Vector2((float)Math.Cos(RandomGenerator.Get.Next(100, 100) / 0.01f), (float)Math.Sin(RandomGenerator.Get.Next(-100, 100) / 0.01f));

            Position = new Vector2(RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));

            Speed = RandomGenerator.Get.Next(10, 1000) * 0.01f;
            RotationSpeed = RandomGenerator.Get.Next(-100, 100) * 0.002f;

            Size = RandomGenerator.Get.Next(10, 25) / 10;
        }

        public void Update(GameTime gameTime)
        {
            if (!Destroyed)
            {
                Position += Direction * (int)(Speed * (gameTime.ElapsedGameTime.TotalMilliseconds / 10));

                Rotation += RotationSpeed;


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

                CollisionBoundaries = new Rectangle((int)(Position.X - Texture.Width * Size / 2), (int)(Position.Y - Texture.Height * Size / 2), (int)(Texture.Width * Size), (int)(Texture.Height * Size));
            }
        }

        public void CollidedWith(ICollidableComponent component)
        {
            if (component is Fireball)
            {
                GameServices.GetService<ScoreService>().AddScore();
                GameServices.GetService<SoundService>().PlayExplosion();
                Destroyed = true;
            }

            else if (component is Asteroid)
            {
              
            }

            else if (component is Ship)
            {
                Destroyed = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, new Vector2(25, 25), Size, SpriteEffects.None, 0f);
        }
    }
}
