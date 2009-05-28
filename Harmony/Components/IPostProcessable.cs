#region

using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Components
{
    public interface IPostProcessable : IComponent
    {
        void PostProcess(GraphicsDevice a_graphicsDevice);
    }
}