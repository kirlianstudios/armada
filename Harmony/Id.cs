#region

using System;
using System.Collections.ObjectModel;

#endregion

namespace Harmony
{
    public class Id : IEquatable<Id>
    {
        public static Collection<Type> Types { get; set; }

        public Guid Guid { get; set; }
        public string Handle { get; set; }

        #region IEquatable<Id> Members

        public bool Equals(Id other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Guid.Equals(Guid) && Equals(other.Handle, Handle);
        }

        #endregion

        public static bool operator ==(Id a_idA, Id a_idB)
        {
            if ((object) a_idA == null)
            {
                return ((object) a_idB == null);
            }

            else
            {
                return a_idA.Equals(a_idB);
            }
        }

        public static bool operator !=(Id a_idA, Id a_idB)
        {
            return !(a_idA == a_idB);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Id)) return false;
            return Equals((Id) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Guid.GetHashCode()*397) ^ (Handle != null ? Handle.GetHashCode() : 0);
            }
        }
    }
}