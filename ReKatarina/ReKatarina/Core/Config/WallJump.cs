using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;
using ReKatarina.Base.Interfaces;
using ReKatarina.Extensions;

namespace ReKatarina.Core.Config
{
    internal class WallJump : IConfig
    {
        public static readonly Menu Menu = new Menu("Rekatarina.Walljump", "Walljump");

        public void Initialize()
        {
            // Add menu options
            Menu.AddMenuSeparator("Walljump.Labels.Main", "Walljump settings");

            Menu.AddMenuList("Walljump.Mode", "Walljump mode", new[] { "Always enabled", "Hold key", "Release key" });
            Menu.AddMenuList("Walljump.Difficulty", "Jump difficulty", new[] { "Easy", "Medium", "Hard" }, 2);
            Menu.AddMenuSeparator("Walljump.Labels.Info", "A greater level of difficulty = more jump spots");

            // Add walljump menu
            Menu_ShowOptions(Menu.GetMenuList("Walljump.Mode"));

            // Trigger OnChange event
            Menu["Walljump.Mode"].OnValueChanged += WallJump_OnValueChanged;

            // Attach `local` menu to `global` menu
            Base.Controller.Menu.Add(Menu);
        }

        private void WallJump_OnValueChanged(MenuComponent sender, ValueChangedArgs args)
        {
            if (args.InternalName.Contains("Walljump.Mode"))
            {
                // Hide old and show new options
                Menu_HideOptions(args.GetPreviousValue<MenuList>().Value);
                Menu_ShowOptions(args.GetNewValue<MenuList>().Value);
            }
        }

        /// <summary>
        ///     Add dyanmic menu
        /// </summary>
        /// <param name="id"></param>
        private void Menu_ShowOptions(int id)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (id)
            {
                case 0:
                    {
                        Menu.AddMenuBool("Walljump.Status", "Enable Walljump");
                        Menu.AddMenuBool("Walljump.Combo", "Enable in combo mode", false);
                        Menu.AddMenuBool("Walljump.Harass", "Enable in harass mode", false);
                        Menu.AddMenuBool("Walljump.Farm", "Enable in clear mode", false);
                        Menu.AddMenuBool("Walljump.Lasthit", "Enable in last hit", false);

                        break;
                    }

                case 1:
                    {
                        Menu.AddMenuKeyBind("Walljump.Status", "Enable Walljump", KeyCode.U, KeybindType.Press, false);

                        break;
                    }

                case 2:
                    {
                        Menu.AddMenuKeyBind("Walljump.Status", "Enable Walljump", KeyCode.U, KeybindType.Toggle);
                        Menu.AddMenuBool("Walljump.Combo", "Enable in combo mode", false);
                        Menu.AddMenuBool("Walljump.Harass", "Enable in harass mode", false);
                        Menu.AddMenuBool("Walljump.Farm", "Enable in clear mode", false);
                        Menu.AddMenuBool("Walljump.Lasthit", "Enable in last hit", false);

                        break;
                    }
            }
        }

        /// <summary>
        ///     Remove dynamic menu
        /// </summary>
        /// <param name="id"></param>
        private void Menu_HideOptions(int id)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (id)
            {
                case 0:
                case 2:
                    {
                        Menu["Walljump.Combo"].Dispose();
                        Menu["Walljump.Harass"].Dispose();
                        Menu["Walljump.Farm"].Dispose();
                        Menu["Walljump.Lasthit"].Dispose();

                        break;
                    }
            }

            Menu["Walljump.Status"].Dispose();
        }
    }
}
