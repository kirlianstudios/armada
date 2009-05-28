#region

using Harmony.Components.Managers;

#endregion

namespace Harmony.Components.Loadables
{
    public class LoadableManager : Singleton<LoadableManager>, IComponentManager<ILoadable>
    {
        #region Implementation of IComponentManager<ILoadable>

        public LoadableManager()
        {
            Components = new ComponentCollection<Id, ILoadable>();
        }

        public void Initialize()
        {
        }

        public ComponentCollection<Id, ILoadable> Components { get; set; }

        #endregion
    }
}