using System.IO;

namespace WeatherElectric.ContentPresence.Bepin;

internal static class UserData
{
    private static readonly string UserDataDirectory = Path.Combine(BepInEx.Paths.ConfigPath, "Weather Electric/Content Presence");
    private static readonly string DllPath = Path.Combine(UserDataDirectory, "discord_game_sdk.dll");
    public static readonly string UserEntriesPath = Path.Combine(UserDataDirectory, "UserEntries.txt");
    
    private static bool _hasLoadedLib;
    private static IntPtr _rpcLib;
    
    public static void Setup()
    {
        if (!Directory.Exists(UserDataDirectory))
        {
            Directory.CreateDirectory(UserDataDirectory);
        }
        if (!File.Exists(DllPath))
        {
            File.WriteAllBytes(DllPath, HelperMethods.GetResourceBytes("WeatherElectric.ContentPresence.Resources.discord_game_sdk.dll"));
        }
        if (!File.Exists(UserEntriesPath))
        {
            File.WriteAllBytes(UserEntriesPath, HelperMethods.GetResourceBytes("WeatherElectric.ContentPresence.Resources.UserEntries.txt"));
        }
        if (!_hasLoadedLib)
        {
            _rpcLib = DllTools.LoadLibrary(DllPath);
            _hasLoadedLib = true;
        }
    }
    
    public static void Dispose()
    {
        if (_hasLoadedLib)
        {
            DllTools.FreeLibrary(_rpcLib);
            _hasLoadedLib = false;
        }
    }
}