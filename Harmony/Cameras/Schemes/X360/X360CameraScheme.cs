#region

using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Cameras.Schemes.X360
{
    public class X360CameraScheme : CameraScheme
    {
        public override void Initialize()
        {
            base.Initialize();

            GamePads[PlayerIndex.One].OnJoystickMoved += Gamepad_OnJoystickMoved;
        }

        protected void Gamepad_OnJoystickMoved(Vector2 leftStick, Vector2 rightStick)
        {
            ActiveCamera.Revolve(new Vector3(1, 0, 0), rightStick.Y*0.1f);
            ActiveCamera.Revolve(new Vector3(0, 1, 0), rightStick.X*0.1f);
            ActiveCamera.Translate(new Vector3(0, leftStick.Y*0.5f, 0));
            ActiveCamera.Translate(new Vector3(leftStick.X*0.5f, 0, 0));
        }
    }
}