using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Components
{
  public interface IUpdatableComponent : IComponent
    {
      void Update(GameTime gameTime);
    }
}
