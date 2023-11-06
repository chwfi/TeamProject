using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangularPrism : Reflective
{
    public List<TriangluarPlane> _planes;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < transform.childCount; i++)
        {
            _planes.Add(transform.GetChild(i).GetComponent<TriangluarPlane>());
        }
    }

    public override void SetDataModify(ReflectData data)
    {
        foreach (var plane in _planes)
        {
            plane.SetDataModify(data);
        }
    }
}
