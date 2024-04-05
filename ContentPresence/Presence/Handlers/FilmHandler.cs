namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class FilmHandler
{
    public static void Patch()
    {
        Plugin.Mls.LogInfo("Patching FilmHandler...");
        On.VideoCamera.StartRecording += OnStartRecording;
        On.VideoCamera.StopRecording += OnStopRecording;
    }
    
    private static void OnStartRecording(On.VideoCamera.orig_StartRecording orig, VideoCamera self, Clip clip)
    {
        orig(self, clip);
        if (Plugin.DiscordClosed) return;
        RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "recording");
        RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {Objects.VideoInfoEntry.timeLeft.ToPercent(Objects.VideoInfoEntry.maxTime)}%");
    }
    
    private static void OnStopRecording(On.VideoCamera.orig_StopRecording orig, VideoCamera self)
    {
        orig(self);
        if (Plugin.DiscordClosed) return;
        RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
        RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {Objects.VideoInfoEntry.timeLeft.ToPercent(Objects.VideoInfoEntry.maxTime)}%");
    }
    
    public static void Update()
    {
        if (Objects.VideoCamera == null) return;
        if (Objects.VideoCamera.recording)
        {
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {Objects.VideoInfoEntry.timeLeft.ToPercent(Objects.VideoInfoEntry.maxTime)}%");
        }
    }
}