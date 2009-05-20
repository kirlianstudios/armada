using System.Collections.Generic;
using System.Collections.ObjectModel;
using Harmony.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Harmony.Controls.Schemes
{
    public class FpsCameraScheme : Scheme
    {
        private Dictionary<Keys, Vector3> Map { get; set; }

        public override void Initialize()
        {
            // Map keys to translations
            Map[Keys.W] = new Vector3(0, 0, -1);
            Map[Keys.S] = new Vector3(0, 0, 1);
            Map[Keys.A] = new Vector3(-1, 0, 0);
            Map[Keys.D] = new Vector3(1, 0, 0);

            Keyboard.KeyHold += Keyboard_KeyHold;
        }

        private void Keyboard_KeyHold(Collection<Keys> a_keys)
        {
            var translation = new Vector3();

            foreach (Keys key in Map.Keys)
            {
                if (a_keys.Contains(key))
                {
                    translation += Map[key];
                }
            }

            CameraManager.ActiveCamera.Translate(translation);
        }
    }
}