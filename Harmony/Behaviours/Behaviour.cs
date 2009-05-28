#region

using Microsoft.Xna.Framework;

#endregion

namespace Harmony.Behaviours
{
    public class Behaviour : IBehaviour
    {
        #region IBehaviour Members

        public virtual void Update(GameTime a_gameTime)
        {
        }

        public Id Id { get; set; }

        public virtual void Initialize()
        {
        }

        #endregion
    }
}