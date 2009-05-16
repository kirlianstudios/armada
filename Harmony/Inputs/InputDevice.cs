using System;
using Harmony.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using IUpdateable=Harmony.Components.IUpdateable;

namespace Harmony.Inputs
{
    public enum PressedState
    {
        Pressed,
        Released,
        Held,
        Idle
    }

    public abstract class InputDevice : IUpdateable, IInitializable, IDisposable
    {
        #region IDisposable Members

        public abstract void Dispose();

        #endregion

        #region IInitializable Members

        public abstract void Initialize();

        #endregion

        #region IUpdateable Members

        public abstract void Update(GameTime a_ameTime);

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