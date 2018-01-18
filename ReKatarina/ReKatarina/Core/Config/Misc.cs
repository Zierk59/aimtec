using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using ReKatarina.Base;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Config
{
    internal class Misc : IConfig
    {
        public static readonly Menu Menu = new Menu("Rekatarina.Misc", "Misc");

        public void Initialize()
        {
            // Add menu options
            Menu.AddMenuSeparator("Misc.Labels.Main", "Misc settings");

            Menu.AddMenuBool("Misc.KillSteal.Q", "Enable Kill Steal with Q");
            Menu.AddMenuBool("Misc.KillSteal.E", "Enable Kill Steal with E");
            // Menu.AddMenuBool("Misc.KillSteal.Extended", "Enable Kill Steal with E + Q"); <-- Currently disabled
            Menu.AddMenuSlider("Misc.KillSteal.Health", "My health must be >= x% to use E.", 30, 1);

            Menu.AddMenuSeparator("Misc.Labels.Skin", "Skin manager");
            Menu.AddMenuSlider("Misc.Skin.Id", "Select your skin", 0, 0, 10);

            Menu.AddMenuSeparator("Misc.Labels.Other", "Other settings");
            Menu.AddMenuBool("Misc.Developer", "Enable developer mode", false);

            Menu["Misc.Skin.Id"].OnValueChanged += Misc_OnValueChanged;

            // Attach `local` menu to `global` menu
            Controller.Menu.Add(Menu);
        }

        private static void Misc_OnValueChanged(MenuComponent sender, ValueChangedArgs args)
        {
            if (args.InternalName.Contains("Misc.Skin.Id"))
            {
                ReKatarina.Player.SetSkinId(args.GetNewValue<MenuSlider>().Value);
                Logger.Print($"Player skin changed, new Id = {args.GetNewValue<MenuSlider>().Value}", Logger.LogType.Warning);
            }
        }
    }
}
