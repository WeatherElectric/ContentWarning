using WeatherElectric.ContentLib;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

public class LevelHandler
{
    public static void OnLevelLoad(string levelName)
    {
        DeathHandler.Reset();
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