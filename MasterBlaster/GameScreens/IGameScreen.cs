using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.GameScreens
{
    public interface IGameScreen
    {
        string Name { get; }

        void Activate();
        void Deactivate();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}