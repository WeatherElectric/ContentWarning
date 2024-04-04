using HarmonyLib;
using MelonLoader;
using WeatherElectric.ContentPresence.Melon;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

public static class DeathHandler
{
    private static int _casualties;

    public static void Reset()
    {
        _casualties = 0;
        if (Preferences.DetailsMode.Value == DetailsMode.Casualties) RpcManager.SetActivity(RpcManager.ActivityField.Details, "No Casualties Yet!");
    }
    
    [HarmonyPatch(typeof(Player), "Die")]
    public class OnPlayerDeath
    {
        public static void Postfix(Player __instance)
        {
            _casualties++;
            if (Preferences.DetailsMode.Value == DetailsMode.Casualties) RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Casualties: {_casualties}");
            if (__instance.IsLocal) RpcManager.SetActivity(RpcManager.ActivityField.State, "Dead");
        }
    }
}