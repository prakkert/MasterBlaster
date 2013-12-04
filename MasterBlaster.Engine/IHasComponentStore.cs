using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBlaster.Engine
{
    public interface IHasComponentStore
    {
        ComponentStore Components { get; }
    }
}
