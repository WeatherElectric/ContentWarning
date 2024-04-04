using System.Reflection;

namespace WeatherElectric.ContentLib;

public static class HelperMethods
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