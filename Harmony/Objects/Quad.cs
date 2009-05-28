#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Objects
{
    public class Quad : GameObject
    {
        public Quad(string a_asset, Color a_color)
        {
            Asset = a_asset;
            Color = a_color;
            Scale = new Vector3(1);
        }

        private string Asset { get; set; }

        public VertexPositionColorTexture[] Vertices { get; set; }

        public Color Color { get; set; }

        public Texture2D Texture { get; set; }

        public int[] Indexes { get; set; }

        public override void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager)
        {
            Texture = a_contentManager.Load<Texture2D>(Asset);

            VertexDeclaration = new VertexDeclaration(a_graphicsDevice, VertexPositionColorTexture.VertexElements);

            Vertices = new[]
                           {
                               new VertexPositionColorTexture(new Vector3(-0.5f, 0.5f, 0), Color, new Vector2(0, 0)),
                               new VertexPositionColorTexture(new Vector3(0.5f, 0.5f, 0), Color, new Vector2(1, 0)),
                               new VertexPositionColorTexture(new Vector3(-0.5f, -0.5f, 0), Color, new Vector2(0, 1)),
                               new VertexPositionColorTexture(new Vector3(0.5f, -0.5f, 0), Color, new Vector2(1, 1))
                           };

            Indexes = new[] {0, 1, 2, 1, 3, 2};
        }

        public override void UnloadContent()
        {
        }

        public override void Draw(GraphicsDevice a_graphicsDevice)
        {
            a_graphicsDevice.Textures[0] = Texture;
            a_graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, Vertices, 0, 4, Indexes, 0, 2);
        }
    }
}