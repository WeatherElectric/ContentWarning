namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class OxygenHandler
{
    public static void Patch()
    {
        Plugin.Mls.LogInfo("Patching OxygenHandler...");
        On.Player.PlayerData.UpdateValues += OnPlayerUpdate;
    }
    
    public static void Unpatch()
    {
        Plugin.Mls.LogInfo("Unpatching OxygenHandler...");
        On.Player.PlayerData.UpdateValues -= OnPlayerUpdate;
    }

    public static void PrefsChanged()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == SceneNames.MainMenu)
        {
            RpcManager.SetActivity(RpcManager.ActivityField.Details, "You don't need oxygen in the menu!");
        }
        if (sceneName == SceneNames.Home)
        {
            RpcManager.SetActivity(RpcManager.ActivityField.Details, "Around trees. Oxygen fine!");
        }
        else
        {
            if (Player.localPlayer.data.usingOxygen) RpcManager.SetActivity(RpcManager.ActivityField.Details,
                    $"Oxygen Left: {Player.localPlayer.data.remainingOxygen.ToPercent(Player.localPlayer.data.maxOxygen)}%");
        }
    }

    private static void OnPlayerUpdate(On.Player.PlayerData.orig_UpdateValues orig, Player.PlayerData self)
    {
        orig(self);
        if (Plugin.DiscordClosed) return;
        if (Preferences.Mode.Value != Preferences.DetailsMode.OxygenLeft) return;
        if (self.usingOxygen)
            RpcManager.SetActivity(RpcManager.ActivityField.Details,
                $"Oxygen Left: {Player.localPlayer.data.remainingOxygen.ToPercent(Player.localPlayer.data.maxOxygen)}%");
    }
}