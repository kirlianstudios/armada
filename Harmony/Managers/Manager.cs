using System;
using Harmony.Components;

namespace Harmony.Managers
{
    public class ManagerManager<TComponent> : Singleton<ManagerManager>, IManager<TComponent> where TComponent : IComponent where TManager : Manager<TManager, TComponent>
    {
        public Id Id { get; set; }

        public Manager()
        {
            
        }

        #region IManager<T> Members

        public virtual void Initialize()
        {
            Components = new ComponentCollection<Id, T>();
        }

        public Game Game
        {
            get { return Engine.Game; }
        }

        public ComponentCollection<Id, T> Components { get; set; }

        #endregion
    }
}