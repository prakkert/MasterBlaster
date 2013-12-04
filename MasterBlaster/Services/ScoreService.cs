using MasterBlaster.Components;
using MasterBlaster.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class ScoreService : IUpdatableComponent
    {
       public int Points { get; private set; }

       public void AddScore(int score = 1)
       {
           Points += score;
       }

       public void ResetScore()
       {
           Points = 0;
       }

       public void Update(Microsoft.Xna.Framework.GameTime gameTime)
       {
           throw new NotImplementedException();
       }
    }
}
