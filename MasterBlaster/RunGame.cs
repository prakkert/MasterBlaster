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
using Microsoft.Xna.Framework.Audio;
using MasterBlaster.Engine;
using MasterBlaster.Components;

#endregion

namespace MasterBlaster
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RunGame : BaseGame
    {
        public MouseState LastMouseState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }

        public RunGame()
            : base()
        {
                     

            Resolution.Init(ref _graphics);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            IsFixedTimeStep = true;

            AddGameServices();

            Resolution.SetVirtualResolution(1920, 1080);
            Resolution.SetResolution(1920, 1080, false);

            base.Initialize();
        }

        private void AddGameServices()
        {
            ComponentStore.Add<CollisionService>(new CollisionService());
            // ComponentStore.Add<GameScreenService>(new GameScreenService());
            ComponentStore.Add<ScoreService>(new ScoreService());
            ComponentStore.Add<SoundService>(new SoundService());
            ComponentStore.Add<KeyboardService>(new KeyboardService());
            ComponentStore.Add<MovementService>(new MovementService());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";

            LoadTextures();
            LoadSounds();

            GameScreenService.Push(new MainMenuGameScreen(this));
        }

        private void LoadSounds()
        {
            SoundEffects.Add("Explosion", Content.Load<SoundEffect>(@"Sounds\Explosion"));

            ComponentStore.GetSingle<SoundService>().LoadContent(SoundEffects);
        }

        private void LoadTextures()
        {
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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());

            GameScreenService.ActiveGameScreen.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}
