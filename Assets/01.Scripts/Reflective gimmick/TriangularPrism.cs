using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangularPrism : Reflective
{
    public List<TriangluarPlane> _planes;

    protected override void Awake()
    {
        base.Awake();

        _planes.AddRange(transform.GetComponentsInChildren<TriangluarPlane>());
    }
    public override void OnHandleReflected()
    {
        base.OnHandleReflected();

        foreach (var plane in _planes)
        {
            plane.OnHandleReflected();

            plane.SetColor(myReflectData.color);
        }


    }
    public override void UnHandleReflected()
    {
        base.UnHandleReflected();

        foreach (var plane in _planes)
        {
            plane.UnHandleReflected();
        }
    }
    public override void GetReflectedObjectDataModify(ReflectData data)
    {
        foreach (var plane in _planes)
        {
            plane.GetReflectedObjectDataModify(data);
        }
    }
}
