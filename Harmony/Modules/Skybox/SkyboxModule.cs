using System;
using System.Collections.Generic;
using Harmony.Components;
using Harmony.Effects;
using Harmony.Objects;
using Microsoft.Xna.Framework.Graphics;
using Model=Harmony.Objects.Model;

namespace Harmony.Modules.Skybox
{
    public class SkyboxModule : IModule
    {
        #region IModule Members

        public string Handle { get; set; }

        public bool Active { get; set; }

        public List<IModule> Dependencies { get; set; }

        public string[] Textures { get; set; }

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
            skybox.Shader = "TCT";
            GameObjectManager.AddGameObject("skybox", skybox);
        }

        #endregion
    }
}