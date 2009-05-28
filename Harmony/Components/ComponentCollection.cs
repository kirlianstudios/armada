#region

using System.Collections.Generic;

#endregion

namespace Harmony.Components
{
    public class ComponentCollection<TKey, TValue> : Dictionary<TKey, TValue> where TValue : IComponent where TKey : Id
    {
    }
}