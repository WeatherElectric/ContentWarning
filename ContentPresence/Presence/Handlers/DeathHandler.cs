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
    public class Player_Die
    {
        public static void Postfix(Player __instance)
        {
            if (Plugin.DiscordClosed) return;
            if (__instance.IsLocal) RpcManager.SetActivity(RpcManager.ActivityField.State, "Dead");
            if (Plugin.Mode.Value != DetailsMode.Casualties) return;
            _casualties++;
            RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Casualties: {_casualties}");
        }
    }
}