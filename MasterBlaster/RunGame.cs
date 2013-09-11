#region Using Statements
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
#endregion

namespace MasterBlaster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RunGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Dictionary<string, Texture2D> Textures { get; private set; }

        Ship ship;
        Fireball fireball;
        List<Asteroid> asteroids;

        SpriteFont defaultFont;

        KeyboardState lastKeyboardState;

        bool pause = false;
        int fps = 0;
        int points = 0;

        private List<Vector2> starPoints;

        public RunGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.IsFullScreen = true;
            IsMouseVisible = false;

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            graphics.ApplyChanges();

            lastKeyboardState = Keyboard.GetState();
            base.Initialize();

            ship = new Ship(Textures["Ship"], new Vector2((int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2), (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2)));

            asteroids = new List<Asteroid>();

            for (int i = 0; i < 5; i++)
            {
                asteroids.Add(new Asteroid(Textures["Asteroid"]));
            }

            defaultFont = Content.Load<SpriteFont>("DefaultFont");

            fireball = null;

            starPoints = new List<Vector2>();

            points = 0;

            for (int i = 0; i < 1000; i++)
            {
                starPoints.Add(new Vector2(RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 1), RandomGenerator.Get.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 1)));
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Textures = new Dictionary<string, Texture2D>();

            Textures.Add("Ship", Content.Load<Texture2D>("Ship"));
            Textures.Add("Asteroid", Content.Load<Texture2D>("Asteroid"));
            Textures.Add("Fireball", Content.Load<Texture2D>("Fireball"));

            Texture2D star = new Texture2D(this.GraphicsDevice, 1, 1);
            star.SetData(new Color[] { Color.White });

            Textures.Add("Star", star);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.F12) && (lastKeyboardState.IsKeyUp(Keys.F12)))
            {
                pause = !pause;
            }

           

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!pause)
            {

                if (keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyDown(Keys.Right))
                {
                    ship.Right();
                }

                else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
                {
                    ship.Left();
                }

                if (keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.Down))
                {
                    ship.Accelerate();
                }

                else if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyDown(Keys.Down))
                {
                    ship.Decelerate();
                }

                if (keyboardState.IsKeyDown(Keys.LeftControl) && fireball == null)
                {
                    fireball = new Fireball(Textures["Fireball"], ship.Position, ship.Direction, ship.Rotation);
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
                        ResetElapsedTime();
                        Initialize();

                        return;
                    }
                }

             

                while (asteroids.Count < 3)
                {
                    asteroids.Add(new Asteroid(Textures["Asteroid"]));
                }
            }

            fps = (int)(1000 / gameTime.ElapsedGameTime.TotalMilliseconds);

            lastKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (Vector2 starPoint in starPoints)
            {
                spriteBatch.Draw(Textures["Star"], starPoint, Color.White);
            }

            spriteBatch.DrawString(defaultFont, "Points: " + points, new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(defaultFont, "Speed: " + Math.Round(ship.Speed,1), new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(defaultFont, "FPS: " + fps, new Vector2(10, 50), Color.Red);

            spriteBatch.Draw(ship.Texture, ship.Position, null, Color.White, ship.Rotation, new Vector2(50,50), 1.0f, SpriteEffects.None, 0f);

            foreach (Asteroid asteroid in asteroids)
            {
                spriteBatch.Draw(asteroid.Texture, asteroid.Position, null, Color.White, asteroid.Rotation, new Vector2(25, 25), asteroid.Size, SpriteEffects.None, 0f);
            }

            if (fireball != null)
            {
                spriteBatch.Draw(fireball.Texture, fireball.Position, null, Color.White, fireball.Rotation, new Vector2(25, 12), 1.0f, SpriteEffects.None, 0f);
            }
      
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
