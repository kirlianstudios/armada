#region

using System;
using System.Collections.ObjectModel;

#endregion

namespace Harmony
{
    public static class GuidManager
    {
        static GuidManager()
        {
            Guids = new Collection<Guid>();
        }

        public static Collection<Guid> Guids { get; set; }

        public static Guid NewGuid()
        {
            var guid = Guid.NewGuid();
            while (Guids.Contains(guid))
            {
                Guid.NewGuid();
            }
            Guids.Add(guid);
            return guid;
        }
    }
}