using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class TriangluarPlane : Reflective //삼각형의 각 면
{
    public override void OnHandleReflected()
    {
        base.OnHandleReflected();
    }
    public override void UnHandleReflected()
    {
        base.UnHandleReflected();
    }
    public void SetColor(Color color)
    {
        SetLightColor(color);
    }

    public void SetDataModify(ReflectData reflectedData, TriangluarPlane comp)
    {
        GetReflectedObjectDataModify(reflectedData);
    }
    public override void GetReflectedObjectDataModify(ReflectData reflectedData)
    {
        SetStartPos(transform.position);

        Color cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = transform.up;

        Reflective obj = OnShootRaycast<Reflective>(transform.position, raycastDirection); //자, 우리 한 번 빛을 쏴볼까요?

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);

        obj?.GetReflectedObjectDataModify(myReflectData);

        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(transform.position, raycastDirection);

        door?.ColorMatch(myReflectData.color);
    }
}
