namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class DeathHandler
{
    private static int _casualties;

    public static void Patch()
    {
        Plugin.Mls.LogInfo("Patching DeathHandler...");
        On.Player.Die += OnPlayerDie;
    }

    public static void Unpatch()
    {
        Plugin.Mls.LogInfo("Unpatching DeathHandler...");
        On.Player.Die -= OnPlayerDie;
    }
    
    public static void PrefsChanged()
    {
        if (SceneManager.GetActiveScene().name == SceneNames.MainMenu) RpcManager.SetActivity(RpcManager.ActivityField.Details, "You can't die in the menu!");
        else RpcManager.SetActivity(RpcManager.ActivityField.Details, _casualties == 0 ? "No Casualties Yet!" : $"Casualties: {_casualties}");
    }

    private static void OnPlayerDie(On.Player.orig_Die orig, Player self)
    {
        orig(self);
        if (Plugin.DiscordClosed) return;
        if (self.IsLocal) RpcManager.SetActivity(RpcManager.ActivityField.State, "Dead");
        if (Preferences.Mode.Value != Preferences.DetailsMode.Casualties) return;
        _casualties++;
        RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Casualties: {_casualties}");
    }

    public static void Reset()
    {
        _casualties = 0;
        RpcManager.SetActivity(RpcManager.ActivityField.Details, "No Casualties Yet!");
    }
}