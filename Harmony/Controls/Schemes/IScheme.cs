using Harmony.Components;

namespace Harmony.Controls.Schemes
{
    public interface IScheme : IInitializable
    {
        ControlManager ControlManager { get; set; }
    }
}