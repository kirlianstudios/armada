using Harmony.Cameras;
using Harmony.Effects;
using Harmony.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HMEngine.HMObjects
{
    public class Skybox : GameObject
    {
        private readonly Color Color;
        private readonly string[] Files;
        private readonly Vector3[] Offset;
        private readonly Quad[] Sides;

        public Skybox(string[] textures, Color color)
        {
            Files = textures;
            Sides = new Quad[6];
            Offset = new Vector3[6];
            Color = color;
            Scale = new Vector3(5);
            CreateSides();
        }

        private void CreateSides()
        {
            for (int i = 0; i < 6; i++)
            {
                Sides[i] = new Quad(Files[i], Color) {Scale = Scale};
            }
            Sides[0].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.PiOver2);
            Sides[1].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -MathHelper.PiOver2);
            Sides[2].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.PiOver2);
            Sides[3].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), -MathHelper.PiOver2);
            Sides[5].Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.Pi);
            CalculateOffsets();
        }

        private void CalculateOffsets()
        {
            Offset[0] = new Vector3(0, 0.5f, 0)*Scale;
            Offset[1] = new Vector3(0, -0.5f, 0)*Scale;
            Offset[2] = new Vector3(-0.5f, 0, 0)*Scale;
            Offset[3] = new Vector3(0.5f, 0, 0)*Scale;
            Offset[4] = new Vector3(0, 0, -0.5f)*Scale;
            Offset[5] = new Vector3(0, 0, 0.5f)*Scale;
        }

        public override void LoadContent(GraphicsDevice aGraphicsDevice, ContentManager aContentManager)
        {
            foreach (Quad quad in Sides)
            {
                quad.LoadContent(aGraphicsDevice, aContentManager);
            }
            VertexDeclaration = Sides[0].VertexDeclaration;
        }

        public override void Render(GraphicsDevice aGraphicsDevice)
        {
            aGraphicsDevice.RenderState.DepthBufferWriteEnable = false;
            for (int i = 0; i < 6; i++)
            {
                Sides[i].Position = CameraManager.ActiveCamera.Position + Offset[i];
                EffectManager.ActiveShader.SetParameters(Sides[i]);
                Sides[i].Render(aGraphicsDevice);
            }
            aGraphicsDevice.RenderState.DepthBufferWriteEnable = true;
        }

        public override void UnloadContent()
        {
        }
    }
}