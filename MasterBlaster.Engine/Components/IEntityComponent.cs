using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBlaster.Engine.Components
{
    public interface IEntityComponent : IComponent
    {
        bool Destroyed { get; set; }

        void Destroy();
    }
}
