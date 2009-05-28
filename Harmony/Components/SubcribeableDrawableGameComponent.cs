#region

using Harmony.Components.Managers;
using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Components
{
    internal class SubscribeableDrawableGameComponent : DrawableGameComponent
    {
        public SubscribeableDrawableGameComponent(Microsoft.Xna.Framework.Game game) : base(game)
        {
        }

        protected override void UnloadContent()
        {
            ComponentManager.UnloadContent();
            base.UnloadContent();
        }

        protected override void LoadContent()
        {
            ComponentManager.LoadContent();
            base.LoadContent();
        }

        public override void Update(GameTime a_gameTime)
        {
            ComponentManager.Update(a_gameTime);
            base.Update(a_gameTime);
        }

        public override void Draw(GameTime a_gameTime)
        {
            ComponentManager.Draw(a_gameTime);
            base.Draw(a_gameTime);
        }

        public override void Initialize()
        {
            ComponentManager.Instance.InitializeHook();
            base.Initialize();
        }
    }
}