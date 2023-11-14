using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangularPrism : Reflective
{
    public List<TriangluarPlane> _planes;

    private TriangluarPlane _currentPlane;
    protected override void Awake()
    {
        base.Awake();

        _planes.AddRange(transform.GetComponentsInChildren<TriangluarPlane>());
    }
    public void SetTriangluarPlane(TriangluarPlane plane)
    {
        _currentPlane = plane;
    }
    public override void OnHandleReflected()
    {
        //base.OnHandleReflected();

        foreach (var plane in _planes)
        {
            if (_currentPlane != plane) return;

            plane.OnHandleReflected();

            plane.SetColor(myReflectData.color);
        }
    }


    public override void UnHandleReflected()
    {
        //base.UnHandleReflected();

        foreach (var plane in _planes)
        {
            if (_currentPlane != plane) return;

            plane.UnHandleReflected();
        }
    }
    public override void GetReflectedObjectDataModify(ReflectData data)
    {
        foreach (var plane in _planes)
        {
            if (_currentPlane != plane) return;

            plane.GetReflectedObjectDataModify(data);
        }
    }
}
