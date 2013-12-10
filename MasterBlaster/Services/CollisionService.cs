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

                            if (a1.Bounced || a2.Bounced)
                            {
                                continue;
                            }

                            var normala1 = a1.Position - a2.Position;
                            var normala2 = a2.Position - a1.Position;

                            normala1.Normalize();
                            normala2.Normalize();

                            Vector2 CMVelocity = Vector2.Divide(Vector2.Multiply(a1.Velocity, (float)a1.Mass) + Vector2.Multiply(a2.Velocity, (float)a2.Mass), (float)(a1.Mass + a2.Mass));

                            a1.Direction -= CMVelocity;
                            a1.Direction = Vector2.Reflect(a1.Direction, normala1);
                            a1.Direction += CMVelocity;

                            a2.Direction -= CMVelocity;
                            a2.Direction = Vector2.Reflect(a2.Direction, normala2);
                            a2.Direction += CMVelocity;

                            a1.Bounced = true;
                            a2.Bounced = true;
                        }
                    }
                }
            }
        }
    }
}