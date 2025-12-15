using Satchel.BetterMenus;
using System;

namespace NoGlow
{
    public static class ModMenu
    {
        private static Menu menu;
        private static MenuScreen menuScreen;

        /// <summary>
        /// Builds the Exaltation Expanded menu
        /// </summary>
        /// <param name="modListMenu"></param>
        /// <returns></returns>
        public static MenuScreen CreateMenuScreen(MenuScreen modListMenu)
        {
            // Declare the menu
            menu = new Menu("No Glow Options", new Element[] { });

            // Populate main menu
            menu.AddElement(new HorizontalOption("Show Hero Glow?",
                                                   "",
                                                   MenuValues(),
                                                   value => NoGlow.globalSettings.stopGlow = !Convert.ToBoolean(value),
                                                   () => Convert.ToInt32(!NoGlow.globalSettings.stopGlow)));

            // Insert the menu into the overall menu
            menuScreen = menu.GetMenuScreen(modListMenu);

            return menuScreen;
        }

        private static string[] MenuValues()
        {
            return new string[] { "NO", "YES" };
        }
    }
}