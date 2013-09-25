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
        Guid Id { get; }
        bool Destroyed { get; set; }
    }
}