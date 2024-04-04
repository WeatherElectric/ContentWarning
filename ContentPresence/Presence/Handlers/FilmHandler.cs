using HarmonyLib;
using WeatherElectric.ContentLib;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class FilmHandler
{
    [HarmonyPatch(typeof(VideoCamera), "StartRecording")]
    public class VideoCamera_StartRecording
    {
        public static void Postfix(VideoCamera __instance)
        {
            if (Main.DiscordClosed) return;
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "recording");
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {Objects.VideoInfoEntry.timeLeft.ToPercent(Objects.VideoInfoEntry.maxTime)}%");
        }
    }
    
    [HarmonyPatch(typeof(VideoCamera), "StopRecording")]
    public class VideoCamera_StopRecording
    {
        public static void Postfix(VideoCamera __instance)
        {
            if (Main.DiscordClosed) return;
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {Objects.VideoInfoEntry.timeLeft.ToPercent(Objects.VideoInfoEntry.maxTime)}%");
        }
    }

    public static void Update()
    {
        if (Objects.VideoCamera.recording)
        {
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {Objects.VideoInfoEntry.timeLeft.ToPercent(Objects.VideoInfoEntry.maxTime)}%");
        }
    }
}