using Harmony.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Harmony.Devices.Inputs
{
    public sealed class InputManager : GameComponent
    {
        static InputManager()
        {
            // Inputs
            Keyboard = new KeyboardDevice {};
            AddDevice("keyboard", Keyboard);

            // Mouse
            Mouse = new MouseDevice();
            AddDevice("mouse", Mouse);

            // GamePad[s] // TODO detect controllers
            AddDevice("GamePad1", new GamePadDevice(PlayerIndex.One));
        }

        public InputManager(Microsoft.Xna.Framework.Game a_game)
            : base(a_game)
        {
        }

        static InputDeviceCollection Devices { get; set; }

        public static KeyboardDevice Keyboard { get; set; }
        public static MouseDevice Mouse { get; set; }
        public static GamePadDeviceCollection GamePads { get; set; }

        public static void AddDevice(string a_handle, InputDevice a_inputDevice)
        {
            ComponentManager.AddComponent(a_handle, a_inputDevice);

            Devices.Add(a_inputDevice);

            if (a_inputDevice is GamePadDevice)
            {
                GamePads.Add((GamePadDevice)a_inputDevice);
            }
        }
    }
}