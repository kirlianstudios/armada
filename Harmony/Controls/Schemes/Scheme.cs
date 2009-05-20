using Harmony.Devices.Inputs;

namespace Harmony.Controls.Schemes
{
    public class Scheme : IScheme
    {
        public KeyboardDevice Keyboard
        {
            get { return InputManager.Keyboard; }
        }

        public MouseDevice Mouse
        {
            get { return InputManager.Mouse; }
        }

        public GamePadDeviceCollection GamePads
        {
            get { return InputManager.GamePads; }
        }

        // TODO don't think you need this

        #region IScheme Members

        public ControlManager ControlManager { get; set; }

        public virtual void Initialize()
        {
        }

        #endregion
    }
}