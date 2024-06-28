/*using HarmonyLib;
using System.Threading;

namespace EOSPlugin.Patches
{
    [HarmonyPatch(typeof(SteamAuth), "OnInstanceSetup")]
    internal class SteamAuthOnInstanceSetupPatch
    {



        internal static void Postfix()
        {
            var Logger = EOSPlugin.logSource;
            Logger.LogInfo($"Fetching Steam tickes");

            new Thread(() => {
                var appTicket = SteamAuth.RequestEncryptedAppTicket().Result;
                Logger.LogInfo($"Steam EAPP Ticket: {appTicket}");
                EOSPlugin.SEAAP = appTicket;

                SteamAuth.RequestAuthSessionTicket((ticket) => {
                    Logger.LogInfo($"Steam ASS Ticket: {ticket.Ticket}");
                    EOSPlugin.SASS = ticket.Ticket;

                    EOSPlugin.SteamAuthFound = true;
                }, (failedResult) => {
                    Logger.LogInfo($"{failedResult.ToString()}");
                });

            }).Start();
            
        }
    }
}
*/