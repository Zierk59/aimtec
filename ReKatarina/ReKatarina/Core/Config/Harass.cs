using Aimtec;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Util.Cache;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Config
{
    internal class Harass : IConfig
    {
        public static readonly Menu Menu = new Menu("Rekatarina.Harass", "Harass");
        private static readonly Menu Whitelist = new Menu("Rekatarina.Harass.Whitelist", "Whitelist");

        public void Initialize()
        {
            // Add menu options
            Menu.AddMenuSeparator("Harass.Labels.Main", "Harass settings");
            Menu.AddMenuBool("Harass.Q.Enabled", "Use Q");

            Menu.AddMenuSeparator("Harass.Labels.Auto", "Auto Harass settings");
            Menu.AddMenuBool("Harass.Q.Auto.Enabled", "Use Q");
            Menu.AddMenuSlider("Harass.Q.Auto.Chance", "Auto harass usage chance", 10, 1);
            Menu.AddMenuSeparator("Harass.Labels.Info", "100% = always, 0% = never");

            // Add whitelist
            foreach (Obj_AI_Hero p in GameObjects.EnemyHeroes)
            {
                Whitelist.AddMenuBool($"Harass.Auto.Whitelist.{p.ChampionName}", p.ChampionName);
            }

            // Attach `local` menu to `global` menu
            Menu.Add(Whitelist);
            Base.Controller.Menu.Add(Menu);
        }
    }
}
