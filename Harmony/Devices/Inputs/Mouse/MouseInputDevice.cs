#region

using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Harmony.Devices.Inputs
{
    public enum MouseButton
    {
        LeftButton = 0,
        MiddleButton = 1,
        RightButton = 2,
        XButton1 = 3,
        XButton2 = 4
    }

    public delegate void MouseClickedHandler(Point a_position, Collection<MouseButton> a_buttons);

    public delegate void MouseHeldHandler(Point a_position, Collection<MouseButton> a_buttons);

    public delegate void MouseReleasedHandler(Point a_position, Collection<MouseButton> a_buttons);

    public delegate void MouseMovedHandler(Vector2 a_move);

    public delegate void MouseScrolledHandler(int a_ticks);

    public sealed class MouseInputDevice : InputDevice
    {
        private Collection<PressedState> ButtonStates { get; set; }
        private bool FreezeMouse { get; set; }
        private Collection<MouseButton> Held { get; set; }
        private Point InitialMousePosition { get; set; }
        private int LastScrollPosition { get; set; }
        private MouseState MouseState { get; set; }
        private Collection<MouseButton> Pressed { get; set; }
        private Collection<ButtonState> PressedStates { get; set; }
        private Collection<MouseButton> Released { get; set; }

        public bool Freeze
        {
            get { return FreezeMouse; }
            set
            {
                FreezeMouse = value;
                if (FreezeMouse)
                {
                    MouseState = Mouse.GetState();
                }
                InitialMousePosition = new Point(MouseState.X, MouseState.Y);
            }
        }

        public event MouseClickedHandler MouseClicked;
        public event MouseHeldHandler MouseHeld;
        public event MouseReleasedHandler MouseReleased;
        public event MouseMovedHandler MouseMoved;
        public event MouseScrolledHandler MouseScrolled;

        public override void Initialize()
        {
            MouseState = Mouse.GetState();
            InitialMousePosition = new Point(MouseState.X, MouseState.Y);
            LastScrollPosition = MouseState.ScrollWheelValue;
            ButtonStates = new Collection<PressedState>();
            for (var i = 0; i < 5; i++)
            {
                ButtonStates.Add(PressedState.Idle);
            }
        }

        public override void Update(GameTime a_gameTime)
        {
            // MouseInputDevice Wheel Checks
            var scrollWheelValue = MouseState.ScrollWheelValue;
            var scrollMoved = LastScrollPosition - scrollWheelValue;
            LastScrollPosition = scrollWheelValue;

            // MouseInputDevice Move Checks
            MouseState = Mouse.GetState();
            var currentMousePosition = new Point(MouseState.X, MouseState.Y);
            var mouseMoved = new Vector2(currentMousePosition.X - InitialMousePosition.X,
                                         currentMousePosition.Y - InitialMousePosition.Y);
            if (FreezeMouse)
            {
                Mouse.SetPosition(InitialMousePosition.X, InitialMousePosition.Y);
            }
            else
            {
                InitialMousePosition = currentMousePosition;
            }

            // MouseInputDevice Buttons Checks
            Pressed = new Collection<MouseButton>();
            Held = new Collection<MouseButton>();
            Released = new Collection<MouseButton>();
            PressedStates = MousePressedStateArray(MouseState);
            for (var i = 0; i < 5; i++)
            {
                ButtonStates[i] = CheckPressedState(PressedStates[i], ButtonStates[i]);
            }

            // Loading Event Lists
            for (var i = 0; i < 5; i++)
            {
                if (ButtonStates[i] == PressedState.Pressed)
                {
                    Pressed.Add((MouseButton) i);
                }
                else if (ButtonStates[i] == PressedState.Held)
                {
                    Held.Add((MouseButton) i);
                }
                else if (ButtonStates[i] == PressedState.Released)
                {
                    Released.Add((MouseButton) i);
                }
            }

            // Event Calls
            if (scrollMoved != 0 && null != MouseScrolled)
            {
                MouseScrolled(scrollMoved);
            }
            if (mouseMoved.Length() > 0 && null != MouseMoved)
            {
                MouseMoved(mouseMoved);
            }
            if (Pressed.Count > 0 && null != MouseClicked)
            {
                MouseClicked(currentMousePosition, Pressed);
            }
            if (Held.Count > 0 && null != MouseHeld)
            {
                MouseHeld(currentMousePosition, Held);
            }
            if (Released.Count > 0 && null != MouseReleased)
            {
                MouseReleased(currentMousePosition, Released);
            }
        }

        private static Collection<ButtonState> MousePressedStateArray(MouseState mouse)
        {
            var states = new Collection<ButtonState>
                             {
                                 mouse.LeftButton,
                                 mouse.MiddleButton,
                                 mouse.RightButton,
                                 mouse.XButton1,
                                 mouse.XButton2
                             };

            return states;
        }

        public override void Dispose()
        {
        }
    }
}