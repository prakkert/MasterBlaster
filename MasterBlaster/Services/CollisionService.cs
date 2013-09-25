using MasterBlaster.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class CollisionService
    {
        public void CheckForCollisions(List<ICollidableComponent> components)
        {
            for (int i = 0; i < components.Count; i++)
            {
                for (int j = i + 1; j < components.Count; j++)
                {
                    if (components[i] != null && components[j] != null && components[i].CollisionBoundaries.Intersects(components[j].CollisionBoundaries))
                    {
                        components[i].CollidedWith(components[j]);
                        components[j].CollidedWith(components[i]);
                    }
                }
            }
        }
    }
}