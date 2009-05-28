#region

using System.Collections.Generic;
using Harmony.Cameras.Behaviours.Targeting;
using Harmony.Components;

#endregion

namespace Harmony.Cameras.Targets
{
    public interface ICameraTarget : IComponent
    {
        Dictionary<ICamera, ComponentCollection<Id, ITargetingCameraBehaviour>> TargetMap { get; set; }
    }
}