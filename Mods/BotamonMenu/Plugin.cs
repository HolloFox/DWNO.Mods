using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Runtime.InteropServices;
using UnityEngine;

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

    [HarmonyPatch(typeof(EventTriggerManager), "AddEventTriger")]
    [HarmonyPrefix]
    public static bool EventTriggerManager_AddEventTriger_Prefix(GameObject _object, uint _placementId)
    {
        Logger.LogWarning($"AddEventTriger:{_object.name}: {_placementId}");
        if (_placementId == 532456915)
        {
            Logger.LogWarning($"Trigger found!!:{_placementId}");
            return false;
        }
        return true;
    }

    

    [HarmonyPatch(typeof(EventTriggerManager), "ActiveTalkEventTrigger")]
    [HarmonyPrefix]
    public static void EventTriggerManager_ActiveTalkEventTrigger_Prefix(uint _placementId, bool _isActive)
    {
        Logger.LogWarning($"ActiveTalkEventTrigger: {_placementId}");
    }

    [HarmonyPatch(typeof(EventTriggerScript), "Start")]
    [HarmonyPrefix]
    public static bool EventTriggerScript_Start_Prefix(ref EventTriggerScript __instance)
    {
        Logger.LogWarning($"EventTriggerScript Started");

        return true;
    }

    /*
    [HarmonyPatch(typeof(uDigimonMessagePanel), "StartMessage")]
    [HarmonyPostfix]
    public static void uDigimonMessagePanel_StartMessage_Postfix(string message, float time)
    {
        Logger.LogWarning($"{time}: {message}");
    }

    [HarmonyPatch(typeof(uCommonMessageWindow), "SetMessage")]
    [HarmonyPostfix]
    public static void uCommonMessageWindow_SetMessage_Postfix(string str, Pos window_pos = Pos.Center)
    {
        // Big Dialogs, not Npc Talking
        Logger.LogWarning($"{str}");
    }

    [HarmonyPatch(typeof(ParameterPlacementNpc), "OpenDialog")]
    [HarmonyPostfix]
    public static void uDialogBase_OpenDialog_Postfix(string title, string message)
    {
        
        AppMainScript.parameterManager.digimonIdList.ForEach((digimonId) =>
        {
            Logger.LogWarning($"{title}: {message}");
        });
        

        Logger.LogWarning($"{title}: {message}");
    }*/

}