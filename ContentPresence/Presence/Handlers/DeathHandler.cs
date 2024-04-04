using HarmonyLib;
using MelonLoader;
using WeatherElectric.ContentPresence.Melon;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class DeathHandler
{
    private static int _casualties;

    public static void Reset()
    {
        _casualties = 0;
        RpcManager.SetActivity(RpcManager.ActivityField.Details, "No Casualties Yet!");
    }
    
    [HarmonyPatch(typeof(Player), "Die")]
    public class OnPlayerDeath
    {
        public static void Postfix(Player __instance)
        {
            if (Main.DiscordClosed) return;
            if (__instance.IsLocal) RpcManager.SetActivity(RpcManager.ActivityField.State, "Dead");
            if (Preferences.DetailsMode.Value != DetailsMode.Casualties) return;
            _casualties++;
            RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Casualties: {_casualties}");
        }
    }
}