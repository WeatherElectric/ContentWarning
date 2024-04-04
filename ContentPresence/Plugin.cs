using System.Diagnostics;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace WeatherElectric.ContentPresence;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string PluginGuid = "WeatherElectric.SoulWithMae.ContentPresence";
    private const string PluginName = "ContentPresence";
    private const string PluginVersion = "1.0.0";
    
    public static bool DiscordClosed { get; private set; }
    
    public static ManualLogSource Mls { get; private set; }
    
    public static ConfigEntry<long> DiscordAppId;
    public static ConfigEntry<DetailsMode> Mode;
    
    private Harmony _harmony;
    
    private void Awake()
    {
        UserData.Setup();
        _harmony = new Harmony(PluginGuid);
        _harmony.PatchAll(Assembly.GetExecutingAssembly());
        Mls = Logger;
        Logger.LogInfo($"Plugin {PluginGuid} is loaded!");
        DiscordAppId = Config.Bind("Discord", "DiscordAppId", 1225293196914069524, "The application ID that the mod will use.");
        Mode = Config.Bind("Details", "DetailsMode", DetailsMode.Entries, "The mode for the details section. Possible Values: Entries, Casualties, OxygenLeft");
        if (!DiscordOpen()) return;
        RpcManager.Init();
        SceneManager.sceneLoaded += OnSceneWasInitialized;
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

public enum DetailsMode
{
    Entries,
    Casualties,
    OxygenLeft
}