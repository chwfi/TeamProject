using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define.Define;

public class FootPrint : PoolableMono
{
    public override void Init()
    {
        GenerateFootprint();
    }

    private void GenerateFootprint()
    {
        transform.position = new Vector3(PlayerTrm.position.x, PlayerTrm.position.y - 0.05f, PlayerTrm.position.z);
        Vector3 lookDirection = new Vector3(-PlayerTrm.forward.x, 90, -PlayerTrm.forward.z).normalized;

        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
