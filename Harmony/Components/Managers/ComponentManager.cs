#region

using System;
using Harmony.Components.Drawables;
using Harmony.Components.Initializables;
using Harmony.Components.Loadables;
using Harmony.Components.Updateables;
using Harmony.Effects;
using Harmony.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Components.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentManager : Singleton<ComponentManager>, IComponentManager<IComponent>, IComponent
    {
        public ComponentManager()
        {
            var component = new SubscribeableDrawableGameComponent(Engine.Game);
            Engine.Game.Components.Add(component);
        }

        #region IComponentManager<IComponent> Members

        public ComponentCollection<Id, IComponent> Components { get; set; }

        public void Initialize()
        {
        }

        #endregion

        public static T GetComponent<T>(object a_object) where T : IComponent
        {
            var id = new Id();
            //{Handle = string.Empty, Guid = GuidManager.NewGuid()};
            if (a_object is string)
            {
                id.Handle = (string) a_object;
            }
            if (a_object is Guid)
            {
                id.Guid = (Guid) a_object;
            }

            if (ManagerManager.Map.ContainsKey(typeof (T)))
            {
                var type = typeof (T);

                if (ManagerManager.Map[type] is IComponentManager<T>)
                {
                    var manager = (IComponentManager<T>) ManagerManager.Map[typeof (T)];

                    if (manager.Components.ContainsKey(id))
                    {
                        return manager.Components[id];
                    }
                }
            }

            throw new ComponentException("No component with the handle " + a_object + " exists");
        }

        public static void AddComponent<T>(T a_component) where T : IComponent
        {
            // TODO check for null
            AddComponent(a_component.Id, a_component);
        }

        public static void AddComponent<T>(Id a_id, T a_component) where T : IComponent
        {
            Instance.Components.Add(a_id, a_component);

            foreach (Type type in ManagerManager.Map.Keys)
            {
                if (type.IsInstanceOfType(a_component))
                {
                    ((IComponentManager<T>) ManagerManager.Map[type]).Components[a_id] = a_component;
                }
            }
        }

        public static void Draw(GameTime a_gameTime)
        {
            foreach (IDrawable drawable in DrawableManager.Instance.Components.Values)
            {
                //drawable.Draw(Instance.Game.GraphicsDevice);
                Engine.Game.GraphicsDevice.VertexDeclaration = drawable.VertexDeclaration;

                EffectManager.ActiveShader = EffectManager.DefaultShader;
                EffectManager.ActiveShader.SetParameters(drawable);

                EffectManager.ActiveShader.BackingEffect.Begin();
                foreach (EffectPass pass in EffectManager.ActiveShader.BackingEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    drawable.Draw(Engine.Game.GraphicsDevice);
                    pass.End();
                }
                EffectManager.ActiveShader.BackingEffect.End();
            }
        }

        public static void Update(GameTime a_gameTime)
        {
            foreach (IUpdateable updateable in UpdateableManager.Instance.Components.Values)
            {
                updateable.Update(a_gameTime);
            }
        }

        public static void LoadContent()
        {
            foreach (ILoadable component in LoadableManager.Instance.Components.Values)
            {
                var loadable = component;
                loadable.LoadContent(Engine.Game.GraphicsDevice, Engine.Game.Content);
            }
        }

        public static void UnloadContent()
        {
            foreach (ILoadable component in LoadableManager.Instance.Components.Values)
            {
                var loadable = component;
                loadable.UnloadContent();
            }
        }

        public void InitializeHook()
        {
            foreach (ILoadable component in InitializableManager.Instance.Components.Values)
            {
                var loadable = component;
                loadable.UnloadContent();
            }
        }
    }
}