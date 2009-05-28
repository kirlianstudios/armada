#region

using System;

#endregion

namespace Harmony.Components
{
    public class HarmonyException : Exception
    {
        public HarmonyException(string a_message, Exception a_innerException) : base(a_message, a_innerException)
        {
        }
    }
}