using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ReflectiveObject : Reflective
{
    public override void SetDataModify(ReflectData data)
    {
        var cCol = ColorSystem.GetColorCombination(data.color, defaultColor);
        _lr.material.color = cCol;

        var raycastDirection = Vector3.Reflect(data.direction, data.normal);
        myReflectData.direction = raycastDirection;

        OnShootRaycast(data, raycastDirection);
    }

    public override void OnHandleReflected()
    {
        base.OnHandleReflected();
        Debug.Log(gameObject.name + " : on");
    }

    public override void UnHandleReflected()
    {
        base.UnHandleReflected();

        Debug.Log(gameObject.name + " : un");
    }
}
