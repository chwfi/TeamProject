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
        PoolableMono previousFootPrint = FindAnyObjectByType<FootPrint>(); //���� ���� FootPrint�� �ִ��� Ȯ���ϰ�
        if (previousFootPrint != null) //������ ���� �� ����
        {
            PoolManager.Instance.Push(previousFootPrint);
            PoolManager.Instance.Pop(_prefab);
        }
        else
        {
            PoolManager.Instance.Pop(_prefab); //������ �� ����
        }       
    }

    private void OnDestroy()
    {
        _inputReader.OnStartFireEvent -= FootPrint;
    }
}
