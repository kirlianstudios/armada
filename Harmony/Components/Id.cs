using System;
using System.Collections.ObjectModel;

namespace Harmony.Components
{
    public class Id
    {
        public static Collection<Type> Types { get; set; }

        public Guid Guid { get; set; }
        public string Handle { get; set; }
    }
}