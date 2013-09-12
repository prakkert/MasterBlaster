using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterBlaster.Components
{
    public interface IComponent
    {
        void Initialize(Game game);

        void LoadContent(ContentManager content);
        void UnloadContent();
    }
}