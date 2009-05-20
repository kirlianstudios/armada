using System;
using Harmony.Components;

namespace Harmony.Devices
{
    public interface IDevice : IUpdateable, IInitializable, IDisposable
    {
    }
}