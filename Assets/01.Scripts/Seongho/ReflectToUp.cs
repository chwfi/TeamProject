using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ReflectToUp : Reflective
{
    public override void OnHandleReflected()
    {
        base.OnHandleReflected();
    }
    public override void UnHandleReflected()
    {
        base.UnHandleReflected();
    }
    public override void OnDeflected(ReflectData reflectedData)
    {
        Vector3 shootPos = transform.position + (transform.up * transform.localScale.y * .5f);

        SetStartPos(shootPos);

        Color cCol;

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = transform.up;

        var obj = OnShootRaycast<Reflective>(shootPos, raycastDirection);
        ChangedReflectObject(obj);
        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.OnDeflected(myReflectData);

        var triPlane = OnShootRaycast<TriangluarPlane>(shootPos, raycastDirection);
        triPlane?.OnDeflected(myReflectData);


        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(shootPos, raycastDirection);
        door?.ColorMatch(cCol, this);
    }
}
