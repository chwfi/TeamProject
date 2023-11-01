using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : PoolableMono
{
    [SerializeField]
    private float fallingTime;

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


    }
}
