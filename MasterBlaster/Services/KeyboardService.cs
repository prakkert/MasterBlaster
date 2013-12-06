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

        private List<KeyValuePair<KeyCombination, Action>> keyActions;

        public KeyboardService()
        {
            LastKeyboardState = Keyboard.GetState();
            CurrentKeyboardState = LastKeyboardState;

            keyActions = new List<KeyValuePair<KeyCombination, Action>>();
        }

        public void Update(GameTime gameTime)
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            foreach (var keyAction in keyActions)
            {
                bool result = true;

                foreach (var keyCombination in keyAction.Key.Keys)
                {
                    if (!IsKeyInState(keyCombination.Key, keyCombination.Value))
                    {
                        result = false;
                        break;
                    }
                }

                if (result)
                {
                    keyAction.Value.Invoke();
                }
            }
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

        private bool IsKeyInState(Keys key, KeyEventType keyEventType)
        {
            switch (keyEventType)
            {
                case KeyEventType.Up:
                    {
                        return IsKeyUp(key);
                    }
                case KeyEventType.Down:
                    {
                        return IsKeyDown(key);
                    }
                case KeyEventType.Pressed:
                    {
                        return IsKeyPressed(key);
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public bool IsKeyUp(Keys key)
        {
            return CurrentKeyboardState.IsKeyUp(key);
        }

        public void RegisterKeyListener(KeyCombination keyCombination, Action keyAction)
        {
            keyActions.Add(new KeyValuePair<KeyCombination, Action>(keyCombination, keyAction));
        }

        public class KeyCombination
        {
            public List<KeyValuePair<Keys, KeyEventType>> Keys;

            public KeyCombination(Keys key, KeyEventType keyEventType)
            {
                Keys = new List<KeyValuePair<Keys, KeyEventType>>();
                Keys.Add(new KeyValuePair<Keys, KeyEventType>(key, keyEventType));
            }

            public KeyCombination(Keys key1, KeyEventType keyEventType1, Keys key2, KeyEventType keyEventType2)
            {
                Keys = new List<KeyValuePair<Keys, KeyEventType>>();
                Keys.Add(new KeyValuePair<Keys, KeyEventType>(key1, keyEventType1));
                Keys.Add(new KeyValuePair<Keys, KeyEventType>(key2, keyEventType2));
            }

            public KeyCombination(Keys key1, KeyEventType keyEventType1, Keys key2, KeyEventType keyEventType2, Keys key3, KeyEventType keyEventType3)
            {
                Keys = new List<KeyValuePair<Keys, KeyEventType>>();
                Keys.Add(new KeyValuePair<Keys, KeyEventType>(key1, keyEventType1));
                Keys.Add(new KeyValuePair<Keys, KeyEventType>(key2, keyEventType2));
                Keys.Add(new KeyValuePair<Keys, KeyEventType>(key3, keyEventType3));
            }

            public KeyCombination(List<KeyValuePair<Keys, KeyEventType>> keys)
            {
                Keys = keys;
            }
        }
        public enum KeyEventType
        {
            Pressed,
            Up,
            Down
        }
    }
}