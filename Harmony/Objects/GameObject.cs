#region

using Harmony.Components;
using Harmony.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable=Harmony.Components.IDrawable;
using IUpdateable=Harmony.Components.IUpdateable;

#endregion

namespace Harmony.Objects
{
    public abstract class GameObject : IDrawable, ILoadable, IUpdateable
    {
        protected GameObject()
        {
            Position = new Vector3();
            Scale = new Vector3(1);
            Rotation = new Quaternion();
        }

        #region IDrawable Members

        public Id Id { get; set; }

        // path these through to specific objects

        // support general rendering operations
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Quaternion Rotation { get; set; }

        // shaders
        public Shader Shader { get; set; }
        public VertexDeclaration VertexDeclaration { get; set; }

        public abstract void Draw(GraphicsDevice a_graphicsDevice);

        #endregion

        #region ILoadable Members

        public abstract void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager);
        public abstract void UnloadContent();
        public string Path { get; set; }

        #endregion

        #region IUpdateable Members

        public void Update(GameTime a_gameTime)
        {
            // nothing
        }

        #endregion
    }
}