using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using System.Collections;
using System.Threading;

namespace BotamonMenu;

[BepInPlugin(GUID, PluginName, PluginVersion)]
[BepInProcess("Digimon World Next Order.exe")]
public class Plugin : BasePlugin
{
    internal const string GUID = "hollofox.DWNO.BotamonMenu";
    internal const string PluginName = "BotamonMenu";
    internal const string PluginVersion = "1.0.0";

    // internal static Config
    static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);
    public override void Load() => Harmony.CreateAndPatchAll(typeof(Plugin), GUID);

    private static EventWindowPanel instance;

    private static bool searching = false;
    private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    [HarmonyPatch(typeof(TalkMain), "DispTextMain")]
    [HarmonyPrefix]
    public static bool TalkMain_DispTextMain_Postfix(string name, string text, ref Il2CppSystem.Collections.IEnumerator __result)
    {
        Logger.LogInfo($"TalkMain_DispTextMain_Postfix: {name} {text}");
        if (name == "a001" && (text == "TOWN_TALK_A001_001" || text == "TOWN_TALK_A001_002"))
        {
            __result = GetEnumerator().WrapToIl2Cpp();
            return false;
        }
        return true;
    }

    public static Il2CppSystem.Collections.IEnumerator wrapped = GetEnumerator().WrapToIl2Cpp();

    public static IEnumerator GetEnumerator()
    {
        yield return null;
    }

}