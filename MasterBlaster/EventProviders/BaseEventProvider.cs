using MasterBlaster.Components;
using MasterBlaster.Engine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.EventProviders
{
    public abstract class BaseEventProvider : IUpdatableComponent
    {
        public Guid Id { get; private set; }
        public bool Destroyed { get; set; }

        public BaseEventProvider()
        {
            Id = Guid.NewGuid();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}