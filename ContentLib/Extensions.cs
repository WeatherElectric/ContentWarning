using System.Linq;
using UnityEngine;

namespace WeatherElectric.ContentLib;

/// <summary>
/// Contains extension methods
/// </summary>
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
    
    /// <summary>
    /// Get a percentage of a given float from a max value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int ToPercent(this float value, float max)
    {
        return Mathf.RoundToInt(value / max * 100f);
    }
}