using MasterBlaster.Components;
using MasterBlaster.Entities;
using MasterBlaster.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.GameScreens
{
    public class SpaceGameScreen : BaseGameScreen
    {
        SpriteFont defaultFont;

        TimeSpan levelTime;

        bool pause = false;
        int fps = 0;

        private List<Vector2> starPoints;

        public SpaceGameScreen(string name, RunGame game)
            : base(name, game)
        {
            Game.IsMouseVisible = false;

            Reset();
        }

        private void Reset()
        {
            Game.ResetElapsedTime();

            Components.Clear();

            Components.Add(new Ship(Game.Textures["Ship"], new Vector2((int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2), (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2))));

            for (int i = 0; i < 5; i++)
            {
                Components.Add(new Asteroid(Game.Textures["Asteroid"]));
            }

            defaultFont = Game.Content.Load<SpriteFont>("DefaultFont");


            starPoints = new List<Vector2>();

            for (int i = 0; i < 1000; i++)
            {
                starPoints.Add(new Vector2(RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 1), RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 1)));
            }

            levelTime = new TimeSpan();

            GameServices.GetService<ScoreService>().ResetScore();
        }

        public override void Activate()
        {

        }

        public override void Deactivate()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Game.CurrentKeyboardState.IsKeyDown(Keys.F12) && (Game.LastKeyboardState.IsKeyUp(Keys.F12)))
            {
                pause = !pause;
            }

            if (!pause && levelTime.TotalSeconds > 3)
            {

                List<ICollidableComponent> collidableComponents = GetComponentsOfType<ICollidableComponent>();
                GameServices.GetService<CollisionService>().CheckForCollisions(collidableComponents);

                Ship ship = GetComponentsOfType<Ship>().First();

                if (Game.CurrentKeyboardState.IsKeyUp(Keys.Left) && Game.CurrentKeyboardState.IsKeyDown(Keys.Right))
                {
                    ship.Right();
                }

                else if (Game.CurrentKeyboardState.IsKeyDown(Keys.Left) && Game.CurrentKeyboardState.IsKeyUp(Keys.Right))
                {
                    ship.Left();
                }

                if (Game.CurrentKeyboardState.IsKeyDown(Keys.Up) && Game.CurrentKeyboardState.IsKeyUp(Keys.Down))
                {
                    ship.Accelerate();
                }

                else if (Game.CurrentKeyboardState.IsKeyUp(Keys.Up) && Game.CurrentKeyboardState.IsKeyDown(Keys.Down))
                {
                    ship.Decelerate();
                }

                Fireball fireball = GetComponentsOfType<Fireball>().FirstOrDefault();

                if (Game.CurrentKeyboardState.IsKeyDown(Keys.LeftControl) && fireball == null)
                {
                    Components.Add(new Fireball(Game.Textures["Fireball"], ship.Position, ship.Direction, ship.Rotation));
                    fireball = GetComponentsOfType<Fireball>().First();
                }


                ship.Update(gameTime);

                if (fireball != null)
                {
                    fireball.Update(gameTime);

                    if (fireball.Destroyed)
                    {
                        Components.Remove(fireball);
                    }
                }

                var asteroids = GetComponentsOfType<Asteroid>();

                foreach (Asteroid asteroid in asteroids)
                {
                    asteroid.Update(gameTime);
                }

                while (GetComponentsOfType<Asteroid>().Count < 5)
                {
                    Components.Add(new Asteroid(Game.Textures["Asteroid"]));
                }

                if (ship.Destroyed)
                {
                    Reset();
                }
            }
            fps = (int)(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);

            levelTime += gameTime.ElapsedGameTime;

            base.Update(gameTime);
        }

        
    
        public override void Draw(SpriteBatch spriteBatch)
        {
            var drawableComponents = GetComponentsOfType<IDrawableComponent>();

            foreach (var component in drawableComponents)
            {
                component.Draw(spriteBatch);

                if (component is ICollidableComponent)
                {
                    var collidableComponent = (ICollidableComponent)component;
                    DrawBorder(spriteBatch, Game.Textures["Star"], collidableComponent.CollisionBoundaries, 1, Color.Red);

                }
                          
            }
             
            foreach (Vector2 starPoint in starPoints)
            {
                spriteBatch.Draw(Game.Textures["Star"], starPoint, Color.White);
            }



            spriteBatch.DrawString(defaultFont, "Points: " + GameServices.GetService<ScoreService>().Points, new Vector2(10, 10), Color.Red);
          //  spriteBatch.DrawString(defaultFont, "Speed: " + Math.Round(GetComponentsOfType<Ship>().First().Speed, 1), new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(defaultFont, "FPS: " + fps, new Vector2(10, 50), Color.Red);
            spriteBatch.DrawString(defaultFont, "Memory: " + Math.Round((double)GC.GetTotalMemory(true) / 1024 / 1024,1) + " mB", new Vector2(10, 70), Color.Red);

        }
    }
}