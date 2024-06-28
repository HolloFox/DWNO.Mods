using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Linq;
using static ScreenResolution;

namespace MoreResolutions;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "hollofox.DWNO.MoreResolutions";
    internal const string PluginName = "MoreResolutions";
    internal const string PluginVersion = "1.0.0";

    static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);
    public override void Load() => Harmony.CreateAndPatchAll(typeof(Plugin));

    [HarmonyPatch(typeof(ScreenResolution), "GetSupportedResolutions", typeof(ScreenRatio))]
    [HarmonyPostfix]
    public static void GetSupportedResolutions_Postfix(ScreenRatio sr, ref Il2CppReferenceArray<ResolutionType> __result)
    {
        var resolutionTypes = new ResolutionType[]
        {
            new(2304, 1440, ScreenRatio._16_10),
            new(2560, 1440, ScreenRatio._16_9),
            new(3840, 2160, ScreenRatio._16_9),
            new(3840, 2400, ScreenRatio._16_10)
        };

        __result = __result.AddUniqueResolutions(resolutionTypes);
        m_resolution = m_resolution.AddUniqueResolutions(resolutionTypes);
    }
}

internal static class Extenions
{
    internal static Il2CppReferenceArray<ResolutionType> AddUniqueResolutions(this Il2CppReferenceArray<ResolutionType> values, params ResolutionType[] item)
    {
        var listedValues = values.ToList();
        foreach (var i in item)
        {
            if (!listedValues.Any(v => v.x == i.x && v.y == i.y))
            {
                listedValues.Add(i);
            }
        }
        return listedValues.ToArray();
    }
}