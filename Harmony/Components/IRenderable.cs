using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Components
{
    public interface IRenderable : IComponent
    {
        string Shader { get; set; }
        VertexDeclaration VertexDeclaration { get; set; }
        Vector3 Position { get; set; }
        Vector3 Scale { get; set; }
        Quaternion Rotation { get; set; }

        void Render(GraphicsDevice a_graphicsDevice);
    }
}