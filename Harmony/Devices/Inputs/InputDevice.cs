#region

using System.Collections.Generic;
using Harmony.Devices.Inputs.GamePad;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Harmony.Devices.Inputs
{
    public enum PressedState
    {
        Pressed,
        Released,
        Held,
        Idle
    }

    public abstract class InputDevice : IInputDevice
    {
        public Dictionary<PlayerIndex, GamePadInputDevice> GamePads { get; set; }
        public KeyboardInputDevice Keyboard { get; set; }
        public MouseInputDevice MouseInputDevice { get; set; }

        #region IInputDevice Members

        public Id Id { get; set; }

        public abstract void Dispose();

        public abstract void Initialize();

        public abstract void Update(GameTime a_gameTime);

        #endregion

        protected static PressedState CheckPressedState(ButtonState a_currentState, PressedState a_lastState)
        {
            if (a_currentState == ButtonState.Pressed)
            {
                if (a_lastState == PressedState.Pressed || a_lastState == PressedState.Held)
                {
                    return PressedState.Held;
                }

                return PressedState.Pressed;
            }

            if (a_lastState != PressedState.Released && a_lastState != PressedState.Idle)
            {
                return PressedState.Released;
            }

            return PressedState.Idle;
        }
    }
}