using System.Diagnostics;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace WeatherElectric.ContentPresence;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string PluginGuid = "WeatherElectric.SoulWithMae.ContentPresence";
    private const string PluginName = "ContentPresence";
    private const string PluginVersion = "1.1.0";
    
    public static bool DiscordClosed { get; private set; }
    public static ManualLogSource Mls { get; private set; }
    
    private void Awake()
    {
        Mls = Logger;
        Logger.LogInfo($"Plugin {PluginGuid} is loaded!");
        
        Preferences.Config = Config;
        Preferences.Setup();
        UserData.Setup();
        
        if (!DiscordOpen()) return;
        InitialPrefCheck();
        RpcManager.Init();
        PatchAll();
        SceneManager.sceneLoaded += OnSceneWasInitialized;
        Preferences.Mode.SettingChanged += OnDetailModeChange;
    }

    private static void PatchAll()
    {
        PlayerFaceHandler.Patch();
        FilmHandler.Patch();
        QuotaHandler.Patch();
        Objects.Patch();
    }

    private static void InitialPrefCheck()
    {
        switch (Preferences.Mode.Value)
        {
            case Preferences.DetailsMode.Entries:
                break;
            case Preferences.DetailsMode.Casualties:
                DeathHandler.Patch();
                break;
            case Preferences.DetailsMode.OxygenLeft:
                OxygenHandler.Patch();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void OnDetailModeChange(object obj, EventArgs eventArgs)
    {
        switch (Preferences.Mode.Value)
        {
            case Preferences.DetailsMode.Entries:
                DeathHandler.Unpatch();
                OxygenHandler.Unpatch();
                EntryHandler.SetDetails();
                break;
            case Preferences.DetailsMode.Casualties:
                DeathHandler.Patch();
                OxygenHandler.Unpatch();
                DeathHandler.PrefsChanged();
                break;
            case Preferences.DetailsMode.OxygenLeft:
                DeathHandler.Unpatch();
                OxygenHandler.Patch();
                OxygenHandler.PrefsChanged();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnApplicationQuit()
    {
        if (DiscordClosed) return;
        RpcManager.Dispose();
        UserData.Dispose();
    }

    private static void OnSceneWasInitialized(Scene scene, LoadSceneMode mode)
    {
        if (DiscordClosed) return;
        LevelHandler.OnLevelLoad(scene.name);
    }

    private void Update()
    {
        if (DiscordClosed) return;
        RpcManager.Discord.RunCallbacks();
        FilmHandler.Update();
    }
    
    private static bool DiscordOpen()
    {
        var discord = Process.GetProcessesByName("discord");
        var discordcanary = Process.GetProcessesByName("discordcanary");
        var discordptb = Process.GetProcessesByName("discordptb");
        if (discordcanary.Length <= 0 && discord.Length <= 0 && discordptb.Length <= 0)
        {
            Mls.LogError("Neither Discord, Discord Canary, or Discord PTB are running!");
            DiscordClosed = true;
            return false;
        }
        if ((discord.Length > 0 && discordcanary.Length > 0) || (discord.Length > 0 && discordptb.Length > 0) || (discordcanary.Length > 0 && discordptb.Length > 0))
        {
            Mls.LogError("You have 2 Discords open! Discord may struggle to pick one, and it may not work! Please close one and restart!");
        }
        return true;
    }
}