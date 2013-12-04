using MasterBlaster.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Engine
{
    public abstract class BaseGameScreen : IGameScreen
    {
        public string Name { get; protected set; }
        public BaseGame Game { get; private set; }

        public List<IComponent> Components { get; protected set; }

        public BaseGameScreen(string name, BaseGame game, List<IComponent> components = null)
        {
            Game = game;
            Name = name;

            if (components == null)
            {
                Components = new List<IComponent>();
            }
            else
            {
                Components = components;
            }
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public virtual void Update(GameTime gameTime)
        {
            var updatableServices = Game.ComponentStore.GetAllOfType<IUpdatableComponent>();

            RemoveDestroyedItems();
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        public List<T> GetComponentsOfType<T>()
        {
            List<T> items = new List<T>();

            foreach (var component in Components)
            {
                if (component is T)
                {
                    items.Add((T)component);
                }
            }

            return items;
        }

        public void RemoveDestroyedItems()
        {
          //  Components.RemoveAll(c => c.Destroyed);
        }

        public void DrawBorder(SpriteBatch spriteBatch, Texture2D pixel, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}