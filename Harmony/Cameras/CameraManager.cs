using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Cameras
{
    public sealed class CameraManager : DrawableGameComponent
    {
        internal CameraManager(Microsoft.Xna.Framework.Game a_game)
            : base(a_game)
        {

        }

        private static Camera DefaultCamera { get; set; }

        public static Camera ActiveCamera { get; private set; }

        protected override void LoadContent()
        {
            DefaultCamera = new Camera(GraphicsDevice.Viewport);

            AddCamera("HMEngine.HMCameras.DefaultCamera", DefaultCamera);

            SetActiveCamera("HMEngine.HMCameras.DefaultCamera");

            base.LoadContent();
        }

        public static void AddCamera(string a_handle, Camera a_camera)
        {
            ComponentManager.AddComponent(a_handle, a_camera);
        }
        
        public static void SetActiveCamera(string a_handle)
        {
            ActiveCamera = ComponentManager.GetComponent<Camera>(a_handle);
        }
    }
}
