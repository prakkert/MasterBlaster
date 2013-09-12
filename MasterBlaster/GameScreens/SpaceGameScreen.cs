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
        Ship ship;
        Fireball fireball;
        List<Asteroid> asteroids;

        SpriteFont defaultFont;

        TimeSpan levelTime;

        bool pause = false;
        int fps = 0;
        int points = 0;

        private List<Vector2> starPoints;

        public SpaceGameScreen(string name, RunGame game)
            : base(name, game)
        {
        }

        public override void Initialize()
        {
            Game.IsMouseVisible = false;

            ship = new Ship(Game.Textures["Ship"], new Vector2((int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2), (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2)));

            asteroids = new List<Asteroid>();

            for (int i = 0; i < 5; i++)
            {
                asteroids.Add(new Asteroid(Game.Textures["Asteroid"]));
            }

            defaultFont = Game.Content.Load<SpriteFont>("DefaultFont");

            fireball = null;

            starPoints = new List<Vector2>();

            points = 0;

            for (int i = 0; i < 1000; i++)
            {
                starPoints.Add(new Vector2(RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 1), RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 1)));
            }

            levelTime = new TimeSpan();
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();

            if (!pause && levelTime.TotalSeconds > 3)
            {

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

                if (Game.CurrentKeyboardState.IsKeyDown(Keys.LeftControl) && fireball == null)
                {
                    fireball = new Fireball(Game.Textures["Fireball"], ship.Position, ship.Direction, ship.Rotation);
                }

                ship.Update(gameTime);

                if (fireball != null)
                {
                    fireball.Update(gameTime);

                    if (fireball.Destroyed)
                    {
                        fireball = null;
                    }
                }


                asteroids = asteroids.Where(ast => ast.Destroyed == false).ToList();

                foreach (Asteroid asteroid in asteroids)
                {
                    asteroid.Update(gameTime);

                    if (fireball != null && asteroid.Boundaries.Intersects(fireball.Boundaries))
                    {
                        asteroid.Destroy();
                        fireball.Destroy();
                        points++;
                    }
                    if (asteroid.Boundaries.Intersects(ship.Boundaries))
                    {
                        Game.ResetElapsedTime();
                        Initialize();

                        return;
                    }
                }

                while (asteroids.Count < 5)
                {
                    asteroids.Add(new Asteroid(Game.Textures["Asteroid"]));
                }
            }

            fps = (int)(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);

            levelTime += gameTime.ElapsedGameTime;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Vector2 starPoint in starPoints)
            {
                spriteBatch.Draw(Game.Textures["Star"], starPoint, Color.White);
            }

            spriteBatch.DrawString(defaultFont, "Points: " + points, new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(defaultFont, "Speed: " + Math.Round(ship.Speed, 1), new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(defaultFont, "FPS: " + fps, new Vector2(10, 50), Color.Red);
            spriteBatch.DrawString(defaultFont, "Memory: " + Math.Round((double)GC.GetTotalMemory(true) / 1024 / 1024,1) + " mB", new Vector2(10, 70), Color.Red);

            spriteBatch.Draw(ship.Texture, ship.Position, null, Color.White, ship.Rotation, new Vector2(50, 50), 1.0f, SpriteEffects.None, 0f);

            foreach (Asteroid asteroid in asteroids)
            {
                spriteBatch.Draw(asteroid.Texture, asteroid.Position, null, Color.White, asteroid.Rotation, new Vector2(25, 25), asteroid.Size, SpriteEffects.None, 0f);
            }

            if (fireball != null)
            {
                spriteBatch.Draw(fireball.Texture, fireball.Position, null, Color.White, fireball.Rotation, new Vector2(25, 12), 1.0f, SpriteEffects.None, 0f);
            }
        }
    }
}