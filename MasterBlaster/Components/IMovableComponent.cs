using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Components
{
    public interface IMovableComponent : IComponent
    {
        Vector2 Position { get; set; }
        Vector2 Direction { get; set; }
        float Speed { get; set; }

        void Move(GameTime gameTime);
    }
}