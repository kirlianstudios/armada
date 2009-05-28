#region

using Harmony.Components;
using Harmony.Components.Managers;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Effects
{
    public class EffectManager : Singleton<EffectManager>, IComponentManager<Effect>
    {
        public EffectManager()
        {
            var basicShader = new Shader("Shaders/Basic");
            AddEffect("Harmony.Effects.Basic", basicShader);


            var tctShader = new Shader("Shaders/TransformColorTexture");
            AddEffect("Harmony.Effects.TransformColorTexture", tctShader);

            DefaultShader = basicShader;

            ActiveShader = DefaultShader;
        }

        public static Texture2D ScreenTexture { get; set; }

        public static Shader DefaultShader { get; set; }
        public static Shader ActiveShader { get; set; }

        #region IComponentManager<Effect> Members

        public void Initialize()
        {
        }

        #endregion

        public static void AddEffect(string a_handle, Effect a_effect)
        {
            var id = new Id {Guid = GuidManager.NewGuid(), Handle = string.Empty};
            Instance.Components.Add(id, a_effect);
        }

        #region Implementation of IComponent

        public Id Id { get; set; }

        #endregion

        #region Implementation of IComponentManager<Effect>

        public ComponentCollection<Id, Effect> Components { get; set; }

        #endregion
    }
}