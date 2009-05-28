#region

using System;
using Harmony.Components;

#endregion

namespace Harmony
{
    public class Singleton<T> : IComponent where T : IComponent, new()
    {
        //private static readonly Mutex s_mutex = new Mutex();
        private static readonly Object s_lock = new Object();
        private static T s_instance;

        public static T Instance
        {
            get
            {
                lock (s_lock)
                {
                    if (s_instance == null)
                    {
                        s_instance = new T();
                    }
                }
                return s_instance;
            }
        }

        #region Implementation of IComponent

        private Id m_id { get; set; }

        public Id Id
        {
            get
            {
                if (m_id == null)
                {
                    m_id = new Id {Guid = GuidManager.NewGuid(), Handle = "Singleton::" + typeof (T).ToString()};
                }
                return m_id;
            }
            set { m_id = value; }
        }

        #endregion
    }
}