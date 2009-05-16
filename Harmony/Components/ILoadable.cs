using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Components
{
    public interface ILoadable : IComponent
    {
        void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager);
        void UnloadContent();
    }
}