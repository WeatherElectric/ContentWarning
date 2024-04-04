using System;
using System.IO;
using MelonLoader;
using MelonLoader.Utils;
using WeatherElectric.ContentLib;
using WeatherElectric.ContentPresence.Internal;

namespace WeatherElectric.ContentPresence.Melon;

internal static class UserData
{
    private static readonly string UserDataDirectory = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric/Content Presence");
    private static readonly string DllPath = Path.Combine(UserDataDirectory, "discord_game_sdk.dll");
    public static readonly string UserEntriesPath = Path.Combine(UserDataDirectory, "UserEntries.txt");
    
    private static bool _hasLoadedLib;
    private static IntPtr _rpcLib;
    
    public static void Setup()
    {
        if (!Directory.Exists(UserDataDirectory))
        {
            ModConsole.Msg($"User data directory not found, creating at {UserDataDirectory}", 1);
            Directory.CreateDirectory(UserDataDirectory);
        }
        if (!File.Exists(DllPath))
        {
            ModConsole.Msg($"Discord SDK not unpacked", 1);
            File.WriteAllBytes(DllPath, HelperMethods.GetResourceBytes(Main.CurrAsm,"WeatherElectric.ContentPresence.Resources.discord_game_sdk.dll"));
        }
        if (!File.Exists(UserEntriesPath))
        {
            ModConsole.Msg($"User entries file not unpacked", 1);
            File.WriteAllBytes(UserEntriesPath, HelperMethods.GetResourceBytes(Main.CurrAsm,"WeatherElectric.ContentPresence.Resources.UserEntries.txt"));
        }
        if (!_hasLoadedLib)
        {
            ModConsole.Msg($"Loading Discord SDK from {DllPath}", 1);
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