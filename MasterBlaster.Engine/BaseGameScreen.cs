using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
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

        public ComponentStore Components { get; private set; }

        public BaseGameScreen(string name, BaseGame game, List<IComponent> components = null)
        {
            Game = game;
            Name = name;

            if (components == null)
            {
                Components = new ComponentStore();
            }
            else
            {
                Components = new ComponentStore(components);
            }
        }

        public abstract void Activate();

        public abstract void Deactivate();

        public virtual void Update(GameTime gameTime)
        {
            var updatableComponents = Components.GetAllOfType<IUpdatableComponent>();

            foreach (var updatableComponent in updatableComponents)
            {
                updatableComponent.Update(gameTime);
            }

            RemoveDestroyedItems();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            List<IDrawableComponent> drawableComponents = Components.GetAllOfType<IDrawableComponent>();

            foreach (var drawableComponent in drawableComponents)
            {
                drawableComponent.Draw(spriteBatch);
            }
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