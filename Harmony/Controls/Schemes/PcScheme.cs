using System.Collections.ObjectModel;
using Harmony.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Harmony.Controls.Schemes
{
    public class PcScheme : Scheme
    {
        public override void Initialize()
        {
            Keyboard.KeyPress += Keyboard_KeyPress;

            Mouse.MouseMoved += Mouse_MouseMoved;
        }

        private void Mouse_MouseMoved(Vector2 a_move)
        {
            if (Microsoft.Xna.Framework.Input.Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                CameraManager.ActiveCamera.Revolve(Vector3.Up, a_move.X);
                CameraManager.ActiveCamera.Revolve(Vector3.UnitX, a_move.Y);
            }
        }

        private void Keyboard_KeyPress(Collection<Keys> a_keys)
        {
            if (a_keys.Contains(Keys.F))
            {
                if (SchemeManager.Game is Game)
                {
                    ((Game) SchemeManager.Game).ToggleFullScreen();
                }
            }

            // Allows the game to exit
            if (a_keys.Contains(Keys.Escape))
            {
                SchemeManager.Game.Exit();
            }
        }
    }
}