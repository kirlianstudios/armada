#region

using System.Collections.Generic;
using Harmony.Devices.Inputs.GamePad;
using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Controls.Schemes
{
    public class X360ControlScheme : ControlScheme
    {
        public override void Initialize()
        {
            base.Initialize();

            GamePads[PlayerIndex.One].OnButtonPressed += Gamepad_OnButtonPressed;
        }

        private void Gamepad_OnButtonPressed(List<GamePadButton> buttons)
        {
            if (buttons.Contains(GamePadButton.Back))
            {
                Game.Exit();
            }
        }
    }
}