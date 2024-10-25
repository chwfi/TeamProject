using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReflectiveObject : Reflective
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
        Vector3 shootPos = reflectedData.hitPos;
        SetStartPos(shootPos);

        Color cCol;

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = Vector3.Reflect(reflectedData.direction, reflectedData.normal);

        var obj = OnShootRaycast<Reflective>(shootPos, raycastDirection);
        ChangedReflectObject(obj);
        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.OnDeflected(myReflectData);

        var triPlane = OnShootRaycast<TriangluarPlane>(shootPos, raycastDirection);
        triPlane?.OnDeflected(myReflectData);


        var door = OnShootRaycast<DoorOpenTrigger>(shootPos, raycastDirection);
        door?.ColorMatch(cCol, this);
    }

   
}