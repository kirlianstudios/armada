#region

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Components
{
    public interface ILoadable : IComponent
    {
        string Path { get; set; }
        void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager);
        void UnloadContent();
    }
}