#region

using Harmony.Components.Managers;

#endregion

namespace Harmony.Components.Updateables
{
    public class UpdateableManager : Singleton<UpdateableManager>, IComponentManager<IUpdateable>
    {
        public UpdateableManager()
        {
            Components = new ComponentCollection<Id, IUpdateable>();
        }

        #region Implementation of IComponentManager<IUpdateable>

        public void Initialize()
        {
        }

        public ComponentCollection<Id, IUpdateable> Components { get; set; }

        #endregion
    }
}