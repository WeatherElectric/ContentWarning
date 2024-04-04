using WeatherElectric.ContentLib;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class OxygenHandler
{
    public static void Update()
    {
        if (Objects.CurrentScene != SceneNames.MainMenu && Objects.CurrentScene != SceneNames.Home)
        {
            RpcManager.SetActivity(RpcManager.ActivityField.Details, $"Oxygen Left: {Objects.LocalPlayer.data.remainingOxygen.ToPercent(Objects.LocalPlayer.data.maxOxygen)}%");
        }
    }
}