#region

using Harmony.Components;
using Harmony.Managers;

#endregion

namespace Harmony.Objects
{
    public class ModelManager : Singleton<ModelManager>, IManager<Model>
    {
        public ModelManager()
        {
            Components = new ComponentCollection<Id, Model>();
        }

        public static void AddModel(Id a_id, Model a_model)
        {
            Instance.Components.Add(a_id, a_model);
        }

        #region Implementation of IComponentManager<Model>

        public ComponentCollection<Id, Model> Components { get; set; }

        public void Initialize()
        {
        }

        #endregion
    }
}