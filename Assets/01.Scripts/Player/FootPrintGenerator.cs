using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class FootPrintGenerator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private AssetReference _prefab;

    private void Awake()
    {
        _inputReader.OnStartFireEvent += FootPrint;
    }

    public void FootPrint()
    {
        PoolableMono previousFootPrint = FindAnyObjectByType<FootPrint>(); //현재 씬에 FootPrint가 있는지 확인하고
        if (previousFootPrint != null) //있으면 제거 후 생성
        {
            PoolManager.Instance.Push(previousFootPrint);
            PoolManager.Instance.Pop(_prefab);
        }
        else
        {
            PoolManager.Instance.Pop(_prefab); //없으면 걍 생성
        }       
    }

    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= FootPrint;
    }
}
