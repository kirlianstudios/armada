#region

using System.Collections.Generic;
using Harmony.Components;
using Harmony.Components.Managers;
using Harmony.Devices.Inputs.GamePad;
using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Devices.Inputs
{
    public class InputDeviceManager : Singleton<InputDeviceManager>, IComponentManager<IInputDevice>
    {
        public Dictionary<PlayerIndex, GamePadInputDevice> GamePads { get; set; }
        public KeyboardInputDevice Keyboard { get; set; }
        public MouseInputDevice MouseInputDevice { get; set; }

        #region IComponentManager<IInputDevice> Members

        public void Initialize()
        {
            // Inputs
            Keyboard = new KeyboardInputDevice();
            AddDevice("Devices.Inputs.Keyboard", Keyboard);

            // MouseInputDevice
            MouseInputDevice = new MouseInputDevice();
            AddDevice("Devices.Inputs.MouseInputDevice", MouseInputDevice);

            // GamePadInputDevice[s] // TODO detect controllers
            AddDevice("Devices.Inputs.GamePads.One", new GamePadInputDevice {PlayerIndex = PlayerIndex.One});
        }

        #endregion

        public static void AddDevice(string a_handle, InputDevice a_inputDevice)
        {
            var id = new Id {Guid = GuidManager.NewGuid(), Handle = a_handle};
            Instance.Components.Add(id, a_inputDevice);

            if (a_inputDevice is GamePadInputDevice)
            {
                ((InputDeviceManager) Instance).GamePads.Add(((GamePadInputDevice) a_inputDevice).PlayerIndex,
                                                             (GamePadInputDevice) a_inputDevice);
            }

            a_inputDevice.Initialize();
        }

        #region Implementation of IComponent

        public Id Id { get; set; }

        #endregion

        #region Implementation of IComponentManager<IInputDevice>

        public ComponentCollection<Id, IInputDevice> Components { get; set; }

        #endregion
    }
}