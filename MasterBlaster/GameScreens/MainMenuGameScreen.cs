using MasterBlaster.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBlaster.GameScreens
{
    public partial class MainMenuGameScreen : BaseGameScreen
    {
        public SelectedMenuItem Selected { get; private set; }

        public Vector2 ArrowPosition { get; private set; }

        private readonly Vector2 _newGameArrowPosition = new Vector2(500, 474);
        private readonly Vector2 _creditsArrowPosition = new Vector2(500, 590);
        private readonly Vector2 _exitArrowPosition = new Vector2(500, 697);

        public MainMenuGameScreen(RunGame game) : base("MainMenu", game)
        {

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
            if (Game.CurrentKeyboardState.IsKeyDown(Keys.Down) && Game.LastKeyboardState.IsKeyUp(Keys.Down))
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

            if (Game.CurrentKeyboardState.IsKeyDown(Keys.Up) && Game.LastKeyboardState.IsKeyUp(Keys.Up))
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

            if (Game.CurrentKeyboardState.IsKeyDown(Keys.Enter) && Game.LastKeyboardState.IsKeyUp(Keys.Enter))
            {
                switch (Selected)
                {
                    case SelectedMenuItem.NewGame:
                        {
                            GameServices.GetService<GameScreenService>().Push(new SpaceGameScreen("Space", Game));
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game.Textures["MainMenuBackground"],new Rectangle(0,0,1920,1080), Color.Wheat);
            spriteBatch.Draw(Game.Textures["MainMenuTitle"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Game.Textures["MainMenuNewGameButton"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Game.Textures["MainMenuCreditsButton"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Game.Textures["MainMenuExitButton"], new Rectangle(0, 0, 1920, 1080), Color.Wheat);
            spriteBatch.Draw(Game.Textures["Arrow"], ArrowPosition, Color.Wheat);
        }

        public enum SelectedMenuItem
        {
            NewGame, 
            Credits,
            Exit
        }
    }
}