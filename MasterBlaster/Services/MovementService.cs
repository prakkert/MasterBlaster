using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class MovementService : IUpdatableComponent
    {
        private ComponentStore _components;

        public MovementService(ComponentStore components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
            var movableComponents = _components.GetAllOfType<IMovableComponent>();

            foreach (var component in movableComponents)
            {
                component.Move(gameTime);
            }
        }
    }
}