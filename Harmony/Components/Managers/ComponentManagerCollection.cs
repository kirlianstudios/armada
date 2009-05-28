#region

using System.Collections.Generic;

#endregion

namespace Harmony.Components.Managers
{
    public class ManagerCollection<T> : List<IComponentManager<T>> where T : IComponent
    {
    }
}