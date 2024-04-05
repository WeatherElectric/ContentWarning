using System.IO;

namespace WeatherElectric.ContentPresence.Bepin;

internal static class UserData
{
    private static readonly string UserDataDirectory = Path.Combine(BepInEx.Paths.ConfigPath, "Weather Electric/Content Presence");
    public static readonly string UserEntriesPath = Path.Combine(UserDataDirectory, "UserEntries.txt");
    
    public static void Setup()
    {
        if (!Directory.Exists(UserDataDirectory))
        {
            Directory.CreateDirectory(UserDataDirectory);
        }
        if (!File.Exists(UserEntriesPath))
        {
            File.WriteAllBytes(UserEntriesPath, HelperMethods.GetResourceBytes("WeatherElectric.ContentPresence.Resources.UserEntries.txt"));
        }
    }
}