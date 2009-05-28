#region

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Modules.Skybox
{
    public class SkyboxModule : IModule
    {
        public string[] Textures { get; set; }

        #region IModule Members

        public Id Id { get; set; }

        public string Handle { get; set; }

        public bool Active { get; set; }

        public List<IModule> Dependencies { get; set; }

        public void Initialize()
        {
            // Skybox
            if (Textures == null)
            {
                Textures = new string[]
                               {
                                   "Textures/Skybox/white_side", // top
                                   "Textures/Skybox/white_side", // bottom
                                   "Textures/Skybox/white_side", // left
                                   "Textures/Skybox/white_side", // right
                                   "Textures/Skybox/red_side", // front
                                   "Textures/Skybox/white_side" // back
                               };
            }

            var skybox = new Objects.Skybox(Textures, Color.White);
            //skybox.Shader = EffectManager.DefaultShader;
            //GameObjectManager.AddGameObject("skybox", skybox);
        }

        #endregion
    }
}