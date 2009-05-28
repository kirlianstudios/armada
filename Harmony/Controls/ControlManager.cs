#region

using Harmony.Components;
using Harmony.Components.Managers;

#endregion

namespace Harmony.Controls
{
    public class ControlManager : Singleton<ControlManager>, IComponentManager<IComponent>
    {
        #region IComponentManager<IComponent> Members

        public void Initialize()
        {
        }

        #endregion

        #region Implementation of IComponent

        public Id Id { get; set; }

        #endregion

        #region Implementation of IComponentManager<IComponent>

        public ComponentCollection<Id, IComponent> Components { get; set; }

        #endregion
    }
}