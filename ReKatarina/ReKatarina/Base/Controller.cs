using Aimtec.SDK.Menu;
using ReKatarina.Extensions;
using System.Collections.Generic;
using ReKatarina.Base.Interfaces;

namespace ReKatarina.Base
{
    internal static class Controller
    {
        // Add global menu
        public static readonly Menu Menu = new Menu("Rekatarina.Addon", "ReKatarina", true);

        private static readonly List<IMode> Modes = new List<IMode>()
        {
            new Core.Modes.Combo(),
            new Core.Modes.Harass(),
            new Core.Modes.LaneClear(),
            new Core.Modes.JungleClear(),
            new Core.Modes.LastHit(),
            new Core.Modes.PermaActive(),
            new Core.Modes.WallJump()
        };

        private static readonly List<IConfig> Configs = new List<IConfig>()
        {
            new Core.Config.Combo(),
            new Core.Config.Drawing(),
            new Core.Config.Farm(),
            new Core.Config.Harass(),
            new Core.Config.Misc(),
            new Core.Config.WallJump()
        };

        // Controller initializer
        public static void Initialize()
        {
            // Implement orbwalker
            ReKatarina.Orbwalker = Aimtec.SDK.Orbwalking.Orbwalker.Implementation;

            Configs.ForEach(delegate (IConfig c)
            {
                c.Initialize();
            });

            Modes.ForEach(delegate (IMode m)
            {
                m.Initialize();
            });

            // Attach menu
            ReKatarina.Orbwalker.Attach(Menu);
            Menu.Attach();

            // Add version info
            Menu.AddMenuSeparator("Rekatarina.Version", $"Version {Other.Version.AssVersion}");

            Logger.Print("Controller initialized");
        }

        // Script main tick
        public static void Update()
        {
            Modes.ForEach(delegate (IMode m)
            {
                if (m.ShouldGetExecuted())
                {
                    m.Execute();
                }
            });
        }

        // Drawings managment
        public static void Draw()
        {
            Modes.ForEach(delegate (IMode m)
            {
                m.Draw();
            });
        }
    }
}
