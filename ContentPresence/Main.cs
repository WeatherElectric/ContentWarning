using System.Diagnostics;
using MelonLoader;
using WeatherElectric.ContentPresence.Melon;
using WeatherElectric.ContentPresence.Presence;

namespace WeatherElectric.ContentPresence;

// most of this code is just yoinked from my bonelab rich presence mod lol
// why completely remake something when you've already done the base part before
public class Main : MelonMod
{
    internal const string ModName = "ContentPresence";
    internal const string ModVersion = "1.0.0";
    internal const string ModAuthor = "Weather Electric";
    
    public static bool DiscordClosed;

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        if (!DiscordOpen()) return;
        UserData.Setup();
        RpcManager.Init();
    }
    
    public override void OnApplicationQuit()
    {
        if (DiscordClosed) return;
        RpcManager.Dispose();
        UserData.Dispose();
    }

    public override void OnUpdate()
    {
        if (DiscordClosed) return;
        RpcManager.Discord.RunCallbacks();
    }
    
    private static bool DiscordOpen()
    {
        var discord = Process.GetProcessesByName("discord");
        var discordcanary = Process.GetProcessesByName("discordcanary");
        var discordptb = Process.GetProcessesByName("discordptb");
        if (discordcanary.Length <= 0 && discord.Length <= 0 && discordptb.Length <= 0)
        {
            ModConsole.Error("Neither Discord, Discord Canary, or Discord PTB are running!");
            DiscordClosed = true;
            return false;
        }
        if ((discord.Length > 0 && discordcanary.Length > 0) || (discord.Length > 0 && discordptb.Length > 0) || (discordcanary.Length > 0 && discordptb.Length > 0))
        {
            ModConsole.Error("You have 2 Discords open! Discord may struggle to pick one, and it may not work! Please close one and restart!");
        }
        return true;
    }
}