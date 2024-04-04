namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class OxygenHandler
{
    [HarmonyPatch(typeof(Player.PlayerData), "UpdateValues")]
    public class PlayerData_UpdateValues
    {
        public static void Postfix(Player __instance)
        {
            if (Plugin.DiscordClosed) return;
            if (Plugin.Mode.Value != DetailsMode.OxygenLeft) return;
            if (__instance.data.usingOxygen)
                RpcManager.SetActivity(RpcManager.ActivityField.Details,
                    $"Oxygen Left: {Player.localPlayer.data.remainingOxygen.ToPercent(Player.localPlayer.data.maxOxygen)}%");
        }
    }
}