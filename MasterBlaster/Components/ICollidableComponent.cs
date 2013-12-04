using MasterBlaster.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Components
{
    public interface ICollidableComponent : IComponent
    {
        Rectangle CollisionBoundaries { get; }

        void CollidedWith(ICollidableComponent component);
    }
}
