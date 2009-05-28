#region

using Harmony.Components.Managers;

#endregion

namespace Harmony.Components.Drawables
{
    public class DrawableManager : Singleton<DrawableManager>, IComponentManager<IDrawable>
    {
        public DrawableManager()
        {
            Components = new ComponentCollection<Id, IDrawable>();
        }

        #region IComponentManager<IDrawable> Members

        public void Initialize()
        {
            Components = new ComponentCollection<Id, IDrawable>();
        }

        public ComponentCollection<Id, IDrawable> Components { get; set; }

        #endregion

        public static void AddDrawable(Id a_id, IDrawable a_drawable)
        {
            Instance.Components.Add(a_id, a_drawable);
        }
    }
}