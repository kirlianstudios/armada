#region

using Harmony.Components;
using Harmony.Schemes;

#endregion

namespace Harmony.Cameras.Schemes
{
    public class CameraScheme : Scheme
    {
        public CameraScheme()
        {
            Cameras = new ComponentCollection<Id, ICamera>();
        }

        public ComponentCollection<Id, ICamera> Cameras { get; set; }

        public ICamera ActiveCamera { get; set; }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void AttachCamera(ICamera a_camera)
        {
            if (ActiveCamera == null)
            {
                ActiveCamera = a_camera;
            }
            Cameras.Add(a_camera.Id, a_camera);
        }
    }
}