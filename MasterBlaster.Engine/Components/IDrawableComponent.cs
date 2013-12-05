using MasterBlaster.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Engine.Components
{
    public interface IDrawableComponent : IComponent
    {
        Texture2D Texture { get; set; }
        Vector2 Position { get; set; }
        float Rotation { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }
}
