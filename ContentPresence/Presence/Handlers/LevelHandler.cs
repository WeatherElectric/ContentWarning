using WeatherElectric.ContentLib;
using WeatherElectric.ContentPresence.Melon;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal class LevelHandler
{
    public static void OnLevelLoad(string levelName)
    {
        if (Main.DiscordClosed) return;
        switch (Preferences.DetailsMode.Value)
        {
            case DetailsMode.OxygenLeft when levelName == SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "Around trees. Oxygen fine!");
                break;
            case DetailsMode.OxygenLeft when levelName == SceneNames.MainMenu:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "You don't need oxygen in the menu!");
                break;
            case DetailsMode.Entries:
                EntryHandler.SetDetails();
                break;
            case DetailsMode.Casualties:
                DeathHandler.Reset();
                break;
            default:
                ModConsole.Error("Invalid Details Mode!");
                break;
        }
        
        switch (levelName)
        {
            case SceneNames.MainMenu:
                RpcManager.SetActivity(RpcManager.ActivityField.Details, "");
                RpcManager.SetActivity(RpcManager.ActivityField.State, "In Main Menu");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "gamelogo");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "Main Menu");
                break;
            case SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.State, "At Home");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "home");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "Home");
                break;
            case SceneNames.Factory:
                RpcManager.SetActivity(RpcManager.ActivityField.State, "Down In The Factory");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "factory");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "The Old World");
                break;
            case SceneNames.Harbor:
                RpcManager.SetActivity(RpcManager.ActivityField.State, "Down In The Harbor");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "harbor");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "The Old World");
                break;
            default:
                RpcManager.SetActivity(RpcManager.ActivityField.State, $"In Unknown Scene: {levelName}");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageKey, "gamelogo");
                RpcManager.SetActivity(RpcManager.ActivityField.LargeImageText, "Unknown Scene");
                break;
        }
    }
}