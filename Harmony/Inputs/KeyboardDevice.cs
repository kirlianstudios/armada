using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Harmony.Inputs
{
    public delegate void KeyPressHandler(Collection<Keys> a_keys);

    public delegate void KeyHoldHandler(Collection<Keys> a_keys);

    public delegate void KeyReleaseHandler(Collection<Keys> a_keys);

    public sealed class KeyboardDevice : InputDevice
    {
        private Collection<Keys> Held { get; set; }
        private Collection<Keys> LastPressedKeys { get; set; }

        private KeyboardState LastState { get; set; }

        private Collection<Keys> Pressed { get; set; }
        private Collection<Keys> Released { get; set; }

        public event KeyPressHandler KeyPress;
        public event KeyHoldHandler KeyHold;
        public event KeyReleaseHandler KeyRelease;

        public override void Initialize()
        {
            LastState = Keyboard.GetState();
        }

        public override void Update(GameTime aAGameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            var currentPressedKeys = new Collection<Keys>(currentState.GetPressedKeys());
            LastPressedKeys = new Collection<Keys>(LastState.GetPressedKeys());
            LastState = currentState;

            Pressed = new Collection<Keys>();
            Held = new Collection<Keys>();
            Released = new Collection<Keys>();

            // Loading Event Lists
            foreach (Keys key in currentPressedKeys)
            {
                if (LastPressedKeys.Contains(key))
                {
                    Held.Add(key);
                }
                else
                {
                    Pressed.Add(key);
                }
            }

            foreach (Keys key in LastPressedKeys)
            {
                if (!currentPressedKeys.Contains(key))
                {
                    Released.Add(key);
                }
            }

            // Event Calls
            if (Pressed.Count > 0 && null != KeyPress)
            {
                KeyPress(Pressed);
            }

            if (Held.Count > 0 && null != KeyHold)
            {
                KeyHold(Held);
            }

            if (Released.Count > 0 && null != KeyRelease)
            {
                KeyRelease(Released);
            }
        }

        public override void Dispose()
        {
        }
    }
}