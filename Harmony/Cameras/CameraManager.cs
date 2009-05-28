#region

using Harmony.Components;
using Harmony.Components.Managers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Cameras
{
    public class CameraManager : Singleton<CameraManager>, IComponentManager<ICamera>
    {
        public CameraManager()
        {
            Components = new ComponentCollection<Id, ICamera>();
        }

        private static ICamera DefaultCamera { get; set; }

        public static ICamera ActiveCamera { get; private set; }

        public static GraphicsDevice GraphicsDevice { get; set; }
        public string Path { get; set; }

        #region IComponentManager<ICamera> Members

        public Id Id { get; set; }

        public void Initialize()
        {
            DefaultCamera = new TargetCamera
                                {
                                    Viewport = Engine.Game.GraphicsDevice.Viewport,
                                    Id = new Id {Handle = "Camera.Default"}
                                };

            AddCamera(DefaultCamera);

            SetActiveCamera(DefaultCamera);
        }

        #endregion

        #region Implementation of IComponentManager<ICamera>

        public ComponentCollection<Id, ICamera> Components { get; set; }

        #endregion

        public void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager)
        {
        }

        public void UnloadContent()
        {
            SetActiveCamera(DefaultCamera);
        }

        public static void AddCamera(ICamera a_camera)
        {
            a_camera.Id = new Id {Guid = GuidManager.NewGuid()};
            ComponentManager.AddComponent(a_camera.Id, a_camera);
        }

        public static void SetActiveCamera(ICamera a_camera)
        {
            ActiveCamera = a_camera;
        }
    }
}