using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

public class M_TAssetLoad : MonoBehaviour
{
    [SerializeField]
    private List<AssetReference> assetReference;

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            StartCoroutine(SpawnTest());
        }
    }

    private IEnumerator SpawnTest()
    {
        //assetReference.ForEach(asset => { PoolManager.Instance.Pop(asset); });
        foreach (var asset in assetReference)
        {
            PoolManager.Instance.Pop(asset);
            yield return new WaitForSeconds(3);
        }
    }
}
