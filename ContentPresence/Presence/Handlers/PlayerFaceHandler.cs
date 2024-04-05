using System.Collections;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class PlayerFaceHandler
{
    public static string Face { get; private set; }
    private static PlayerVisor _playerVisor;

    public static void Patch()
    {
        Plugin.Mls.LogInfo("Patching PlayerFaceHandler...");
        On.PlayerVisor.Start += OnVisorStart;
        On.PlayerCustomizer.SetFaceText += OnCustomizerSet;
        On.PlayerCustomizer.OnApply += OnCustomizerApply;
        On.PlayerCustomizer.OnQuit += OnCustomizerQuit;
    }
    
    private static IEnumerator OnVisorStart(On.PlayerVisor.orig_Start orig, PlayerVisor self)
    {
        yield return orig(self);
        if (GetPlayer(self).IsLocal) _playerVisor = self;
        SetFace();
    }
    
    private static void OnCustomizerSet(On.PlayerCustomizer.orig_SetFaceText orig, PlayerCustomizer self, string text)
    {
        orig(self, text);
        if (!self.playerInTerminal.IsLocal) return;
        SetFaceSpecial(text);
    }
    
    private static void OnCustomizerApply(On.PlayerCustomizer.orig_OnApply orig, PlayerCustomizer self)
    {
        if (self.playerInTerminal.IsLocal) SetFace();
        orig(self);
    }
    
    private static void OnCustomizerQuit(On.PlayerCustomizer.orig_OnQuit orig, PlayerCustomizer self)
    {
        if (self.playerInTerminal.IsLocal) ResetFace();
        orig(self);
    }

    private static void SetFaceSpecial(string face)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case SceneNames.Home:
                RpcManager.SetActivity(RpcManager.ActivityField.State,
                    $"{face} | At Home | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                break;
            case SceneNames.Factory:
                RpcManager.SetActivity(RpcManager.ActivityField.State,
                    $"{face} | Down In The Factory | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                break;
            case SceneNames.Harbor:
                RpcManager.SetActivity(RpcManager.ActivityField.State,
                    $"{face} | Down In The Harbor | {QuotaHandler.FulfilledQuota.AutoRound()} of {QuotaHandler.TotalQuota.AutoRound()} Views");
                break;
        }
    }

    private static void ResetFace()
    {
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