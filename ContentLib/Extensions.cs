using System.Linq;
using UnityEngine;

namespace WeatherElectric.ContentLib;

public static class Extensions
{
    /// <summary>
    /// Loads an asset from an assetbundle
    /// </summary>
    public static T LoadPersistentAsset<T>(this AssetBundle assetBundle, string name) where T : Object
    {
        Object asset = assetBundle.LoadAsset(name);

        if (asset != null)
        {
            asset.hideFlags = HideFlags.DontUnloadUnusedAsset;
            return asset as T;
        }

        return null;
    }
    
    /// <summary>
    /// Get a random element from a list
    /// </summary>
    public static T GetRandom<T>(this System.Collections.Generic.List<T> list)
    {
        int random = Random.Range(0, list.Count);
        return list.ElementAt(random);
    }
}