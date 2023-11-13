using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangularPrism : Reflective
{
    public List<TriangluarPlane> _planes;

    private TriangluarPlane _plane;
    protected override void Awake()
    {
        base.Awake();

        _planes.AddRange(transform.GetComponentsInChildren<TriangluarPlane>());
    }
    public void SetTriangluarPlane(TriangluarPlane plane)
    {
        //if (_plane == plane) return;
        _plane = plane;
    }
    public override void OnHandleReflected()
    {
        base.OnHandleReflected();

        foreach (var plane in _planes)
        {
            if (_plane == plane) return;

            plane.OnHandleReflected();

            plane.SetColor(myReflectData.color);
        }
    }


    public override void UnHandleReflected()
    {
        base.UnHandleReflected();

        foreach (var plane in _planes)
        {
            if (_plane == plane) return;

            plane.UnHandleReflected();
        }
    }
    public override void GetReflectedObjectDataModify(ReflectData data)
    {
        foreach (var plane in _planes)
        {
            if (_plane == plane) return;

            plane.SetDataModify(data, plane);
        }
    }
}
