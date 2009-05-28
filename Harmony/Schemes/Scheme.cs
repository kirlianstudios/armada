#region

using System.Collections.Generic;
using Harmony.Devices.Inputs;
using Harmony.Devices.Inputs.GamePad;
using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Schemes
{
    public class Scheme : IScheme
    {
        public KeyboardInputDevice Keyboard
        {
            get { return InputDeviceManager.Instance.Keyboard; }
        }

        public MouseInputDevice MouseInputDevice
        {
            get { return InputDeviceManager.Instance.MouseInputDevice; }
        }

        public Dictionary<PlayerIndex, GamePadInputDevice> GamePads
        {
            get { return InputDeviceManager.Instance.GamePads; }
        }

        #region IScheme Members

        public virtual void Initialize()
        {
        }

        public Game Game
        {
            get { return Engine.Game; }
        }

        public Id Id { get; set; }

        #endregion
    }
}