using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectToReflect : Reflective
{
    public override void OnDeflected(ReflectData data)
    {
        Vector3 shootPos = data.hitPos;
        SetStartPos(shootPos);

        Color cCol;

        cCol = ColorSystem.GetColorCombination(data.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = Vector3.Reflect(data.direction, data.normal);

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
