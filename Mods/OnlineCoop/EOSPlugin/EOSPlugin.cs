using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using EOSPlugin.EOS;
using HarmonyLib;

namespace EOSPlugin
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInProcess("Digimon World Next Order.exe")]
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
        }

        internal static bool isInitialized = false;

        public void Update()
        {
            EOSManager.Tick();
        }

    }
}
