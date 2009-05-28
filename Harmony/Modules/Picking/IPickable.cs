#region

using Harmony.Components;
using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Modules.Picking
{
    public interface IPickable : IComponent
    {
        BoundingBox BoundingBox { get; set; }
        bool Selected { get; set; }
    }
}