using System;
using Harmony.Cameras;
using Harmony.Components;
using Harmony.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Effect=Microsoft.Xna.Framework.Graphics.Effect;

namespace Harmony.Objects
{
    public class Model : GameObject, IPickable
    {
        public Model(string a_asset)
        {
            Asset = a_asset;
            Scale = new Vector3(1);

            AmbientColor = new Vector3(0.15f);
            DiffuseColor = new Vector3(0.25f);
            SpecularPower = 8;

            Selected = false;
        }

        private bool Selected { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public Vector3 AmbientColor { get; set; }
        public Vector3 DiffuseColor { get; set; }
        public float SpecularPower { get; set; }

        private string Asset { get; set; }
        public Microsoft.Xna.Framework.Graphics.Model BackingModel { get; private set; }

        public BoundingBox GetBoundingBox()
        {
            return BoundingBox;
        }

        public void SetSelected(bool a_selected)
        {
            Selected = a_selected;
        }

        public override void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager)
        {
            BackingModel = a_contentManager.Load<Microsoft.Xna.Framework.Graphics.Model>(Asset);
        }

        public void SetMaterialProperties()
        {
            Effect effect = EffectManager.ActiveShader.BackingEffect;
            if (null != effect.Parameters["AmbientColor"])
            {
                effect.Parameters["AmbientColor"].SetValue(AmbientColor);
            }
            if (null != effect.Parameters["DiffuseColor"])
            {
                effect.Parameters["DiffuseColor"].SetValue(DiffuseColor);
            }
            if (null != effect.Parameters["SpecularPower"])
            {
                effect.Parameters["SpecularPower"].SetValue(SpecularPower);
            }
            effect.CommitChanges();
        }

        public override void Render(GraphicsDevice a_graphicsDevice)
        {
            Matrix World = CameraManager.ActiveCamera.World*Matrix.CreateScale(Scale)*
                           Matrix.CreateFromQuaternion(Rotation)*Matrix.CreateTranslation(Position);

            foreach (ModelMesh mesh in BackingModel.Meshes)
            {
                a_graphicsDevice.Indices = mesh.IndexBuffer;

                if (null != EffectManager.ActiveShader.BackingEffect.Parameters["World"])
                {
                    EffectManager.ActiveShader.BackingEffect.Parameters["World"].SetValue(World*
                                                                                          mesh.ParentBone.Transform);
                }

                EffectManager.ActiveShader.BackingEffect.CommitChanges();

                // Each mesh is made of parts (grouped by texture, etc.)
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    // Change the device settings for each part to be rendered
                    a_graphicsDevice.VertexDeclaration = part.VertexDeclaration;
                    a_graphicsDevice.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);

                    // Make sure we use the texture for the current part also
                    a_graphicsDevice.Textures[0] = ((BasicEffect) part.Effect).Texture;

                    // Set up the shader properties for this mesh part
                    SetMaterialProperties();

                    // Finally draw the actual triangles on the screen
                    a_graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0,
                                                           part.NumVertices, part.StartIndex, part.PrimitiveCount);
                }
            }
        }

        public override void UnloadContent()
        {
        }
    }
}