using Microsoft.Xna.Framework;

namespace Harmony.Components
{
    public interface IPickable : IComponent
    {
        BoundingBox GetBoundingBox();
        void SetSelected(bool a_selected);
    }
}