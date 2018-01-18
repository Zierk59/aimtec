using Aimtec.SDK.Menu;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Config
{
    internal class Drawing : IConfig
    {
        public static readonly Menu Menu = new Menu("Rekatarina.Drawing", "Drawing");

        public void Initialize()
        {
            // Add menu options
            Menu.AddMenuSeparator("Drawing.Labels.Main", "Drawing settings");
            Menu.AddMenuBool("Drawing.Q.Enabled", "Draw Q");
            Menu.AddMenuBool("Drawing.W.Enabled", "Draw W", false);
            Menu.AddMenuBool("Drawing.E.Enabled", "Draw E");
            Menu.AddMenuBool("Drawing.R.Enabled", "Draw R", false);
            Menu.AddMenuBool("Drawing.Daggers", "Draw Daggers");

            // Attach `local` menu to `global` menu
            Base.Controller.Menu.Add(Menu);
        }
    }
}
