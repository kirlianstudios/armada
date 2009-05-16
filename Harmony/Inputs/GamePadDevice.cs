using System.Collections.ObjectModel;
using Harmony.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Harmony.Inputs
{
    public enum GamePadButton
    {
        A = 0,
        B = 1,
        Back = 2,
        LeftShoulder = 3,
        LeftStick = 4,
        RightShoulder = 5,
        RightStick = 6,
        Start = 7,
        X = 8,
        Y = 9
    }

    public enum GamePadDPadDirection
    {
        Down = 0,
        Left = 1,
        Right = 2,
        Up = 3
    }

    public delegate void GamePadPressedHandler(Collection<GamePadButton> a_buttons);

    public delegate void GamePadReleasedHandler(Collection<GamePadButton> a_buttons);

    public delegate void GamePadHeldHandler(Collection<GamePadButton> a_buttons);

    public delegate void GamePadDPadPressedHandler(Collection<GamePadDPadDirection> a_directions);

    public delegate void GamePadDPadReleasedHandler(Collection<GamePadDPadDirection> a_directions);

    public delegate void GamePadDPadHeldHandler(Collection<GamePadDPadDirection> a_directions);

    public delegate void GamePadJoystickHandler(Vector2 a_leftStick, Vector2 a_rightStick);

    public sealed class GamePadDevice : InputDevice
    {
        private Collection<PressedState> ButtonStates { get; set; }
        private Collection<PressedState> DButtonStates { get; set; }

        private PlayerIndex PlayerIndex { get; set; }

        private Collection<GamePadDPadDirection> DPadHeld { get; set; }
        private Collection<GamePadDPadDirection> DPadPressed { get; set; }
        private Collection<GamePadDPadDirection> DPadReleased { get; set; }
        private Collection<ButtonState> DPressedStates { get; set; }

        private GamePadState GamePadState { get; set; }

        private Collection<GamePadButton> Held { get; set; }

        private Vector2 LeftStickMove { get; set; }
        private Vector2 RightStickMove { get; set; }

        private Collection<GamePadButton> Pressed { get; set; }
        private Collection<ButtonState> PressedStates { get; set; }
        private Collection<GamePadButton> Released { get; set; }


        public GamePadDevice(PlayerIndex a_player)
        {
            PlayerIndex = a_player;
            GamePadState = GamePad.GetState(PlayerIndex);
            ButtonStates = new Collection<PressedState>();
            DButtonStates = new Collection<PressedState>();
            for (int i = 0; i < 10; i++)
            {
                ButtonStates.Add(PressedState.Idle);
            }
            for (int i = 0; i < 4; i++)
            {
                DButtonStates.Add(PressedState.Idle);
            }
        }

        public event GamePadPressedHandler OnButtonPressed;
        public event GamePadReleasedHandler OnButtonReleased;
        public event GamePadHeldHandler OnButtonHeld;
        public event GamePadDPadPressedHandler OnDPadPressed;
        public event GamePadDPadReleasedHandler OnDPadReleased;
        public event GamePadDPadHeldHandler OnDPadHeld;
        public event GamePadJoystickHandler OnJoystickMoved;

        public override void Initialize()
        {
        }

        public override void Update(GameTime a_gameTime)
        {
            // Button checks including the DPad
            GamePadState = GamePad.GetState(PlayerIndex);
            Pressed = new Collection<GamePadButton>();
            Released = new Collection<GamePadButton>();
            Held = new Collection<GamePadButton>();
            DPadPressed = new Collection<GamePadDPadDirection>();
            DPadReleased = new Collection<GamePadDPadDirection>();
            DPadHeld = new Collection<GamePadDPadDirection>();
            PressedStates = GamePadPressedStateArray(GamePadState);
            for (int i = 0; i < 10; i++)
            {
                ButtonStates[i] = CheckPressedState(PressedStates[i], ButtonStates[i]);
            }
            DPressedStates = DPadPressedStateArray(GamePadState.DPad);
            for (int i = 0; i < 4; i++)
            {
                DButtonStates[i] = CheckPressedState(DPressedStates[i], DButtonStates[i]);
            } // Checking the Joysticks for movement
            LeftStickMove = new Vector2(GamePadState.ThumbSticks.Left.X, GamePadState.ThumbSticks.Left.Y);
            RightStickMove = new Vector2(GamePadState.ThumbSticks.Right.X, GamePadState.ThumbSticks.Right.Y);
            // Loading Event Lists
            for (int i = 0; i < 10; i++)
            {
                if (ButtonStates[i] == PressedState.Pressed)
                {
                    Pressed.Add((GamePadButton) i);
                }
                else if (ButtonStates[i] == PressedState.Held)
                {
                    Held.Add((GamePadButton) i);
                }
                else if (ButtonStates[i] == PressedState.Released)
                {
                    Released.Add((GamePadButton) i);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (DButtonStates[i] == PressedState.Pressed)
                {
                    DPadPressed.Add((GamePadDPadDirection) i);
                }
                else if (DButtonStates[i] == PressedState.Held)
                {
                    DPadHeld.Add((GamePadDPadDirection) i);
                }
                else if (DButtonStates[i] == PressedState.Released)
                {
                    DPadReleased.Add((GamePadDPadDirection) i);
                }
            } // Event Calls
            if (Pressed.Count > 0 && null != OnButtonPressed)
            {
                OnButtonPressed(Pressed);
            }
            if (Released.Count > 0 && null != OnButtonReleased)
            {
                OnButtonReleased(Released);
            }
            if (Held.Count > 0 && null != OnButtonHeld)
            {
                OnButtonHeld(Held);
            }
            if (DPadPressed.Count > 0 && null != OnDPadPressed)
            {
                OnDPadPressed(DPadPressed);
            }
            if (DPadReleased.Count > 0 && null != OnDPadReleased)
            {
                OnDPadReleased(DPadReleased);
            }
            if (DPadHeld.Count > 0 && null != OnDPadHeld)
            {
                OnDPadHeld(DPadHeld);
            }
            if (LeftStickMove.Length() + RightStickMove.Length() > 0 && null != OnJoystickMoved)
            {
                OnJoystickMoved(LeftStickMove, RightStickMove);
            }
        }

        public override void Dispose()
        {
        }

        private static Collection<ButtonState> GamePadPressedStateArray(GamePadState a_gamePad)
        {
            var states = new Collection<ButtonState>
                             {
                                 a_gamePad.Buttons.A,
                                 a_gamePad.Buttons.B,
                                 a_gamePad.Buttons.Back,
                                 a_gamePad.Buttons.LeftShoulder,
                                 a_gamePad.Buttons.LeftStick,
                                 a_gamePad.Buttons.RightShoulder,
                                 a_gamePad.Buttons.RightStick,
                                 a_gamePad.Buttons.Start,
                                 a_gamePad.Buttons.X,
                                 a_gamePad.Buttons.Y
                             };
            return states;
        }

        private static Collection<ButtonState> DPadPressedStateArray(GamePadDPad a_dPad)
        {
            var states = new Collection<ButtonState> {a_dPad.Down, a_dPad.Left, a_dPad.Right, a_dPad.Up};
            return states;
        }
    }
}