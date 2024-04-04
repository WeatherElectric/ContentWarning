using MelonLoader;
using MelonLoader.Utils;

namespace WeatherElectric.ContentLib.Melon;

internal static class Preferences
{
    private static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
    private static readonly MelonPreferences_Category Category = MelonPreferences.CreateCategory("ContentPresence");

    public static MelonPreferences_Entry<int> LoggingMode;

    public static void Setup()
    {
        LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0,
            "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
        GlobalCategory.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        GlobalCategory.SaveToFile(false);
        Category.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        Category.SaveToFile(false);
        ModConsole.Msg("Finished preferences setup", 1);
    }
}