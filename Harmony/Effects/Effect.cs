#region

using Harmony.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Effects
{
    public delegate void EffectParameterHandler(Microsoft.Xna.Framework.Graphics.Effect a_effect, IDrawable a_drawable);

    public class Effect : ILoadable
    {
        protected Effect(string a_asset)
        {
            Asset = a_asset;
            LoadContent(Engine.Game.GraphicsDevice, Engine.Game.Content);
        }

        private string Asset { get; set; }

        public Microsoft.Xna.Framework.Graphics.Effect BackingEffect { get; set; }

        #region ILoadable Members

        public Id Id { get; set; }

        public virtual void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager)
        {
            BackingEffect = a_contentManager.Load<Microsoft.Xna.Framework.Graphics.Effect>(Path + Asset);
        }

        public virtual void UnloadContent()
        {
        }

        public string Path { get; set; }

        #endregion

        public event EffectParameterHandler SetUserParameters;

        public virtual void SetParameters(IDrawable a_renderable)
        {
            if (SetUserParameters != null)
            {
                SetUserParameters(BackingEffect, a_renderable);
            }

            BackingEffect.CommitChanges();
        }
    }
}