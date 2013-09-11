#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace MasterBlaster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Ship ship;
        Asteroid asteroid;

        SpriteFont defaultFont;

        Texture2D star;

        KeyboardState lastKeyboardState;

        bool pause = false;
        int fps = 0;
        int points = 0;

        private List<Vector2> starPoints;

        public Game1()
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship = new Ship(Content.Load<Texture2D>("Ship"), new Vector2((int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2), (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2)));
            asteroid = new Asteroid(Content.Load<Texture2D>("Asteroid"));


            defaultFont = Content.Load<SpriteFont>("DefaultFont");

            star = new Texture2D(this.GraphicsDevice, 1, 1);
            star.SetData(new Color[] { Color.White });

            starPoints = new List<Vector2>();

            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                starPoints.Add(new Vector2(r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 1), r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 1)));
            }
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

                asteroid.Update(gameTime);
                ship.Update(gameTime);

                if (asteroid.Boundaries.Intersects(ship.Boundaries))
                {
                    asteroid = new Asteroid(asteroid.Texture);
                    points++;
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
                spriteBatch.Draw(star, starPoint, Color.White);
            }

            spriteBatch.DrawString(defaultFont, "Points: " + points, new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(defaultFont, "Speed: " + Math.Round(ship.Speed,1), new Vector2(10, 30), Color.Red);
            spriteBatch.DrawString(defaultFont, "FPS: " + fps, new Vector2(10, 50), Color.Red);

            spriteBatch.Draw(ship.Texture, ship.Position, null, Color.White, ship.Rotation, new Vector2(50,50), 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(asteroid.Texture, asteroid.Position, null, Color.White, asteroid.Rotation, new Vector2(25, 25), 1.0f, SpriteEffects.None, 0f);

      
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
