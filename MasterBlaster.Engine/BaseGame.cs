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
using Microsoft.Xna.Framework.Audio;
using MasterBlaster.Engine.Components;

#endregion

namespace MasterBlaster.Engine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public abstract class BaseGame : Game, IHasComponentStore
    {
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        public GameScreenService GameScreenService { get; protected set; }

        public new ComponentStore Components { get; private set; }

        public BaseGame()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Components = new ComponentStore();
            GameScreenService = new GameScreenService();

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
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            base.Initialize();
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var updatableComponents = Components.GetAllOfType<IUpdatableComponent>();

            foreach (var updatableComponent in updatableComponents)
            {
                updatableComponent.Update(gameTime);
            }

            GameScreenService.ActiveGameScreen.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();

            _graphics.GraphicsDevice.Clear(Color.Black);

          //  _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());

           // List<IDrawableComponent> drawableComponents = Components.GetAllOfType<IDrawableComponent>();

           // foreach (var drawableComponent in drawableComponents)
           // {
           //     drawableComponent.Draw(_spriteBatch);
           // }

            GameScreenService.ActiveGameScreen.Draw(_spriteBatch);
          //  _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}