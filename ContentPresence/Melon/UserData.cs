using System;
using System.IO;
using MelonLoader;
using MelonLoader.Utils;
using WeatherElectric.ContentPresence.Internal;

namespace WeatherElectric.ContentPresence.Melon;

internal static class UserData
{
    private static readonly string UserDataDirectory = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric/Content Presence");
    private static readonly string LegacyDirectory = Path.Combine(MelonEnvironment.UserDataDirectory, "Content Presence");
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
            ModConsole.Msg($"Discord SDK not unpacked, checking legacy path", 1);
            if (Directory.Exists(LegacyDirectory) && File.Exists(Path.Combine(LegacyDirectory, "discord_game_sdk.dll")))
            {
                File.Move(Path.Combine(LegacyDirectory, "discord_game_sdk.dll"), DllPath);
            }
            else
            {
                ModConsole.Msg($"Legacy path not found, creating at {DllPath}", 1);
                File.WriteAllBytes(DllPath, EmbeddedResource.GetResourceBytes("discord_game_sdk.dll"));
            }
        }
        if (!File.Exists(UserEntriesPath))
        {
            ModConsole.Msg($"User entries file not unpacked, checking legacy path", 1);
            if (Directory.Exists(LegacyDirectory) && File.Exists(Path.Combine(LegacyDirectory, "UserEntries.txt")))
            {
                var entries = Path.Combine(LegacyDirectory, "UserEntries.txt");
                File.Move(entries, UserEntriesPath);
            }
            else
            {
                ModConsole.Msg($"Legacy path not found, creating at {UserEntriesPath}", 1);
                File.WriteAllBytes(UserEntriesPath, EmbeddedResource.GetResourceBytes("UserEntries.txt"));
            }
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