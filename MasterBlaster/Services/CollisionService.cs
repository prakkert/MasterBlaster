using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using MasterBlaster.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class CollisionService : IUpdatableComponent
    {
        private ComponentStore _components;

        public CollisionService(ComponentStore components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
            List<ICollidableComponent> collidableComponents = _components.GetAllOfType<ICollidableComponent>();

            for (int i = 0; i < collidableComponents.Count; i++)
            {
                for (int j = i + 1; j < collidableComponents.Count; j++)
                {
                    if (collidableComponents[i] != null && collidableComponents[j] != null && collidableComponents[i].CollisionBoundaries.Intersects(collidableComponents[j].CollisionBoundaries))
                    {
                        collidableComponents[i].CollidedWith(collidableComponents[j]);
                        collidableComponents[j].CollidedWith(collidableComponents[i]);

                        if (collidableComponents[i] is Asteroid && collidableComponents[j] is Asteroid)
                        {
                            var a1 = collidableComponents[i] as Asteroid;
                            var a2 = collidableComponents[j] as Asteroid;
                        }
                    }
                }
            }
        }
    }
}