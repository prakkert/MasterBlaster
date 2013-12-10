using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using MasterBlaster.Entities;
using MasterBlaster.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

            Components.Add<CollisionService>(new CollisionService(Components));
            Components.Add<ScoreService>(new ScoreService());
            Components.Add<SoundService>(new SoundService());
            Components.Add<KeyboardService>(new KeyboardService());
            Components.Add<MovementService>(new MovementService(Components));
            Components.Add<AsteroidService>(new AsteroidService(Components));
            Components.Add<ShipService>(new ShipService(Components, this));

            var keyboardService = Components.GetSingle<KeyboardService>();

            keyboardService.RegisterKeyListener(new KeyboardService.KeyCombination(Keys.Escape, KeyboardService.KeyEventType.Pressed), ToMainMenu);

            LoadTextures();
            LoadSoundEffects();

            Reset();
        }

        private void Reset()
        {
            Game.ResetElapsedTime();

            Components.RemoveAll<Ship>();
            Components.RemoveAll<Asteroid>();
            Components.RemoveAll<Fireball>();

            Components.Add(new Ship(Textures["Ship"], new Vector2((int)(Resolution.ViewportWidth / 2), (int)(Resolution.ViewportHeight / 2))));

            for (int i = 0; i < 5; i++)
            {
                Components.Add(new Asteroid(Textures["Asteroid"], Asteroid.AsteroidSize.Large, Vector2.Zero));
            }

            defaultFont = Game.Content.Load<SpriteFont>("DefaultFont");


            starPoints = new List<Vector2>();

            for (int i = 0; i < 1000; i++)
            {
                starPoints.Add(new Vector2(RandomGenerator.Get.Next(0, Resolution.ViewportWidth - 1), RandomGenerator.Get.Next(0, Resolution.ViewportHeight - 1)));
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
        public override void LoadSoundEffects()
        {
            SoundEffects.Add("Explosion", Game.Content.Load<SoundEffect>(@"Sounds\Explosion"));

            Components.GetSingle<SoundService>().LoadContent(SoundEffects);
        }
        public override void LoadTextures()
        {
            Textures.Add("Ship", Game.Content.Load<Texture2D>("Ship"));
            Textures.Add("Asteroid", Game.Content.Load<Texture2D>("Asteroid"));
            Textures.Add("Fireball", Game.Content.Load<Texture2D>("Fireball"));

            Texture2D star = new Texture2D(Game.GraphicsDevice, 1, 1);
            star.SetData(new Color[] { Color.White });

            Textures.Add("Star", star);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Ship ship = Components.GetSingle<Ship>();

            if (ship.Destroyed)
            {
                Reset();
            }

            fps = (int)(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);

            levelTime += gameTime.ElapsedGameTime;

            //   base.Update(gameTime);
        }

        public void ToMainMenu()
        {
            Game.GameScreenService.Pop();
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
               //     DrawBorder(spriteBatch, Textures["Star"], component.CollisionBoundaries, 1, Color.Red);
                }

            }

            foreach (Vector2 starPoint in starPoints)
            {
                spriteBatch.Draw(Textures["Star"], starPoint, Color.White);
            }



            spriteBatch.DrawString(defaultFont, "Points: " + Components.GetSingle<ScoreService>().Points, new Vector2(10, 10), Color.Red);
            //  spriteBatch.DrawString(defaultFont, "Speed: " + Math.Round(GetComponentsOfType<Ship>().First().Speed, 1), new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(defaultFont, "FPS: " + fps, new Vector2(10, 50), Color.Red);
            spriteBatch.DrawString(defaultFont, "Memory: " + Math.Round((double)GC.GetTotalMemory(true) / 1024 / 1024, 1) + " mB", new Vector2(10, 70), Color.Red);

            spriteBatch.End();
        }
    }
}