using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using EOSPlugin.EOS;
using HarmonyLib;

namespace EOSPlugin
{
    [BepInPlugin(Guid, Name, Version)]
    public class EOSPlugin : BasePlugin
    {
        // Plugin info
        public const string Name = "EOS";
        public const string Guid = "org.hollofox.plugins.eos";
        public const string Version = "0.0.0.0";

        static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(Name);
        public static ManualLogSource logSource;

        /// <summary>
        /// Method triggered when the plugin loads
        /// </summary>
        public override void Load()
        {
            Logger.LogInfo("In Awake");
            logSource = Logger;

            new Harmony(Guid).PatchAll();
            EOSManager.Initialize();

        }

        bool inBoard = false;

        internal static bool SteamAuthFound = false;
        private static bool SteamAuthProcessed = false;

        internal static string SEAAP;
        internal static string SASS;

        public void Update()
        {
            EOSManager.Tick();

        }

    }
}
