using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Engine
{
    public interface IGameScreen : IHasComponentStore
    {
        string Name { get; }

        void Activate();
        void Deactivate();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}