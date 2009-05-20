using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony.Components;

namespace Harmony.Modules
{
    public interface IModule : IInitializable
    {
        string Handle { get; set; }
        bool Active { get; set; }

        List<IModule> Dependencies { get; set; }
    }
}