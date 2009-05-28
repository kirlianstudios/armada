#region

using Harmony.Components;
using Harmony.Controls.Layouts;
using Harmony.Schemes;

#endregion

namespace Harmony.Controls.Schemes
{
    public abstract class ControlScheme : Scheme
    {
        public ComponentCollection<Id, IControlLayout> Layouts { get; set; }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}