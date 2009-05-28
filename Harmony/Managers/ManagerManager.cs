#region

using System;
using System.Collections.Generic;
using Harmony.Components;
using Harmony.Components.Managers;

#endregion

namespace Harmony.Managers
{
    public class ManagerManager : Singleton<ManagerManager>, IComponentManager<IComponent>
    {
        public ManagerManager()
        {
            Map = new Dictionary<Type, ComponentCollection<Id, IComponentManager>>();
            Components = new ComponentCollection<Id, IComponent>();
        }

        public static Dictionary<Type, ComponentCollection<Id, IComponentManager>> Map { get; set; }

        #region IComponentManager<IComponent> Members

        public void Initialize()
        {
            Subscribe(ComponentManager.Instance, typeof (IComponent));

            Subscribe(Instance, typeof (IComponentManager<IComponent>));

            //// Drawables
            //Map[typeof (IDrawable)] = DrawableManager.Instance;
            //DrawableManager.Instance.Initialize();

            //// Updateables
            //Map[typeof (IUpdateable)] = UpdateableManager.Instance;
            //UpdateableManager.Instance.Initialize();

            //// Loadables
            //Map[typeof (IUpdateable)] = UpdateableManager.Instance;
            //LoadableManager.Instance.Initialize();

            //// Objects
            ////Map[typeof(IUpdateable)] = UpdateableManager.Instance;
            ////LoadableManager.Instance.Initialize();

            //// Models
            //Map[typeof (Model)] = ModelManager.Instance;
            //ModelManager.Instance.Initialize();

            //// Cameras
            //Map[typeof (ICamera)] = CameraManager.Instance;
            ////CameraManager.Instance.Initialize();
        }

        #endregion

        #region Implementation of IComponentManager<IComponentManager>

        public ComponentCollection<Id, IComponent> Components { get; set; }

        #endregion

        public static void Subscribe(IComponentManager<IComponent> a_manager, List<Type> a_types)
        {
            foreach (Type type in a_types)
            {
                Subscribe(a_manager, type);
            }
        }

        public static void Subscribe(IComponentManager<IComponent> a_manager, Type a_type)
        {
            if (!Map.ContainsKey(a_type))
            {
                Map[a_type] = new ComponentCollection<Id, IComponentManager>();
            }
            Map[a_type].Add(a_manager.Id, a_manager);
        }
    }
}