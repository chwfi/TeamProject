using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;

public class FootPrint : PoolableMono
{
    public override void Init()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        transform.position = new Vector3(PlayerTrm.position.x, PlayerTrm.position.y - 0.05f, PlayerTrm.position.z);
        transform.rotation = Quaternion.Euler(90, PlayerTrm.position.y, 0);
        yield break;
    }
}
