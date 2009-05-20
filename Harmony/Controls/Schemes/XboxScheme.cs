using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Harmony.Cameras;
using Harmony.Devices.Inputs;
using Microsoft.Xna.Framework;

namespace Harmony.Controls.Schemes
{
    public class XboxScheme : Scheme
    {
        public override void Initialize()
        {
            base.Initialize();

            GamePads[0].OnJoystickMoved += Gamepad_OnJoystickMoved;
            GamePads[0].OnButtonPressed += Gamepad_OnButtonPressed;
        }

        private void Gamepad_OnButtonPressed(Collection<GamePadButton> buttons)
        {
            if (buttons.Contains(GamePadButton.Back))
            {
                SchemeManager.Game.Exit();
            }
        }

        private static void Gamepad_OnJoystickMoved(Vector2 leftStick, Vector2 rightStick)
        {
            CameraManager.ActiveCamera.Revolve(new Vector3(1, 0, 0), leftStick.Y * 0.1f);
            CameraManager.ActiveCamera.RevolveGlobal(new Vector3(0, 1, 0), leftStick.X * 0.1f);
            CameraManager.ActiveCamera.Translate(new Vector3(0, 0, -rightStick.Y * 0.5f));
        }
    }
}
