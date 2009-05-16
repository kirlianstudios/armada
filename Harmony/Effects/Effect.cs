using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Harmony.Components;
using Harmony.Effects;
using Harmony.Objects;

namespace Harmony.Effects
{
    public delegate void EffectParameterHandler(Microsoft.Xna.Framework.Graphics.Effect a_effect, IRenderable a_renderable);

    public class Effect : ILoadable
    {
        private string Asset { get; set; }

        public event EffectParameterHandler SetUserParameters;
        
        internal Microsoft.Xna.Framework.Graphics.Effect BackingEffect { get; private set; }
        
        protected Effect(string a_asset)
        {
            Asset = a_asset;
        }
        
        public virtual void SetParameters(IRenderable a_renderable)
        {
            if (SetUserParameters != null)
            {
                SetUserParameters(BackingEffect, a_renderable);
            }
            
            BackingEffect.CommitChanges();
        }
        
        public virtual void LoadContent(GraphicsDevice a_graphicsDevice, ContentManager a_contentManager)
        {
            BackingEffect = a_contentManager.Load<Microsoft.Xna.Framework.Graphics.Effect>(Asset);
        }
        
        public virtual void UnloadContent()
        {
            
        }
    }
}
