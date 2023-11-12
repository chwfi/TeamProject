using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RdfResources
{
    private static Dictionary<string, Object> ResCashes = new();

    public static T Load<T>(string path) where T : Object
    {
        if (ResCashes.ContainsKey(path) == false)
            ResCashes[path] = Resources.Load<T>(path);

        return ResCashes[path] as T;
    }

    public static void ClearResourceCashes()
    {
        ResCashes.Clear();
        Resources.UnloadUnusedAssets();
    }
}
