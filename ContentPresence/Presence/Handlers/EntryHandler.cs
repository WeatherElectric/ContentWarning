using System;
using System.IO;
using WeatherElectric.ContentPresence.Melon;

namespace WeatherElectric.ContentPresence.Presence.Handlers;

internal static class EntryHandler
{
    private static string GetEntry()
    {
        var rnd = new Random();
        var lines = File.ReadAllLines(UserData.UserEntriesPath);
        var r = rnd.Next(lines.Length);
        return lines[r];
    }

    public static void SetDetails()
    {
        RpcManager.SetActivity(RpcManager.ActivityField.Details, GetEntry());
    }
}