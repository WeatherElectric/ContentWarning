using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace WeatherElectric.ContentLib;

/// <summary>
/// Stores various instances of objects
/// </summary>
public static class Objects
{
    /// <summary>
    /// The current video info entry. This is normally a private field, so it's stored here for easy access
    /// </summary>
    public static VideoInfoEntry VideoInfoEntry { get; private set; }
    /// <summary>
    /// The current video camera
    /// </summary>
    public static VideoCamera VideoCamera { get; private set; }
    /// <summary>
    /// The local player.
    /// </summary>
    public static Player LocalPlayer { get; private set; }
    /// <summary>
    /// Every player, including the local player
    /// </summary>
    public static List<Player> AllPlayers { get; private set; } = [];
    /// <summary>
    /// Every player that is not the local player
    /// </summary>
    public static List<Player> NetworkedPlayers { get; private set; } = [];
    /// <summary>
    /// The current scene.
    /// </summary>
    public static string CurrentScene { get; private set; }
    
    internal static void SetCurrentScene(string scene)
    {
        CurrentScene = scene;
    }
    
    [HarmonyPatch(typeof(VideoCamera), "Start")]
    internal class VideoCamera_Start
    {
        public static void Postfix(VideoCamera __instance)
        {
            VideoCamera = __instance;
            Type type = __instance.GetType();
            FieldInfo privateFieldInfo = type.GetField("m_recorderInfoEntry", BindingFlags.Instance | BindingFlags.NonPublic);
            if (privateFieldInfo != null) VideoInfoEntry = (VideoInfoEntry)privateFieldInfo.GetValue(__instance);
        }
    }
    
    [HarmonyPatch(typeof(Player), "Start")]
    internal class Player_Start
    {
        public static void Postfix(Player __instance)
        {
            if (__instance.IsLocal) LocalPlayer = __instance;
            AllPlayers.Add(__instance);
            if (!__instance.IsLocal) NetworkedPlayers.Add(__instance);
        }
    }
    
}