using MasterBlaster.Components;
using MasterBlaster.Engine;
using MasterBlaster.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Services
{
    public class ScoreService : IComponent
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
    }
}
