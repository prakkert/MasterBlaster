
using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using MasterBlaster.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Entities
{
    public class Asteroid : BaseComponent, IEntityComponent, ICollidableComponent, IDrawableComponent, IMovableComponent
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }
        public float Rotation { get; set; }
        public float RotationSpeed { get; set; }

        public Vector2 Center { get { return new Vector2(Position.X + (int)(Texture.Bounds.Center.X * Size), Position.Y + (int)(Texture.Bounds.Center.Y * Size)); } }

        public float Size { get; set; }

        public Rectangle CollisionBoundaries { get; set; }

        public double Mass { get; set; }

        public Asteroid(Texture2D texture, AsteroidSize size, Vector2 position)
        {
            Texture = texture;
            Rotation = 0;
            Direction = new Vector2((float)Math.Cos(RandomGenerator.Get.Next(-100, 100) / 0.01f), (float)Math.Sin(RandomGenerator.Get.Next(-100, 100) / 0.01f));

            if (position == Vector2.Zero)
            {
                Position = new Vector2(RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
            }
            else
            {
                Position = position;
            }

      

            switch (size)
            {
                case AsteroidSize.Small:
                    {
                        Size = 1;
                        break;
                    }
                case AsteroidSize.Medium:
                    {
                        Size = 3;
                        break;
                    }
                case AsteroidSize.Large:
                    {
                        Size = 5;
                        break;
                    }
            }

            Speed = (RandomGenerator.Get.Next(10, 1000) * 0.01f) / Size;
            RotationSpeed = RandomGenerator.Get.Next(-100, 100) * 0.002f / Size;

            CalculateMass();
        }

        public void CalculateMass()
        {
            Mass = (4 / 3) * Math.PI * Math.Pow(Size / 2, 3);
        }

        public void CollidedWith(ICollidableComponent component)
        {
            if (component is Fireball)
            {
                //GetService<ScoreService>().AddScore();
                //GameServices.GetService<SoundService>().PlayExplosion();
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


        public void Move(GameTime gameTime)
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

        public override void Destroy()
        {
            Destroyed = true;
        }

        public enum AsteroidSize
        {
            Small, 
            Medium, 
            Large
        }
    }
}
