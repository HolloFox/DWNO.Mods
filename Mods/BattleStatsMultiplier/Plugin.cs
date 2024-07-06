using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;

namespace BattleStatsMultiplier
{
    [BepInPlugin(GUID, PluginName, PluginVersion)]
    [BepInProcess("Digimon World Next Order.exe")]
    public class Plugin : BasePlugin
    {
        internal const string GUID = "hollofox.DWNO.BattleStatModifier";
        internal const string PluginName = "BattleStatModifier";
        internal const string PluginVersion = "1.0.0";

        static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);

        internal static ConfigEntry<int> BattleRateAdd;
        internal static ConfigEntry<double> BattleRateMultiply;
        internal static ConfigEntry<int> battleRateGenAdd;
        internal static ConfigEntry<double> battleRateGenMultiply;

        internal static ConfigEntry<int> trainingRateAdd;
        internal static ConfigEntry<double> trainingRateMultiply;
        internal static ConfigEntry<int> trainingRateGenAdd;
        internal static ConfigEntry<double> trainingRateGenMultiply;

        public override void Load()
        {
            //configfile
            BattleRateAdd = Config.Bind("Battle Configuration", "Added", 0, "Rate in which battle status rewards are added by. Use a positive number.");
            BattleRateMultiply = Config.Bind("Battle Configuration", "Multiplied", 1.0, "Rate in which battle status rewards are multiplied by. Use a positive number.");
            battleRateGenAdd = Config.Bind("Battle Configuration", "Generational Add", 0, "Rate in which battle status rewards are added by (Scales with Digimon Generation). Use a positive number.");
            battleRateGenMultiply = Config.Bind("Battle Configuration", "Generational Multiplied", 0.5, "Rate in which battle status rewards are multiplied by (Scales with Digimon Generation). Use a positive number.");

            trainingRateAdd = Config.Bind("Training Configuration", "Added", 0, "Rate in which training status rewards are added by. Use a positive number.");
            trainingRateMultiply = Config.Bind("Training Configuration", "Multiplied", 1.0, "Rate in which training status rewards are multiplied by. Use a positive number.");
            trainingRateGenAdd = Config.Bind("Training Configuration", "Generational Add", 0, "Rate in which training status rewards are added by (Scales with Digimon Generation). Use a positive number.");
            trainingRateGenMultiply = Config.Bind("Training Configuration", "Generational Multiplied", 0.5, "Rate in which training status rewards are multiplied by (Scales with Digimon Generation). Use a positive number.");

            // Plugin startup logic
            if (BattleRateMultiply.Value < 0 || battleRateGenMultiply.Value < 0 || trainingRateMultiply.Value < 0 || trainingRateGenMultiply.Value < 0)
            {
                Logger.LogInfo($"found problems within its configuration file, please check it!");
                return;
            }
            else
            {
                Logger.LogInfo($"Plugin loaded!");
            }
            Awake();
        }

        public void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(uResultPanelDigimonBase), "SetRiseData")]
        public static class ModPatch
        {

            [HarmonyPrefix]
            public static bool SetRiseData_Patch(uResultPanelDigimonBase __instance,ref int hpRiseValue,ref int mpRiseValue,
                ref int forcefulnessRiseValue,ref int robustnessRiseValue,ref int clevernessRiseValue,
                ref int rapidityRiseValue, int fatigueRiseValue)
            {
                int gen = (int)__instance.m_partnerCtrl.m_data.m_partnerData.m_GenerationNum - 1;
                double multiplier;
                int adder;

                if (__instance.m_partnerCtrl.m_data.m_commonData.m_weight>3000) //see if was training
                {
                    __instance.m_partnerCtrl.m_data.m_commonData.AddWeight(-3000); //remove the weight
                    multiplier = trainingRateMultiply.Value + trainingRateGenMultiply.Value*gen;
                    adder = trainingRateAdd.Value + trainingRateGenAdd.Value*gen;
                }else{
                    multiplier = BattleRateMultiply.Value + battleRateGenMultiply.Value * gen;
                    adder = BattleRateAdd.Value + battleRateGenAdd.Value * gen;
                }

                hpRiseValue = (int)Math.Floor(hpRiseValue * multiplier) + adder;
                mpRiseValue = (int)Math.Floor(mpRiseValue * multiplier) + adder;
                forcefulnessRiseValue = (int)Math.Floor(forcefulnessRiseValue * multiplier) + adder;
                robustnessRiseValue = (int)Math.Floor(robustnessRiseValue * multiplier) + adder;
                clevernessRiseValue = (int)Math.Floor(clevernessRiseValue * multiplier) + adder;
                rapidityRiseValue = (int)Math.Floor(rapidityRiseValue * multiplier) + adder;

                return true;
            }
        }

        [HarmonyPatch(typeof(uTrainingResultPanelDigimon))]
        [HarmonyPatch("SetTrainingResultData")]
        public static class ModPatchTrain
        {
            [HarmonyPrefix]
            public static bool SetTrainingResultData_Patch(ref uTrainingResultPanelDigimon __instance, TrainingResultData trainingResultData){
                __instance.m_partnerCtrl.m_data.m_commonData.AddWeight(3000);
                return true;
            }

        }
        
    }
}
