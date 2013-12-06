using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using MasterBlaster.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBlaster.Services
{
    public class ShipService : IComponent {

        private ComponentStore _components;
        private BaseGameScreen _gameScreen;

        public ShipService(ComponentStore components, BaseGameScreen gameScreen)
        {
            _components = components;
            _gameScreen = gameScreen;

            var keyboardService = _components.GetSingle<KeyboardService>();

            keyboardService.RegisterKeyListener(new KeyboardService.KeyCombination(Keys.Up, KeyboardService.KeyEventType.Down, Keys.Down, KeyboardService.KeyEventType.Up), Accelerate);
            keyboardService.RegisterKeyListener(new KeyboardService.KeyCombination(Keys.Up, KeyboardService.KeyEventType.Up, Keys.Down, KeyboardService.KeyEventType.Down), Decelerate);
            keyboardService.RegisterKeyListener(new KeyboardService.KeyCombination(Keys.Left, KeyboardService.KeyEventType.Down), Left);
            keyboardService.RegisterKeyListener(new KeyboardService.KeyCombination(Keys.Right, KeyboardService.KeyEventType.Down), Right);
            keyboardService.RegisterKeyListener(new KeyboardService.KeyCombination(Keys.LeftControl, KeyboardService.KeyEventType.Pressed), Fire);
        }

        public void Fire()
        {
            var fireball = _components.GetSingleOrDefault<Fireball>();

            if (fireball == null)
            {
                var ship = _components.GetSingle<Ship>();
                _components.Add(new Fireball(_gameScreen.Textures["Fireball"], ship.Position, ship.Direction, ship.Rotation));

            }
            else
                //    fireball.Update(gameTime);

                if (fireball.Destroyed)
                {
                    _components.Remove<Fireball>(fireball);
                }

        }

        public void Accelerate()
        {
            var ship = _components.GetSingle<Ship>();

            if (ship.Speed < ship.MaxSpeed)
            {
                ship.Speed += 0.1f;
            }
            if (ship.Speed > ship.MaxSpeed)
            {
                ship.Speed = ship.MaxSpeed;
            }
        }
        public void Decelerate()
        {
            var ship = _components.GetSingle<Ship>();

            if (ship.Speed > 0)
            {
                ship.Speed -= 0.1f;
            }
            if (ship.Speed < 0)
            {
                ship.Speed = 0;
            }
        }

        public void Left()
        {
            var ship = _components.GetSingle<Ship>();

            ship.Rotation -= 0.1f;
        }

        public void Right()
        {
            var ship = _components.GetSingle<Ship>();

            ship.Rotation += 0.1f;
        }
    }
}