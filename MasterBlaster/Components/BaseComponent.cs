using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MasterBlaster.Components
{
    public class BaseComponent : IComponent
    {
        public Guid Id { get; private set; }
        public bool Destroyed { get; set; }

        public BaseComponent()
        {
            Id = Guid.NewGuid();
        }
    }
}