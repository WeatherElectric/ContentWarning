using MelonLoader;
using MelonLoader.Utils;

namespace WeatherElectric.ContentPresence.Melon;

internal static class Preferences
{
    private static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
    private static readonly MelonPreferences_Category Category = MelonPreferences.CreateCategory("ContentPresence");

    public static MelonPreferences_Entry<int> LoggingMode;
    public static MelonPreferences_Entry<long> DiscordAppId;
    public static MelonPreferences_Entry<DetailsMode> DetailsMode;

    public static void Setup()
    {
        LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0,
            "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
        DiscordAppId = Category.CreateEntry("DiscordAppId", 1225293196914069524, "Discord Application ID",
            "The application ID for the Discord application that will be used for Rich Presence.");
        DetailsMode = Category.CreateEntry("DetailsMode", Melon.DetailsMode.Entries, "Details Mode",
            "The mode for the details section. Possible Values: Entries, FilmLeft, Casualties, DistanceFromPod");

        GlobalCategory.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        GlobalCategory.SaveToFile(false);
        Category.SetFilePath(MelonEnvironment.UserDataDirectory + "/WeatherElectric.cfg");
        Category.SaveToFile(false);
        ModConsole.Msg("Finished preferences setup", 1);
    }
}

public enum DetailsMode
{
    Entries,
    FilmLeft,
    Casualties,
    DistanceFromPod
}