using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Stalactite : PoolableMono
{
    [SerializeField]
    private float fallingTime;
    [SerializeField]
    private bool isDisappearObj; //����� �������̶�� true
    [SerializeField]
    private float disappearTime; //����� �������̶�� �� �� �Ŀ� �������

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Init()
    {
        StartCoroutine(FallingCoroutine());
    }

    private IEnumerator FallingCoroutine()
    {
        yield return new WaitForSeconds(fallingTime);

        _rigidbody.useGravity = true;

        if (isDisappearObj) { StartCoroutine(DisappearCoroutine()); }
    }

    private IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(disappearTime);
        PoolManager.Instance.Push(this);
    }
}
