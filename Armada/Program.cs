#region

using Harmony;

#endregion

namespace Armada
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            using (var engine = new Engine())
            {
                var game = new ArmadaGame();
                Engine.Game = game;
                engine.Initialize();
            }
        }
    }
}