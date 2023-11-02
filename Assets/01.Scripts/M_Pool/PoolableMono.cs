using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class PoolableMono : MonoBehaviour
{
    [HideInInspector]
    public string AssetGUID;
    public abstract void Init();
}
