using WeatherElectric.ContentLib;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

public class LevelHandler
{
    public static void OnLevelLoad(string levelName)
    {
        switch (levelName)
        {
            case SceneNames.MainMenu:
                RpcManager.SetActivity(RpcManager.ActivityField.State, "In Main Menu");
                break;
            case SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.State, "At Home");
                break;
            case SceneNames.Factory:
                RpcManager.SetActivity(RpcManager.ActivityField.State, "Down In The Old World, at the Factory");
                break;
        }
    }
}