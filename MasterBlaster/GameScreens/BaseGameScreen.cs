using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.GameScreens
{
    public abstract class BaseGameScreen : IGameScreen
    {
        public string Name { get; private set; }
        public RunGame Game { get; private set; }

        public BaseGameScreen(string name, RunGame game)
        {
            Game = game;
            Name = name;
        }

        public abstract void Initialize();

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}