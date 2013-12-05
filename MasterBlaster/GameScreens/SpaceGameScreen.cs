using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
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

        int fps = 0;

        private List<Vector2> starPoints;

        public SpaceGameScreen(string name, BaseGame game)
            : base(name, game)
        {
            Game.IsMouseVisible = false;

            Components.Add<CollisionService>(new CollisionService(this.Components));
            Components.Add<ScoreService>(new ScoreService());
            Components.Add<KeyboardService>(new KeyboardService());
            Components.Add<MovementService>(new MovementService(Components));

            Reset();
        }

        private void Reset()
        {
            Game.ResetElapsedTime();

            Components.RemoveAll<Ship>();
            Components.RemoveAll<Asteroid>();
            Components.RemoveAll<Fireball>();

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

            Components.GetSingle<ScoreService>().ResetScore();
        }

        public override void Activate()
        {

        }

        public override void Deactivate()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var keyboardService = Components.GetSingle<KeyboardService>();

            if (keyboardService.IsKeyPressed(Keys.Escape))
            {
                Game.GameScreenService.Pop();
            }


            Ship ship = Components.GetSingle<Ship>();

            if (keyboardService.IsKeyUp(Keys.Left) && keyboardService.IsKeyDown(Keys.Right))
            {
                ship.Right();
            }

            else if (keyboardService.IsKeyDown(Keys.Left) && keyboardService.IsKeyUp(Keys.Right))
            {
                ship.Left();
            }

            if (keyboardService.IsKeyDown(Keys.Up) && keyboardService.IsKeyUp(Keys.Down))
            {
                ship.Accelerate();
            }

            else if (keyboardService.IsKeyUp(Keys.Up) && keyboardService.IsKeyDown(Keys.Down))
            {
                ship.Decelerate();
            }

            Fireball fireball = Components.GetAllOfType<Fireball>().FirstOrDefault();

            if (keyboardService.IsKeyPressed(Keys.LeftControl) && fireball == null)
            {
                Components.Add(new Fireball(Game.Textures["Fireball"], ship.Position, ship.Direction, ship.Rotation));
                fireball = Components.GetAllOfType<Fireball>().First();
            }


            ship.Update(gameTime);

            if (fireball != null)
            {
                //    fireball.Update(gameTime);

                if (fireball.Destroyed)
                {
                    Components.Remove<Fireball>(fireball);
                }
            }

            var asteroids = Components.GetAllOfType<Asteroid>();

            foreach (Asteroid asteroid in asteroids)
            {
                //    asteroid.Update(gameTime);
            }

            while (Components.GetAllOfType<Asteroid>().Count < 5)
            {
                Components.Add(new Asteroid(Game.Textures["Asteroid"]));
            }

            if (ship.Destroyed)
            {
                Reset();
            }

            fps = (int)(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);

            levelTime += gameTime.ElapsedGameTime;

            //   base.Update(gameTime);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());

            base.Draw(spriteBatch);

            var collidableComponents = Components.GetAllOfType<ICollidableComponent>();

            foreach (var component in collidableComponents)
            {
                if (component is ICollidableComponent)
                {
                    DrawBorder(spriteBatch, Game.Textures["Star"], component.CollisionBoundaries, 1, Color.Red);
                }

            }

            foreach (Vector2 starPoint in starPoints)
            {
                spriteBatch.Draw(Game.Textures["Star"], starPoint, Color.White);
            }



            spriteBatch.DrawString(defaultFont, "Points: " + Components.GetSingle<ScoreService>().Points, new Vector2(10, 10), Color.Red);
            //  spriteBatch.DrawString(defaultFont, "Speed: " + Math.Round(GetComponentsOfType<Ship>().First().Speed, 1), new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(defaultFont, "FPS: " + fps, new Vector2(10, 50), Color.Red);
            spriteBatch.DrawString(defaultFont, "Memory: " + Math.Round((double)GC.GetTotalMemory(true) / 1024 / 1024, 1) + " mB", new Vector2(10, 70), Color.Red);

            spriteBatch.End();
        }
    }
}