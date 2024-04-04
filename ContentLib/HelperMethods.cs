using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WeatherElectric.ContentLib.Melon;

namespace WeatherElectric.ContentLib;

/// <summary>
/// Contains helper methods
/// </summary>
public static class HelperMethods
{
    /// <summary>
    /// Loads an embedded assetbundle
    /// </summary>
    public static AssetBundle LoadEmbeddedAssetBundle(Assembly assembly, string name)
    {
        string[] manifestResources = assembly.GetManifestResourceNames();
        AssetBundle bundle = null;
        if (manifestResources.Contains(name))
        {
            ModConsole.Msg($"Loading embedded resource data {name}...", 1);
            using Stream str = assembly.GetManifestResourceStream(name);
            using MemoryStream memoryStream = new MemoryStream();

            str?.CopyTo(memoryStream);
            ModConsole.Msg("Done!", 1);
            byte[] resource = memoryStream.ToArray();

            ModConsole.Msg($"Loading assetBundle from data {name}, please be patient...", 1);
            bundle = AssetBundle.LoadFromMemory(resource);
            ModConsole.Msg("Done!", 1);
        }
        return bundle;
    }
    
    /// <summary>
    /// Gets the raw bytes of an embedded resource
    /// </summary>
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
    
    ///<summary>
    /// Checks if an assembly is loaded from name
    /// </summary>
    public static bool CheckIfAssemblyLoaded(string name)
    {
        return AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name.ToLower().Contains(name.ToLower()));
    }
}