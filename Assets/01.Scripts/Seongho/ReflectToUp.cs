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
    public override void GetReflectedObjectDataModify(ReflectData reflectedData)
    {
        Vector3 shootPos = transform.position + (transform.up * transform.localScale.y * .5f);

        SetStartPos(shootPos);

        Color cCol;

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = transform.up;

        var refObj = OnShootRaycast<ReflectiveObject>(shootPos, raycastDirection);

        ChangedReflectObject(refObj);
        refObj?.OnReflectTypeChanged(ReflectState.OnReflect);
        refObj?.GetReflectedObjectDataModify(myReflectData);

        var refToUp = OnShootRaycast<ReflectToUp>(shootPos, raycastDirection);

        ChangedReflectObject(refToUp);
        refToUp?.OnReflectTypeChanged(ReflectState.OnReflect);
        refToUp?.GetReflectedObjectDataModify(myReflectData);

        var refToRef = OnShootRaycast<ReflectToReflect>(shootPos, raycastDirection);

        ChangedReflectObject(refToRef);
        refToRef?.OnReflectTypeChanged(ReflectState.OnReflect);
        refToRef?.GetReflectedObjectDataModify(myReflectData);

        var triPlane = OnShootRaycast<TriangluarPlane>(shootPos, raycastDirection);
        triPlane?.GetReflectedObjectDataModify(myReflectData);


        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(shootPos, raycastDirection);
        door?.ColorMatch(cCol);
    }
}
