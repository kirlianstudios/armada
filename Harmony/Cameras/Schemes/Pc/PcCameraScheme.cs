#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Harmony.Cameras.Schemes.Pc
{
    public class PcCameraScheme : CameraScheme
    {
        public PcCameraScheme()
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            MouseInputDevice.MouseMoved += Mouse_MouseMoved;
        }

        private void Mouse_MouseMoved(Vector2 a_move)
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                // scale the movement
                a_move *= -0.025f;
                ActiveCamera.Rotate(Vector3.Up, a_move.X);
                ActiveCamera.Rotate(Vector3.UnitX, a_move.Y);
            }
        }
    }
}