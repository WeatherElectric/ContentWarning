namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal class LevelHandler
{
    public static void OnLevelLoad(string levelName)
    {
        if (Plugin.DiscordClosed) return;
        switch (Preferences.Mode.Value)
        {
            case Preferences.DetailsMode.OxygenLeft when levelName == SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "Around trees. Oxygen fine!");
                break;
            case Preferences.DetailsMode.OxygenLeft when levelName == SceneNames.MainMenu:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "You don't need oxygen in the menu!");
                break;
            case Preferences.DetailsMode.Casualties when levelName == SceneNames.MainMenu:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "You can't die in the menu!");
                DeathHandler.Reset();
                break;
            case Preferences.DetailsMode.Entries:
                EntryHandler.SetDetails();
                break;
            case Preferences.DetailsMode.Casualties:
                DeathHandler.Reset();
                break;
            default:
                Plugin.Mls.LogError("Invalid Details Mode!");
                break;
        }
        
        switch (levelName)
        {
            case SceneNames.MainMenu:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "");
                RpcManager.SetActivity(RpcManager.ActivityField.State, "In Main Menu");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "gamelogo");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "Main Menu");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, "");
                break;
            case SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.State, $"{PlayerFaceHandler.Face} | At Home | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "home");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, $"{PlayerFaceHandler.Face} | Home | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, "");
                break;
            case SceneNames.Factory:
                RpcManager.SetActivity(RpcManager.ActivityField.State, $"{PlayerFaceHandler.Face} | Down In The Factory | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "factory");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, $"{PlayerFaceHandler.Face} | The Old World | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, "");
                break;
            case SceneNames.Harbor:
                RpcManager.SetActivity(RpcManager.ActivityField.State, $"{PlayerFaceHandler.Face} | Down In The Harbor | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "harbor");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, $"{PlayerFaceHandler.Face} | The Old World | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, "");
                break;
            default:
                RpcManager.SetActivity(RpcManager.ActivityField.State, $"In Unknown Scene: {levelName}");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "gamelogo");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "Unknown Scene");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageKey, "notrecording");
                RpcManager.SetActivity(RpcManager.ActivityField.SmallImageText, "");
                break;
        }
    }
}