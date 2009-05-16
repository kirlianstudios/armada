using Harmony.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Effects
{
    public sealed class PostProcessor : Effect, IPostProcessable
    {
        private VertexPositionTexture[] Vertices;

        public PostProcessor(string a_asset) : base(a_asset)
        {
        }

        private ResolveTexture2D ResolvedTexture { get; set; }
        private VertexDeclaration VertexDeclaration { get; set; }

        #region IPostProcessable Members

        public void PostProcess(GraphicsDevice a_graphicsDevice)
        {
            if (ResolvedTexture.Width != a_graphicsDevice.Viewport.Width || ResolvedTexture.Height != a_graphicsDevice.Viewport.Height ||
                ResolvedTexture.Format != a_graphicsDevice.DisplayMode.Format)
            {
                LoadResolveTarget(a_graphicsDevice);
            }
            a_graphicsDevice.ResolveBackBuffer(ResolvedTexture);
            a_graphicsDevice.Textures[0] = ResolvedTexture;

            BackingEffect.Begin();
            foreach (EffectPass pass in BackingEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                a_graphicsDevice.VertexDeclaration = VertexDeclaration;
                a_graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, Vertices, 0, 2);
                pass.End();
            }
            BackingEffect.End();

            EffectManager.ScreenTexture = ResolvedTexture;
        }

        #endregion

        private void LoadResolveTarget(GraphicsDevice a_graphicsDevice)
        {
            ResolvedTexture = new ResolveTexture2D(a_graphicsDevice, a_graphicsDevice.Viewport.Width,
                                                   a_graphicsDevice.Viewport.Height, 1,
                                                   a_graphicsDevice.DisplayMode.Format);
        }

        public override void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager)
        {
            base.LoadContent(a_graphicsDevice, a_contentManager);
            Vertices = new[]
                           {
                               new VertexPositionTexture(new Vector3(-1, -1, 0), new Vector2(0, 1)),
                               new VertexPositionTexture(new Vector3(-1, 1, 0), new Vector2(0, 0)),
                               new VertexPositionTexture(new Vector3(1, -1, 0), new Vector2(1, 1)),
                               new VertexPositionTexture(new Vector3(1, 1, 0), new Vector2(1, 0))
                           };
            VertexDeclaration = new VertexDeclaration(a_graphicsDevice, VertexPositionTexture.VertexElements);
            LoadResolveTarget(a_graphicsDevice);
        }
    }
}