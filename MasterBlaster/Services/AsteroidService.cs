using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using MasterBlaster.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBlaster.Services
{
    public class AsteroidService : IUpdatableComponent
    {
        private ComponentStore _components;

        public AsteroidService(ComponentStore components)
        {
            _components = components;
        }

        public void Update(GameTime gameTime)
        {
            var asteroids = _components.GetAllOfType<Asteroid>();

            foreach (var asteroid in asteroids)
            {
                if (asteroid.Destroyed)
                {
                    if (asteroid.Size == 5)
                    {
                        _components.Add(new Asteroid(asteroid.Texture, Asteroid.AsteroidSize.Medium, asteroid.Position));
                        _components.Add(new Asteroid(asteroid.Texture, Asteroid.AsteroidSize.Medium, asteroid.Position));
                        _components.Add(new Asteroid(asteroid.Texture, Asteroid.AsteroidSize.Medium, asteroid.Position));
                    }
                    else if (asteroid.Size == 3)
                    {
                        _components.Add(new Asteroid(asteroid.Texture, Asteroid.AsteroidSize.Small, asteroid.Position));
                        _components.Add(new Asteroid(asteroid.Texture, Asteroid.AsteroidSize.Small, asteroid.Position));
                        _components.Add(new Asteroid(asteroid.Texture, Asteroid.AsteroidSize.Small, asteroid.Position));
                    }

                    _components.Remove<Asteroid>(asteroid);
                }
            }
        }
    }
}