using IComponent = Harmony.Components.IComponent;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Components
{
    public interface IPostProcessable : IComponent
    {
        void PostProcess(GraphicsDevice a_graphicsDevice);
    }
}