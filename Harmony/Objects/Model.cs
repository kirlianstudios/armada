#region

using Harmony.Cameras;
using Harmony.Effects;
using Harmony.Modules.Picking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

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

            LoadContent(Engine.Game.GraphicsDevice, Engine.Game.Content);
        }

        public Vector3 AmbientColor { get; set; }
        public Vector3 DiffuseColor { get; set; }
        public float SpecularPower { get; set; }

        private string Asset { get; set; }
        public Microsoft.Xna.Framework.Graphics.Model BackingModel { get; private set; }

        #region IPickable Members

        public bool Selected { get; set; }
        public BoundingBox BoundingBox { get; set; }

        #endregion

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
            var effect = EffectManager.ActiveShader.BackingEffect;
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

        public override void Draw(GraphicsDevice a_graphicsDevice)
        {
            var World = CameraManager.ActiveCamera.World*Matrix.CreateScale(Scale)*
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

                // Each mesh is made of parts (grouped on the same material)
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    a_graphicsDevice.VertexDeclaration = part.VertexDeclaration;
                    a_graphicsDevice.Vertices[0].SetSource(mesh.VertexBuffer, part.StreamOffset, part.VertexStride);
                    a_graphicsDevice.Textures[0] = ((BasicEffect) part.Effect).Texture;
                    SetMaterialProperties();
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