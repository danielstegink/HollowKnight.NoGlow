using System;
using System.Collections.Generic;
using Modding;
using UnityEngine;

namespace NoGlow
{
    public class NoGlow : Mod, IGlobalSettings<GlobalSettings>, ICustomMenuMod
    {
        /// <summary>
        /// Static instance of this mod I use for logging purposes
        /// </summary>
        public static NoGlow Instance;

        /// <summary>
        /// Version of the mod.
        /// Hopefully this number doesn't change too often.
        /// </summary>
        /// <returns></returns>
        public override string GetVersion() => "1.0.0.0";

        #region Global Settings
        /// <summary>
        /// Stores the global settings for ease of reference
        /// </summary>
        internal static GlobalSettings globalSettings = new GlobalSettings();

        public void OnLoadGlobal(GlobalSettings s)
        {
            globalSettings = s;
        }

        public GlobalSettings OnSaveGlobal()
        {
            return globalSettings;
        }
        #endregion

        /// <summary>
        /// Initialization step. Fairly useless here, but vital in most mods.
        /// </summary>
        /// <param name="preloadedObjects"></param>
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) 
        {
            Log("Initializing");

            Instance = this;
            On.HeroController.Update += ToggleGlow;

            Log("Initialized");
        }

        /// <summary>
        /// Make sure to frequently check if the Hero Glow has been disabled or not
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ToggleGlow(On.HeroController.orig_Update orig, HeroController self)
        {
            orig(self);

            GameObject heroLight = UnityEngine.GameObject.Find("HeroLight");
            if (heroLight == null)
            {
                return;
            }

            SpriteRenderer spriteRenderer = heroLight.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                return;
            }

            spriteRenderer.enabled = !globalSettings.stopGlow;
        }

        #region Menu
        public bool ToggleButtonInsideMenu => false;

        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggleDelegates)
        {
            return ModMenu.CreateMenuScreen(modListMenu);
        }
        #endregion
    }
}