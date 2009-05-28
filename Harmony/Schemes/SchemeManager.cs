#region

using Harmony.Components;
using Harmony.Controls.Schemes;
using Harmony.Controls.Schemes.Pc;
using Harmony.Managers;

#endregion

namespace Harmony.Schemes
{
    public class SchemeManager : Singleton<SchemeManager>, IManager<IScheme>
    {
        public void Initialize()
        {
            Components = new ComponentCollection<Id, IScheme>
                             {
                                 {new Id {Handle = "Controls.Schemes.Pc"}, new PcControlScheme()},
                                 {new Id {Handle = "Controls.Schemes.X360"}, new X360ControlScheme()}
                             };
        }

        #region Implementation of IComponent

        public Id Id { get; set; }

        #endregion

        #region Implementation of IComponentManager<IScheme>

        public ComponentCollection<Id, IScheme> Components { get; set; }

        #endregion
    }
}