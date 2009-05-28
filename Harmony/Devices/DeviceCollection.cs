#region

using Harmony.Components;

#endregion

namespace Harmony.Devices
{
    internal class DeviceCollection<TKey, TValue> : ComponentCollection<TKey, TValue> where TValue : IDevice
                                                                                      where TKey : Id
    {
    }
}