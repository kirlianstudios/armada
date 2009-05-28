#region

using System;

#endregion

namespace Harmony.Components
{
    public class ComponentException : HarmonyException
    {
        public ComponentException(string a_message, Exception a_innerException) : base(a_message, a_innerException)
        {
        }

        public ComponentException(string a_message)
            : base(a_message, null)
        {
        }
    }
}