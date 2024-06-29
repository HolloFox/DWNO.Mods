using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace InstantBuild;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "hollofox.DWNO.InstantBuild";
    internal const string PluginName = "InstantBuild";
    internal const string PluginVersion = "1.0.0";

    // internal static Config
    static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);
    public override void Load() => Harmony.CreateAndPatchAll(typeof(Plugin));

    [HarmonyPatch(typeof(ParameterTownGradeUpData), "GetParam", typeof(uint))]
    [HarmonyPostfix]
    public static void GetKindTownGradeUpData_Postfix(ref uint id, ref ParameterTownGradeUpData __result)
    {
        if (__result != null) __result.m_grade_up_time = 0;
    }
}