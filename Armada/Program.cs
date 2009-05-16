using System;

namespace Armada
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ArmadaGame game = new ArmadaGame())
            {
                game.Run();
            }
        }
    }
}

