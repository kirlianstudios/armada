using Harmony.Components;
using Microsoft.Xna.Framework;

namespace Harmony.Inputs
{
    public sealed class InputManager : GameComponent
    {
        internal InputManager(Game game) : base(game)
        {
        }

        public static void AddDevice(string name, InputDevice device)
        {
            ComponentManager.AddComponent(name, device);
        }
    }
}