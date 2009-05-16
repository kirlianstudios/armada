using Harmony.Components;
using Microsoft.Xna.Framework;

namespace Harmony.Objects
{
    public sealed class GameObjectManager : GameComponent
    {
        internal GameObjectManager(Microsoft.Xna.Framework.Game a_game)
            : base(a_game)
        {
        }

        public static void AddGameObject(string a_handle, GameObject a_gameObject)
        {
            ComponentManager.AddComponent(a_handle, a_gameObject);
        }
    }
}