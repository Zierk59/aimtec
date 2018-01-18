using Aimtec.SDK.Menu;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Config
{
    internal class Farm : IConfig
    {
        public static readonly Menu Menu = new Menu("Rekatarina.Farm", "Farm");

        public void Initialize()
        {
            // Add menu options
            Menu.AddMenuSeparator("Farm.Labels.Main", "Farm settings");
            Menu.AddMenuBool("Farm.Q.Enabled", "Use Q");
            Menu.AddMenuBool("Farm.W.Enabled", "Use W");
            Menu.AddMenuBool("Farm.E.Enabled", "Use E");

            Menu.AddMenuSeparator("Farm.Labels.Separator");
            Menu.AddMenuBool("Farm.Q.Ignore", "Ignore monsters count in Jungle Clear");
            Menu.AddMenuSlider("Farm.Q.HitCount", "Use Q when will hit >= x creeps", 3, 1, 5);

            Menu.AddMenuSeparator("Farm.Labels.Range", "Last hit settings");
            Menu.AddMenuBool("Farm.Q.LastHit", "Use Q in Last Hit", false);
            Menu.AddMenuBool("Farm.Q.Unkillable", "Use Q on Unkillable Minion", false);

            // Attach `local` menu to `global` menu
            Base.Controller.Menu.Add(Menu);
        }
    }
}
