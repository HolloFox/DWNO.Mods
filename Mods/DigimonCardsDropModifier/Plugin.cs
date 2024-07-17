using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace BattleStatsMultiplier
{
    [BepInPlugin(GUID, PluginName, PluginVersion)]
    [BepInProcess("Digimon World Next Order.exe")]
    public class Plugin : BasePlugin
    {
        internal const string GUID = "hollofox.DWNO.DigimonCardsDropModifier";
        internal const string PluginName = "DigimonCardsDropModifier";
        internal const string PluginVersion = "1.0.0";

        static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);

        internal static ConfigEntry<int> DigimonCardsDropModifier;

        public override void Load()
        {
            //configfile
            DigimonCardsDropModifier = Config.Bind("Card Number", "Last Card Number", 1, "Last Card");

            Awake();
        }

        public void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(uItemPickPanelResult), "enablePanel")]
        public static class Patch_uItemPickPanelResult_enablePanel
        {
            
            public static void Prefix(bool enable, ItemPickPointBase targetItemPickPoint, Il2CppStructArray<uint> itemIds, ref int digimonCardNumber)
            {
                for (int i = DigimonCardsDropModifier.Value; i <= 540; i++)
                {
                    StorageData.m_digimonCardFlag.SetFlag((uint)i, true);
                    Logger.LogInfo($"Card Number: {i}");
                    DigimonCardsDropModifier.Value = i;
                }
            }
        }
        
    }
}
