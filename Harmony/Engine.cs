#region

using System;
using Harmony.Managers;

#endregion

namespace Harmony
{
    public class Engine : IDisposable
    {
        public static Game Game { get; set; }

        //public Dictionary<Type, IComponentManager<IComponent>> ComponentManagers { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        public void Initialize()
        {
            ManagerManager.Instance.Initialize();
            Game.Run();
            //Harmony.Engine.Game = new ArmadaGame();
        }
    }
}