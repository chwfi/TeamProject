using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyUP : Reflective
{
    public override void SetDataModify(ReflectData inData)
    {
        OnShootRaycast(inData, Vector3.up); // ������ ���θ�
    }

    public override void OnHandleReflected()
    {
        base.OnHandleReflected();
    }
    public override void UnHandleReflected()
    {
        base.UnHandleReflected();
    }
}
