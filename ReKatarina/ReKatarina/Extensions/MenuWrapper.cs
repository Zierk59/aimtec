using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;

namespace ReKatarina.Extensions
{
    /// <summary>
    ///     My own menu wrapper. Now you can create menu faster than earlier.
    /// </summary>
    internal static class MenuWrapper
    {
        #region Add options
        /// <summary>
        /// Add `checkbox` menu
        /// </summary>
        public static void AddMenuBool(this Menu m, string internalId, string name, bool Default = true) 
            => m.Add(new MenuBool(internalId, name, Default));

        /// <summary>
        /// Add `slider` menu
        /// </summary>
        public static void AddMenuSlider(this Menu m, string internalId, string name, int Default, int min = 0, int max = 100) 
            => m.Add(new MenuSlider(internalId, name, Default, min, max));

        /// <summary>
        /// Add `combo box` menu
        /// </summary>
        public static void AddMenuList(this Menu m, string internalId, string name, string[] items, int Default = 0) 
            => m.Add(new MenuList(internalId, name, items, Default));

        /// <summary>
        /// Add `keybind` menu
        /// </summary>
        public static void AddMenuKeyBind(this Menu m, string internalId, string name, KeyCode key = KeyCode.U, KeybindType bindType = KeybindType.Press, bool Default = true) 
            => m.Add(new MenuKeyBind(internalId, name, key, bindType, Default));

        /// <summary>
        /// Add `separator` menu
        /// </summary>
        public static void AddMenuSeparator(this Menu m, string internalId, string text = "")
            => m.Add(new MenuSeperator(internalId, text));
        #endregion
        #region Get options
        /// <summary>
        /// Get `checkbox` menu value
        /// </summary>
        public static bool GetMenuBool(this Menu m, string internalId)
        {
            return m[internalId].Enabled;
        }

        /// <summary>
        /// Get `slider` menu value
        /// </summary>
        public static int GetMenuSlider(this Menu m, string internalId)
        {
            return m[internalId].Value;
        }

        /// <summary>
        /// Get `combo box` menu value
        /// </summary>
        public static int GetMenuList(this Menu m, string internalId)
        {
            return m[internalId].Value;
        }

        /// <summary>
        /// Get `key bind` menu value
        /// </summary>
        public static bool GetMenuKeyBind(this Menu m, string internalId)
        {
            return m[internalId].Enabled;
        }
        #endregion
    }
}
