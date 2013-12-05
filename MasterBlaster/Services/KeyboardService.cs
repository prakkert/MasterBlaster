using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class KeyboardService : IUpdatableComponent
    {
        public KeyboardState LastKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }

        public KeyboardService()
        {
            LastKeyboardState = Keyboard.GetState();
            CurrentKeyboardState = LastKeyboardState;
        }

        public void Update(GameTime gameTime)
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key)
        {
            if (CurrentKeyboardState.IsKeyDown(key) && (LastKeyboardState.IsKeyUp(key)))
            {
                return true;
            }
            return false;
        }

        public bool IsKeyReleased(Keys key)
        {
            if (CurrentKeyboardState.IsKeyUp(key) && (LastKeyboardState.IsKeyDown(key)))
            {
                return true;
            }
            return false;
        }

        public bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }
    }
}