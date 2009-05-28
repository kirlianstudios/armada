#region

using Harmony.Components;

#endregion

namespace Harmony.Schemes
{
    public interface IScheme : IInitializable
    {
        Game Game { get; }
    }
}