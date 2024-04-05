using Discord;

namespace WeatherElectric.ContentPresence.Presence;

internal static class RpcManager
{
    public static Discord.Discord Discord;
    private static ActivityManager _activityManager;
    private static readonly long Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    
    private static Activity _activity;
    
    public static void Init()
    {
        Discord = new global::Discord.Discord(Preferences.DiscordAppId.Value, (ulong)CreateFlags.Default);
        _activityManager = Discord.GetActivityManager();
        _activity = new Activity
        {
            State = "Loading Game...",
            Timestamps =
            {
                Start = Start
            },
            Assets =
            {
                LargeImage = "gamelogo",
                LargeText = "Content Warning"
            },
            Instance = false
        };
        UpdateRpc();
    }

    public static void Dispose()
    {
        Discord.Dispose();
    }

    public static void SetActivity(ActivityField activityField, string value)
    {
        switch (activityField)
        {
            case ActivityField.State:
                _activity.State = value;
                break;
            case ActivityField.Details:
                _activity.Details = value;
                break;
            case ActivityField.LargeImageKey:
                _activity.Assets.LargeImage = value;
                break;
            case ActivityField.LargeImageText:
                _activity.Assets.LargeText = value;
                break;
            case ActivityField.SmallImageKey:
                _activity.Assets.SmallImage = value;
                break;
            case ActivityField.SmallImageText:
                _activity.Assets.SmallText = value;
                break;
            case ActivityField.JoinSecret:
                _activity.Secrets.Join = value;
                break;
            case ActivityField.Party:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            default:
                Plugin.Mls.LogError("Invalid activity field!");
                break;
        }
        UpdateRpc();
    }
    public static void SetActivity(ActivityField activityField, ActivityParty party)
    {
        switch (activityField)
        {
            case ActivityField.State:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            case ActivityField.Details:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            case ActivityField.LargeImageKey:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            case ActivityField.LargeImageText:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            case ActivityField.SmallImageKey:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            case ActivityField.SmallImageText:
                Plugin.Mls.LogError("This error is my fault, called a method wrong.");
                break;
            case ActivityField.Party:
                _activity.Party = party;
                break;
            default:
                Plugin.Mls.LogError("Invalid activity field!");
                break;
        }
        UpdateRpc();
    }

    public enum ActivityField
    {
        State,
        Details,
        LargeImageKey,
        LargeImageText,
        SmallImageKey,
        SmallImageText,
        Party,
        JoinSecret
    }

    public static void UpdateRpc()
    {
        _activityManager.UpdateActivity(_activity, (result) =>
        {
            if (result != Result.Ok)
            {
                Plugin.Mls.LogError($"Failed to set activity: {result.ToString()}");
            }
        });
    }
}