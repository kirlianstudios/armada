using System.Collections.ObjectModel;
using Harmony.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Harmony.Inputs
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

    public sealed class MouseDevice : InputDevice
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

        public event MouseClickedHandler OnMouseClicked;
        public event MouseHeldHandler OnMouseHeld;
        public event MouseReleasedHandler OnMouseReleased;
        public event MouseMovedHandler OnMouseMoved;
        public event MouseScrolledHandler OnMouseScrolled;

        public override void Initialize()
        {
            MouseState = Mouse.GetState();
            InitialMousePosition = new Point(MouseState.X, MouseState.Y);
            LastScrollPosition = MouseState.ScrollWheelValue;
            ButtonStates = new Collection<PressedState>();
            for (int i = 0; i < 5; i++)
            {
                ButtonStates.Add(PressedState.Idle);
            }
        }

        public override void Update(GameTime a_gameTime)
        {
            // Mouse Wheel Checks
            int scrollWheelValue = MouseState.ScrollWheelValue;
            int scrollMoved = LastScrollPosition - scrollWheelValue;
            LastScrollPosition = scrollWheelValue;

            // Mouse Move Checks
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

            // Mouse Buttons Checks
            Pressed = new Collection<MouseButton>();
            Held = new Collection<MouseButton>();
            Released = new Collection<MouseButton>();
            PressedStates = MousePressedStateArray(MouseState);
            for (int i = 0; i < 5; i++)
            {
                ButtonStates[i] = CheckPressedState(PressedStates[i], ButtonStates[i]);
            }

            // Loading Event Lists
            for (int i = 0; i < 5; i++)
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
            if (scrollMoved != 0 && null != OnMouseScrolled)
            {
                OnMouseScrolled(scrollMoved);
            }
            if (mouseMoved.Length() > 0 && null != OnMouseMoved)
            {
                OnMouseMoved(mouseMoved);
            }
            if (Pressed.Count > 0 && null != OnMouseClicked)
            {
                OnMouseClicked(currentMousePosition, Pressed);
            }
            if (Held.Count > 0 && null != OnMouseHeld)
            {
                OnMouseHeld(currentMousePosition, Held);
            }
            if (Released.Count > 0 && null != OnMouseReleased)
            {
                OnMouseReleased(currentMousePosition, Released);
            }
        }

        private static Collection<ButtonState> MousePressedStateArray(MouseState mouse)
        {
            var states = new Collection<ButtonState>
                             {mouse.LeftButton, mouse.MiddleButton, mouse.RightButton, mouse.XButton1, mouse.XButton2};
            return states;
        }

        public override void Dispose()
        {
        }
    }
}