#region

using Harmony.Components;
using Harmony.Managers;

#endregion

namespace Harmony.Objects
{
    public class GameObjectManager : Singleton<GameObjectManager>, IManager<GameObject>
    {
        public static void AddGameObject(Id a_id, GameObject a_gameObject)
        {
            //var id = new Id {Handle = a_handle, Guid = GuidManager.NewGuid()};
            Instance.Components.Add(a_id, a_gameObject);
        }

        #region Implementation of IComponent

        public Id Id { get; set; }

        public void Initialize()
        {
        }

        #endregion

        #region Implementation of IComponentManager<GameObject>

        public ComponentCollection<Id, GameObject> Components { get; set; }

        #endregion
    }
}