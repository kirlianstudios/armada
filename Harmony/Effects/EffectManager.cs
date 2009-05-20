using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Harmony.Effects
{
    public sealed class EffectManager : DrawableGameComponent, IInitializable
    {
        public static Texture2D ScreenTexture { get; internal set; }

        public EffectManager(Microsoft.Xna.Framework.Game a_game)
            : base(a_game)
        {

        }

        public override void Initialize()
        {
            AddEffect("Harmony.Effects.BasicShader", new Shader("Shaders/BasicShader"));

            AddEffect("Harmony.Effects.TransformColorTexture", new Shader("Shaders/TransformColorTexture"));

            base.Initialize();
        }

        public static Shader ActiveShader { get; private set; }
        
        public static void AddEffect(string a_handle, Effect a_effect)
        {
            ComponentManager.AddComponent(a_handle, a_effect);
        }
        
        internal static void SetActiveShader(string a_handle)
        {
            ActiveShader = null == a_handle
                ? ComponentManager.GetComponent<Shader>("Harmony.Effects.BasicShader")
                : ComponentManager.GetComponent<Shader>(a_handle);
        }
    }
}
