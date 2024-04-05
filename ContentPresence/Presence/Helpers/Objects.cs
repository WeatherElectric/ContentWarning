namespace WeatherElectric.ContentPresence.Presence.Helpers;

internal static class Objects
{
    public static VideoInfoEntry VideoInfoEntry { get; private set; }
    public static VideoCamera VideoCamera { get; private set; }
    
    public static void Patch()
    {
        On.VideoCamera.Start += OnCameraStart;
    }
    
    private static void OnCameraStart(On.VideoCamera.orig_Start orig, VideoCamera self)
    {
        orig(self);
        VideoCamera = self;
        Type type = self.GetType();
        FieldInfo privateFieldInfo = type.GetField("m_recorderInfoEntry", BindingFlags.Instance | BindingFlags.NonPublic);
        if (privateFieldInfo != null) VideoInfoEntry = (VideoInfoEntry)privateFieldInfo.GetValue(self);
    }
}