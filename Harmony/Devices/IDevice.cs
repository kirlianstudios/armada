#region

using System;
using Harmony.Components;

#endregion

namespace Harmony.Devices
{
    public interface IDevice : IUpdateable, IInitializable, IDisposable
    {
    }
}