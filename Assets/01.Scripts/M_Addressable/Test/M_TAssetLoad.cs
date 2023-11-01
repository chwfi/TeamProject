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
            assetReference.ForEach(asset =>
            {
                PoolManager.Instance.Pop(asset);
            });
        }
    }
}
