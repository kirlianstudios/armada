using Harmony.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Objects
{
    public abstract class GameObject : IRenderable, ILoadable
    {
        protected GameObject()
        {
            Position = new Vector3();
            Scale = new Vector3(1);
            Rotation = new Quaternion();
        }

        #region ILoadable Members

        public abstract void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager);
        public abstract void UnloadContent();

        #endregion

        #region IRenderable Members

        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Quaternion Rotation { get; set; }

        public string Shader { get; set; }
        public VertexDeclaration VertexDeclaration { get; set; }

        public abstract void Render(GraphicsDevice a_graphicsDevice);

        #endregion
    }
}