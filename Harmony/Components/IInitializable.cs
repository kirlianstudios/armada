using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harmony.Components
{
    public interface IInitializable : IComponent
    {
        void Initialize();
    }
}
