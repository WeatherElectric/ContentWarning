namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class PlayerFaceHandler
{
    public static string Face { get; private set; }
    private static PlayerVisor _playerVisor;
    
    [HarmonyPatch(typeof(PlayerVisor), "Start")]
    public class PlayerVisor_Start
    {
        public static void Postfix(PlayerVisor __instance)
        {
            if (!GetPlayer(__instance).IsLocal) return;
            _playerVisor = __instance;
        }
    }
    
    [HarmonyPatch(typeof(PlayerVisor), "LoadFaceFromPlayerPrefs")]
    public class PlayerVisor_LoadFaceFromPlayerPrefs
    {
        public static void Postfix(PlayerVisor __instance)
        {
            if (!GetPlayer(__instance).IsLocal) return;
            Face = __instance.visorFaceText.text;
        }
    }
    
    [HarmonyPatch(typeof(PlayerCustomizer), "SetFaceText")]
    public class PlayerCustomizer_SetFaceText
    {
        public static void Postfix(PlayerCustomizer __instance)
        {
            if (!__instance.playerInTerminal.IsLocal) return;
            SetFace();
        }
    }
    
    [HarmonyPatch(typeof(PlayerCustomizer), "OnApply")]
    public class PlayerCustomizer_OnApply
    {
        public static void Postfix(PlayerCustomizer __instance)
        {
            if (!__instance.playerInTerminal.IsLocal) return;
            SetFace();
        }
    }
    
    [HarmonyPatch(typeof(PlayerCustomizer), "OnQuit")]
    public class PlayerCustomizer_OnQuit
    {
        public static void Postfix(PlayerCustomizer __instance)
        {
            SetFace();
        }
    }
    
    private static void SetFace()
    {
        Face = _playerVisor.visorFaceText.text;
        switch (SceneManager.GetActiveScene().name)
        {
            case SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.State,
                    $"{Face} | At Home | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                break;
            case SceneNames.Factory:
                RpcManager.SetActivity(RpcManager.ActivityField.State,
                    $"{Face} | Down In The Factory | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                break;
            case SceneNames.Harbor:
                RpcManager.SetActivity(RpcManager.ActivityField.State,
                    $"{Face} | Down In The Harbor | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                break;
        }
    }
    
    private static Player GetPlayer(PlayerVisor visor)
    {
        Type type = visor.GetType();
        FieldInfo privateFieldInfo = type.GetField("m_player", BindingFlags.Instance | BindingFlags.NonPublic);
        if (privateFieldInfo != null) return (Player)privateFieldInfo.GetValue(visor);
        return null;
    }
}