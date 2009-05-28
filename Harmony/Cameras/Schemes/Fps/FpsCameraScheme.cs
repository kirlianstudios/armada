#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Harmony.Cameras.Schemes
{
    public class FpsCameraScheme : CameraScheme
    {
        private Dictionary<Keys, Vector3> Map { get; set; }

        public override void Initialize()
        {
            Map = new Dictionary<Keys, Vector3>();
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

            ActiveCamera.Translate(translation);
        }
    }
}