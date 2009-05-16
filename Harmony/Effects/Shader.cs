using Harmony.Cameras;
using Harmony.Components;
using Microsoft.Xna.Framework;

namespace Harmony.Effects
{
    public class Shader : Effect
    {
        public Shader(string a_asset) : base(a_asset)
        {
        }

        public override void SetParameters(IRenderable a_renderable)
        {
            Matrix world =
                CameraManager.ActiveCamera.World*
                Matrix.CreateScale(a_renderable.Scale)*
                Matrix.CreateFromQuaternion(a_renderable.Rotation)*
                Matrix.CreateTranslation(a_renderable.Position);

            if (null != BackingEffect.Parameters["World"])
            {
                BackingEffect.Parameters["World"].SetValue(world);
            }

            if (null != BackingEffect.Parameters["View"])
            {
                BackingEffect.Parameters["View"].SetValue(CameraManager.ActiveCamera.View);
            }

            if (null != BackingEffect.Parameters["Projection"])
            {
                BackingEffect.Parameters["Projection"].SetValue(CameraManager.ActiveCamera.Projection);
            }

            if (null != BackingEffect.Parameters["EyePosition"])
            {
                BackingEffect.Parameters["EyePosition"].SetValue(CameraManager.ActiveCamera.Position);
            }

            if (null != BackingEffect.Parameters["LightDirection"])
            {
                BackingEffect.Parameters["LightDirection"].SetValue(new Vector3(1, -1, -1));
            }
            if (null != BackingEffect.Parameters["LightDiffuseColor"])
            {
                BackingEffect.Parameters["LightDiffuseColor"].SetValue(new Vector3(0.5f));
            }

            if (null != BackingEffect.Parameters["LightSpecularColor"])
            {
                BackingEffect.Parameters["LightSpecularColor"].SetValue(new Vector3(0.5f));
            }

            base.SetParameters(a_renderable);
        }
    }
}