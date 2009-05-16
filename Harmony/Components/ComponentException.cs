using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harmony.Components
{
    internal sealed class ComponentException : Exception
    {
        internal ComponentException(string a_message) : base(a_message)
        {
            
        }
    }
}
