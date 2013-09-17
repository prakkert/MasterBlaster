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
using MasterBlaster.GameScreens;
using MasterBlaster.Services;

#endregion

namespace MasterBlaster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RunGame : Game
    {
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public Dictionary<string, Texture2D> Textures { get; private set; }

        public KeyboardState LastKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }

        public MouseState LastMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public RunGame()
            : base()
        {
            Graphics = new GraphicsDeviceManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.SynchronizeWithVerticalRetrace = true;
            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            IsFixedTimeStep = false;

            GameServices.AddService<GameScreenService>(new GameScreenService());

            Textures = new Dictionary<string, Texture2D>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";

            Textures.Add("Ship", Content.Load<Texture2D>("Ship"));
            Textures.Add("Asteroid", Content.Load<Texture2D>("Asteroid"));
            Textures.Add("Fireball", Content.Load<Texture2D>("Fireball"));

            Textures.Add("MainMenuBackground", Content.Load<Texture2D>("Background"));
            Textures.Add("MainMenuTitle", Content.Load<Texture2D>("Title"));
            Textures.Add("MainMenuNewGameButton", Content.Load<Texture2D>("NewGameButton"));
            Textures.Add("MainMenuCreditsButton", Content.Load<Texture2D>("CreditsButton"));
            Textures.Add("MainMenuExitButton", Content.Load<Texture2D>("ExitGameButton"));

            Textures.Add("Arrow", Content.Load<Texture2D>("Arrow"));

            Texture2D star = new Texture2D(this.GraphicsDevice, 1, 1);
            star.SetData(new Color[] { Color.White });

            Textures.Add("Star", star);

            //GameServices.GetService<GameScreenService>().Push(new SpaceGameScreen("Space", this));
            GameServices.GetService<GameScreenService>().Push(new MainMenuGameScreen(this));
            GameServices.GetService<GameScreenService>().ActiveGameScreen.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            CurrentKeyboardState = Keyboard.GetState();
            CurrentMouseState = Mouse.GetState();

            GameServices.GetService<GameScreenService>().ActiveGameScreen.Update(gameTime);

            LastMouseState = CurrentMouseState;
            LastKeyboardState = CurrentKeyboardState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();

            GameServices.GetService<GameScreenService>().ActiveGameScreen.Draw(SpriteBatch);
      
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
