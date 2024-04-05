using BepInEx.Configuration;

namespace WeatherElectric.ContentPresence.Bepin;

internal static class Preferences
{
    public static ConfigFile Config;
    
    public static ConfigEntry<long> DiscordAppId;
    public static ConfigEntry<DetailsMode> Mode;
    
    public static void Setup()
    {
        DiscordAppId = Config.Bind("Discord", "DiscordAppId", 1225293196914069524, "The application ID that the mod will use.");
        Mode = Config.Bind("Details", "DetailsMode", DetailsMode.Casualties, "The mode for the details section. Possible Values: Entries, Casualties, OxygenLeft");
    }
    
    public enum DetailsMode
    {
        Entries,
        Casualties,
        OxygenLeft
    }
}