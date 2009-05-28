#region

using Harmony.Components;
using Harmony.Components.Managers;

#endregion

namespace Harmony.Devices.Inputs.GamePad
{
    internal class GamePadManager : Singleton<GamePadManager>, IComponentManager<GamePadInputDevice>
    {
        #region Implementation of IComponent

        public Id Id { get; set; }

        public void Initialize()
        {
        }

        #endregion

        #region Implementation of IComponentManager<GamePadInputDevice>

        public ComponentCollection<Id, GamePadInputDevice> Components { get; set; }

        #endregion
    }
}