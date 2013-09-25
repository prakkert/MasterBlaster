using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Components
{
    public interface IDrawableComponent : IComponent
    {
        Texture2D Texture { get; set; }
        void Draw(SpriteBatch spriteBatch);
    }
}
