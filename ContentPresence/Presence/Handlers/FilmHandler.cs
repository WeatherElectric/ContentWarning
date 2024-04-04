using System;
using System.Reflection;
using HarmonyLib;
using WeatherElectric.ContentLib;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

public static class FilmHandler
{
    private static VideoInfoEntry GetVideoInfoEntry(VideoCamera camera)
    {
        Type type = camera.GetType();
        FieldInfo privateFieldInfo = type.GetField("m_recorderInfoEntry", BindingFlags.Instance | BindingFlags.NonPublic);
        if (privateFieldInfo != null) return (VideoInfoEntry)privateFieldInfo.GetValue(camera);
        return null;
    }
    
    private static bool _isRecording;
    private static float _filmLeft;
    private static VideoInfoEntry _videoInfoEntry;
    
    [HarmonyPatch(typeof(VideoCamera), "Start")]
    public class OnCameraAwake
    {
        public static void Postfix(VideoCamera __instance)
        {
            _videoInfoEntry = GetVideoInfoEntry(__instance);
        }
    }
    
    [HarmonyPatch(typeof(VideoCamera), "StartRecording")]
    public class OnCameraRecord
    {
        public static void Postfix(VideoCamera __instance)
        {
            _isRecording = true;
            _filmLeft = _videoInfoEntry.timeLeft;
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "recording");
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {_videoInfoEntry.timeLeft.ToPercent(_videoInfoEntry.maxTime)}");
        }
    }
    
    [HarmonyPatch(typeof(VideoCamera), "StopRecording")]
    public class OnCameraStop
    {
        public static void Postfix(VideoCamera __instance)
        {
            _isRecording = false;
            _filmLeft = _videoInfoEntry.timeLeft;
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {_videoInfoEntry.timeLeft.ToPercent(_videoInfoEntry.maxTime)}");
        }
    }

    public static void Update()
    {
        if (_isRecording)
        {
            _filmLeft = _videoInfoEntry.timeLeft;
            RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, $"Film Left: {_filmLeft.ToPercent(_videoInfoEntry.maxTime)}%");
        }
    }
}