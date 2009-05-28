#region

using Harmony.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Components
{
    public interface IDrawable : IComponent
    {
        Shader Shader { get; set; }
        VertexDeclaration VertexDeclaration { get; set; }
        Vector3 Position { get; set; }
        Vector3 Scale { get; set; }
        Quaternion Rotation { get; set; }

        void Draw(GraphicsDevice a_graphicsDevice);
    }
}