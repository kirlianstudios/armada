#region

using Harmony.Components.Managers;

#endregion

namespace Harmony.Components.Initializables
{
    public class InitializableManager : Singleton<InitializableManager>, IComponentManager<IInitializable>
    {
        public InitializableManager()
        {
            Components = new ComponentCollection<Id, IInitializable>();
        }

        #region Implementation of IComponentManager<IUpdateable>

        public ComponentCollection<Id, IInitializable> Components { get; set; }

        public void Initialize()
        {
            //Components = new ComponentCollection<Id, IInitializable>();
        }

        #endregion
    }
}