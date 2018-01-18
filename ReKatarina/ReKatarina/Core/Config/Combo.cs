using Aimtec.SDK.Menu;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Config
{
    internal class Combo : IConfig
    {
        public static readonly Menu Menu = new Menu("Rekatarina.Combo", "Combo");

        public void Initialize()
        {
            // Add menu options
            Menu.AddMenuSeparator("Combo.Labels.Main", "Combo settings");
            // Menu.AddMenuList("Combo.Style", "Combo mode", new string[] { "E->W->Q->R", "E->Q->W->R", "Q->E->W->R" }); <-- One combo is better, REMOVED in beta version
            Menu.AddMenuBool("Combo.Q.Enabled", "Enable Q");
            Menu.AddMenuBool("Combo.W.Enabled", "Enable W");
            Menu.AddMenuBool("Combo.E.Enabled", "Enable E");
            Menu.AddMenuBool("Combo.R.Enabled", "Enable R");

            Menu.AddMenuSeparator("Combo.Labels.R", "R settings");
            Menu.AddMenuSlider("Combo.R.Enemies", "Minimum enemies to use R", 1, 1, 5);
            Menu.AddMenuSlider("Combo.R.Range", "Maximum R range to cast", 250, 250, 550);

            Menu.AddMenuSeparator("Combo.Labels.Another", "Another settings");
            Menu.AddMenuSlider("Combo.Saver", "Don't engage if my health <= x% and QW is not ready.", 30, 1);
            Menu.AddMenuBool("Combo.E.Turret", "Allow E jump under enemy turret", false);
            Menu.AddMenuSlider("Combo.E.Health", "My health must be >= x% to allow enter under enemy tower.", 40, 1);

            // Attach `local` menu to `global` menu
            Base.Controller.Menu.Add(Menu);
        }
    }
}
