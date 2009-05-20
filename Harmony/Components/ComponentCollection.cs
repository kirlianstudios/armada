using System;
using System.Collections.Generic;

namespace Harmony.Components
{
    public class ComponentCollection<T> : Dictionary<Id, T>
    {
        private Dictionary<Type, List<T>> Map { get; set; }

        public ComponentCollection()
        {
            Id.Types.Add(typeof(Guid));
            Id.Types.Add(typeof(string));

            Map = new Dictionary<Type, List<T>>();
        }

        public void Add(Object a_idObject, T a_component)
        {
            foreach(var type in Id.Types)
            {
                if (type.IsInstanceOfType(a_idObject))
                {
                    if (Map.ContainsKey(type))
                    {
                        Map[type].Add((T)a_idObject);
                    }
                }
            }
        }
    }
}