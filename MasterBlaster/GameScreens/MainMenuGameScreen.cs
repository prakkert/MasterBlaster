using MasterBlaster.Engine;
using MasterBlaster.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBlaster.GameScreens
{
    public class MainMenuGameScreen : BaseGameScreen
    {
        public SelectedMenuItem Selected { get; private set; }

        public Vector2 ArrowPosition { get; private set; }

        private readonly Vector2 _newGameArrowPosition = new Vector2(500, 474);
        private readonly Vector2 _creditsArrowPosition = new Vector2(500, 590);
        private readonly Vector2 _exitArrowPosition = new Vector2(500, 697);

        public MainMenuGameScreen(RunGame game) : base("MainMenu", game)
        {
            Components.Add<KeyboardService>(new KeyboardService());

            LoadTextures();
        }

        public override void Activate()
        {
            Selected = SelectedMenuItem.NewGame;
            ArrowPosition = _newGameArrowPosition;
        }

        public override void Deactivate()
        {
 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Components.GetSingle<KeyboardService>().IsKeyPressed(Keys.Down))
            {
                switch (Selected)
                {
                    case SelectedMenuItem.NewGame:
                        {
                            Selected = SelectedMenuItem.Credits;
                            ArrowPosition = _creditsArrowPosition;
                            break;
                        }
                    case SelectedMenuItem.Credits:
                        {
                            Selected = SelectedMenuItem.Exit;
                            ArrowPosition = _exitArrowPosition;
                            break;
                        }
                    case SelectedMenuItem.Exit:
                        {
                            Selected = SelectedMenuItem.NewGame;
                            ArrowPosition = _newGameArrowPosition;
                            break;
                        }
                }
            }

            if (Components.GetSingle<KeyboardService>().IsKeyPressed(Keys.Up))
            {
                switch (Selected)
                {
                    case SelectedMenuItem.NewGame:
                        {
                            Selected = SelectedMenuItem.Exit;
                            ArrowPosition = _exitArrowPosition;
                            break;
                        }
                    case SelectedMenuItem.Credits:
                        {
                            Selected = SelectedMenuItem.NewGame;
                            ArrowPosition = _newGameArrowPosition;
                            break;
                        }
                    case SelectedMenuItem.Exit:
                        {
                            Selected = SelectedMenuItem.Credits;
                            ArrowPosition = _creditsArrowPosition;
                            break;
                        }
                }
            }

            if (Components.GetSingle<KeyboardService>().IsKeyPressed(Keys.Enter))
            {
                switch (Selected)
                {
                    case SelectedMenuItem.NewGame:
                        {
                            Game.GameScreenService.Push(new SpaceGameScreen("Space", Game));
                            break;
                        }
                    case SelectedMenuItem.Credits:
                        {

                            break;
                        }
                    case SelectedMenuItem.Exit:
                        {
                            Game.Exit();
                            break;
                        }
                }

            }
            if (Components.GetSingle<KeyboardService>().IsKeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
           
            base.Draw(spriteBatch);

            spriteBatch.Draw(Textures["MainMenuBackground"],new Rectangle(0,0,1920,1080), Color.Wheat);
            spriteBatch.Draw(Textures["MainMenuTitle"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Textures["MainMenuNewGameButton"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Textures["MainMenuCreditsButton"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Textures["MainMenuExitButton"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Textures["Arrow"], ArrowPosition, Color.Wheat);

            spriteBatch.End();
        }

        public enum SelectedMenuItem
        {
            NewGame, 
            Credits,
            Exit
        }

        public override void LoadTextures()
        {
            Textures.Add("MainMenuBackground", Game.Content.Load<Texture2D>("Background"));
            Textures.Add("MainMenuTitle", Game.Content.Load<Texture2D>("Title"));
            Textures.Add("MainMenuNewGameButton", Game.Content.Load<Texture2D>("NewGameButton"));
            Textures.Add("MainMenuCreditsButton", Game.Content.Load<Texture2D>("CreditsButton"));
            Textures.Add("MainMenuExitButton", Game.Content.Load<Texture2D>("ExitGameButton"));

            Textures.Add("Arrow", Game.Content.Load<Texture2D>("Arrow"));
        }

        public override void LoadSoundEffects()
        {
    
        }
    }
}