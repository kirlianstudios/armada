#region

using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Components
{
    public interface IUpdateable : IComponent
    {
        void Update(GameTime a_gameTime);
    }
}