using System.Globalization;
using UnityEngine;
namespace WeatherElectric.ContentPresence.Presence.Helpers;

internal static class HelperMethods
{
    public static string AutoRound(this float num)
    {
        return num >= 1000 ? $"{Math.Round(num / 1000, 2)}k" : num.ToString(CultureInfo.CurrentCulture);
    }
    
    public static int ToPercent(this float value, float max)
    {
        return Mathf.RoundToInt(value / max * 100f);
    }
    
    public static byte[] GetResourceBytes(string filename)
    {
        var assembly = Assembly.GetExecutingAssembly();
        foreach (var resource in assembly.GetManifestResourceNames())
        {
            if (resource.Contains(filename))
            {
                using var resFilestream = assembly.GetManifestResourceStream(resource);
                if (resFilestream == null) return null;
                var ba = new byte[resFilestream.Length];
                _ = resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
        return null;
    }
}