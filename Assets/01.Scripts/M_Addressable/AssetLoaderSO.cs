using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

[Serializable]
public struct PoolingItem
{
    public AssetReference assetRef;
    public ushort count;
}

[CreateAssetMenu(menuName = "SO/AddressableAssets")]
public class AssetLoaderSO : ScriptableObject
{
    public int TotalCount => PoolingList.Count;

    public List<PoolingItem> PoolingList;

    private Dictionary<string, AssetReference> _nameDictionary;
    private Dictionary<string, AssetReference> _guidDictionary;

    private void OnEnable()
    {
        _nameDictionary = new();
        _guidDictionary = new();
    }

    public void LoadingComplete(AssetReference reference, string name)
    {
        _guidDictionary.Add(reference.AssetGUID, reference);
        _nameDictionary.Add(name, reference);
    }

    public Object GetAsset(string guid)
    {
        if (_guidDictionary.TryGetValue(guid, out AssetReference value))
        {
            return value.Asset;
        }
        return null;
    }

    public Object GetAssetByName(string name)
    {
        if (_nameDictionary.TryGetValue(name, out AssetReference value))
        {
            return value.Asset;
        }
        return null;
    }
}
