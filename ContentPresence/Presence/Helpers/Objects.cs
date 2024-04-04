namespace WeatherElectric.ContentPresence.Presence.Helpers;

internal static class Objects
{
    public static VideoInfoEntry VideoInfoEntry { get; private set; }
    public static VideoCamera VideoCamera { get; private set; }
    
    
    [HarmonyPatch(typeof(VideoCamera), "Start")]
    internal class VideoCamera_Start
    {
        public static void Postfix(VideoCamera __instance)
        {
            VideoCamera = __instance;
            Type type = __instance.GetType();
            FieldInfo privateFieldInfo = type.GetField("m_recorderInfoEntry", BindingFlags.Instance | BindingFlags.NonPublic);
            if (privateFieldInfo != null) VideoInfoEntry = (VideoInfoEntry)privateFieldInfo.GetValue(__instance);
        }
    }
}