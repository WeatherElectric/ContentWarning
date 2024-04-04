using System.Reflection;

namespace WeatherElectric.ContentPresence.Internal;

internal static class EmbeddedResource
{
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