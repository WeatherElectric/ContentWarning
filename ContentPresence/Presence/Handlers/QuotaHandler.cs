namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class QuotaHandler
{
    public static float FulfilledQuota { get; private set; }
    public static float TotalQuota { get; private set; }

    [HarmonyPatch(typeof(SurfaceNetworkHandler), "OnRoomPropertiesUpdate")]
    public class SurfaceNetworkHandler_OnRoomPropertiesUpdate
    {
        public static void Postfix(SurfaceNetworkHandler __instance)
        {
            FulfilledQuota = BigNumbers.GetScoreToViews(SurfaceNetworkHandler.RoomStats.CurrentQuota, GameAPI.CurrentDay);
            TotalQuota = BigNumbers.GetScoreToViews(SurfaceNetworkHandler.RoomStats.QuotaToReach, GameAPI.CurrentDay);

            switch (SceneManager.GetActiveScene().name)
            {
                case SceneNames.Home:
                    RpcManager.SetActivity(RpcManager.ActivityField.State,
                        $"{PlayerFaceHandler.Face} | At Home | {FulfilledQuota.AutoRound()} of {TotalQuota.AutoRound()} Views");
                    break;
                case SceneNames.Factory:
                    RpcManager.SetActivity(RpcManager.ActivityField.State,
                        $"{PlayerFaceHandler.Face} | Down In The Factory | {FulfilledQuota.AutoRound()} of {TotalQuota.AutoRound()} Views");
                    break;
                case SceneNames.Harbor:
                    RpcManager.SetActivity(RpcManager.ActivityField.State,
                        $"{PlayerFaceHandler.Face} | Down In The Harbor | {FulfilledQuota.AutoRound()} of {TotalQuota.AutoRound()} Views");
                    break;
            }
        }
    }
}