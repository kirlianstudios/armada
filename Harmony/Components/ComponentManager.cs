using System;
using System.Collections.Generic;
using Harmony.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.Reflection.Cache;
using System.Reflection.Emit;

namespace Harmony.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentManager : DrawableGameComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManager"/> class.
        /// </summary>
        /// <param name="a_game">The a_game.</param>
        public ComponentManager(Game a_game) : base(a_game)
        {
            Components = new ComponentCollection<IComponent>();

            Renderables = new ComponentCollection<IRenderable>();
            Loadables = new ComponentCollection<ILoadable>();
            Updateables = new ComponentCollection<IUpdateable>();
            Initializables = new ComponentCollection<IInitializable>();

            Pickables = new ComponentCollection<IPickable>();

            PostProcessables = new ComponentCollection<IPostProcessable>();

            Disposables = new ComponentCollection<IDisposable>();
        }

        public static ComponentCollection<IComponent> Components { get; private set; }

        public static ComponentCollection<IRenderable> Renderables { get; private set; }
        public static ComponentCollection<ILoadable> Loadables { get; private set; }
        public static ComponentCollection<IUpdateable> Updateables { get; private set; }
        public static ComponentCollection<IInitializable> Initializables { get; private set; }
        public static ComponentCollection<IPostProcessable> PostProcessables { get; private set; }
        public static ComponentCollection<IPickable> Pickables { get; private set; }

        public static ComponentCollection<IDisposable> Disposables { get; private set; }

        public static List<IPickable> Picked { get; private set; }

        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a_handle">The a_handle.</param>
        /// <returns></returns>
        internal static T GetComponent<T>(string a_handle)
        {
            if (Components.ContainsKey(a_handle) && Components[a_handle] is T)
            {
                return (T) Components[a_handle];
            }

            throw new ComponentException("No component with the name " + a_handle + " exists");
        }

        internal static T GetComponent<T>(Guid a_guid)
        {
            if (Components.ContainsKey(a_guid) && Components[a_guid] is T)
            {
                return (T)Components[a_guid];
            }

            throw new ComponentException("No component with the GUID " + a_guid + " exists");
        }

        public static void AddComponent(Object a_handle, IComponent a_component)
        {
            if (a_component is IComponent)
            {
                Components.Add(a_handle, a_component);
            }

            if (a_component is IRenderable)
            {
                Renderables.Add(a_handle, (IRenderable) a_component);
            }

            if (a_component is ILoadable)
            {
                Loadables.Add(a_handle, (ILoadable) a_component);
            }

            if (a_component is IUpdateable)
            {
                Updateables.Add(a_handle, (IUpdateable) a_component);
            }

            if (a_component is IInitializable)
            {
                Initializables.Add(a_handle, (IInitializable) a_component);
            }

            if (a_component is IDisposable)
            {
                Disposables.Add(a_handle, (IDisposable) a_component);
            }

            if (a_component is IPostProcessable)
            {
                PostProcessables.Add(a_handle, (IPostProcessable) a_component);
            }

            if (a_component is IPickable)
            {
                Pickables.Add(a_handle, (IPickable) a_component);
            }
        }

        public override void Update(GameTime a_gameTime)
        {
            foreach (IUpdateable updateable in Updateables.Values)
            {
                updateable.Update(a_gameTime);
            }

            base.Update(a_gameTime);
        }

        public override void Draw(GameTime a_gameTime)
        {
            foreach (IRenderable renderable in Renderables.Values)
            {
                GraphicsDevice.VertexDeclaration = renderable.VertexDeclaration;

                EffectManager.SetActiveShader(renderable.Shader);
                EffectManager.ActiveShader.SetParameters(renderable);

                EffectManager.ActiveShader.BackingEffect.Begin();
                foreach (EffectPass pass in EffectManager.ActiveShader.BackingEffect.CurrentTechnique.Passes)
                {
                    pass.Begin();
                    renderable.Render(GraphicsDevice);
                    pass.End();
                }
                EffectManager.ActiveShader.BackingEffect.End();
            }

            foreach (IPostProcessable postProcessable in PostProcessables.Values)
            {
                postProcessable.PostProcess(GraphicsDevice);
            }

            base.Draw(a_gameTime);
        }

        protected override void LoadContent()
        {
            foreach (ILoadable loadable in Loadables.Values)
            {
                loadable.LoadContent(GraphicsDevice, Game.Content);
            }

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            foreach (ILoadable loadable in Loadables.Values)
            {
                loadable.UnloadContent();
            }

            base.UnloadContent();
        }

        public override void Initialize()
        {
            foreach (IInitializable initializable in Initializables.Values)
            {
                initializable.Initialize();
            }

            base.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            foreach (IDisposable disposable in Disposables.Values)
            {
                disposable.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}