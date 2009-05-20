using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Harmony.Controls.Schemes
{
    public class SchemeManager : GameComponent
    {
        public SchemeManager(Microsoft.Xna.Framework.Game a_game)
            : base(a_game)
        {
            Game = a_game;
        }

        public override void Initialize()
        {
            ActiveSchemes.Add(new PcScheme());
        }

        public static SchemeCollection ActiveSchemes { get; set; }

        public static Microsoft.Xna.Framework.Game Game { get; set; }
    }
}
