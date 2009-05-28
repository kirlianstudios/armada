#region

using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Harmony.Controls.Schemes.Pc
{
    public class PcControlScheme : ControlScheme
    {
        public override void Initialize()
        {
            Keyboard.KeyPress += Keyboard_KeyPress;
        }

        private void Keyboard_KeyPress(Collection<Keys> a_keys)
        {
            if (a_keys.Contains(Keys.F))
            {
                Game.ToggleFullScreen();
            }

            // Allows the game to exit
            if (a_keys.Contains(Keys.Escape))
            {
                Engine.Game.Exit();
            }
        }
    }
}