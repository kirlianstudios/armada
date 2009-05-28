namespace Harmony.Components.Managers
{
    public interface IComponentManager : IInitializable
    {
        //ComponentCollection<Id, IComponent> Components { get; set; }
    }

    public interface IComponentManager<T> : IComponentManager where T : IComponent
    {
        ComponentCollection<Id, T> Components { get; set; }
    }
}