using Harmony.Controls.Schemes;
using Harmony.Devices.Inputs;
using Microsoft.Xna.Framework;

namespace Harmony.Controls
{
    public class ControlManager : GameComponent
    {
        public ControlManager(Microsoft.Xna.Framework.Game a_game)
            : base(a_game)
        {
            Game = a_game;
            SchemeManager = new SchemeManager(a_game);
            Game.Components.Add(this);
        }

        public InputManager InputManager { get; set; }

        public SchemeManager SchemeManager { get; set; }

        public Microsoft.Xna.Framework.Game Game { get; set; }

        public override void Initialize()
        {
        }
    }
}