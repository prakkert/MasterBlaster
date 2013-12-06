using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBlaster.Services
{
    public class DestructionService : IUpdatableComponent
    {
        private ComponentStore _components;
        public DestructionService(ComponentStore components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
          
        }
    }
}