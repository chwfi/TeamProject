using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr Instance;

    [SerializeField] private AssetReference _levelRef;
    private List<GameObject> _list = new();

    private void Awake()
    {
        if (Instance != null) Debug.LogError("SceneMgr ÀÖ¾î¿ë");
        Instance = this;
    }

    private async void LoadLevel()
    {
        if (!_levelRef.IsValid())
        {
            await _levelRef.LoadAssetAsync<GameObject>().Task;
        }
        var obj = Instantiate(_levelRef.Asset, Vector3.zero, Quaternion.identity) as GameObject;
        _list.Add(obj);
    }
}
