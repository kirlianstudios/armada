#region

using System.Collections.Generic;
using Harmony.Components;

#endregion

namespace Harmony.Modules
{
    public interface IModule : IInitializable
    {
        string Handle { get; set; }
        bool Active { get; set; }

        List<IModule> Dependencies { get; set; }
    }
}