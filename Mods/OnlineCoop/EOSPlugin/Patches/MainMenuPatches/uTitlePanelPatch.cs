// using EOSPlugin.EOS;
using EOSPlugin.EOS;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace EOSPlugin.Patches.MainMenuPatches
{
    [HarmonyPatch(typeof(uTitlePanel), "Awake")]
    public static class uTitlePanelPatch
    {

        [HarmonyPrefix]
        public static void uTitlePanel_PrefixPatch(ref uTitlePanel __instance)
        {
            Debug.Log("uTitlePanel_PrefixPatch");
            EOSManager.Initialize();

            /*
            Debug.LogError(__instance.TEXT_LANG_CODE.Length);

            // 
            __instance.TEXT_LANG_CODE = new[] {
                __instance.TEXT_LANG_CODE[0],
                __instance.TEXT_LANG_CODE[1],
                Language.makeHash("title_select3"),
                __instance.TEXT_LANG_CODE[2],
                __instance.TEXT_LANG_CODE[3],
            };

            Debug.LogError(__instance.m_cursorObject.Length);

            var o = __instance.m_cursorObject[1];
            GameObject copy = GameObject.Instantiate(o.gameObject);
            o.gameObject.transform.parent.gameObject.AddChild(copy);

            __instance.m_cursorObject = new[] {
                __instance.m_cursorObject[0],
                __instance.m_cursorObject[1],
                copy.GetComponent<RectTransform>(),
                __instance.m_cursorObject[2],
                __instance.m_cursorObject[3],
            };

            __instance.m_close_anim = new[] {
                __instance.m_close_anim[0],
                __instance.m_close_anim[1],
                __instance.m_close_anim[1],
                __instance.m_close_anim[2],
                __instance.m_close_anim[3],
            };*/
        }
    }

    [HarmonyPatch(typeof(Language), "GetString", typeof(uint))]
    public static class LanguagePatch
    {

        [HarmonyPrefix]
        public static bool Language_PrefixPatch(uint lang_code, ref string __result)
        {
            if (lang_code == Language.makeHash("title_select3"))
            {
                __result = "Lobby Setup";
                return false;
            }

            return true;
        }
    }
}
