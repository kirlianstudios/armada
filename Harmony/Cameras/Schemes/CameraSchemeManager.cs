#region

using Harmony.Components;
using Harmony.Components.Managers;

#endregion

namespace Harmony.Cameras.Schemes
{
    public class CameraSchemeManager : Singleton<CameraSchemeManager>, IComponentManager<CameraScheme>
    {
        public CameraSchemeManager()
        {
            Components = new ComponentCollection<Id, CameraScheme>();
        }

        #region Implementation of IComponent

        public Id Id { get; set; }

        public void Initialize()
        {
        }

        #endregion

        #region Implementation of IComponentManager<CameraScheme>

        public ComponentCollection<Id, CameraScheme> Components { get; set; }

        #endregion
    }
}