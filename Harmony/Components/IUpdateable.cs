using Microsoft.Xna.Framework;

namespace Harmony.Components
{
    public interface IUpdateable : IComponent
    {
        void Update(GameTime a_gameTime);
    }
}