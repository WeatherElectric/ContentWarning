using HarmonyLib;
using WeatherElectric.ContentLib;
using WeatherElectric.ContentPresence.Melon;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class OxygenHandler
{
    [HarmonyPatch(typeof(Player), "UpdateValues")]
    public class Player_UpdateValues
    {
        public static void Postfix(Player __instance)
        {
            if (Main.DiscordClosed) return;
            if (Preferences.DetailsMode.Value != DetailsMode.OxygenLeft) return;
            if (__instance.data.usingOxygen)
                RpcManager.SetActivity(RpcManager.ActivityField.Details,
                    $"Oxygen Left: {Objects.LocalPlayer.data.remainingOxygen.ToPercent(Objects.LocalPlayer.data.maxOxygen)}%");
        }
    }
}