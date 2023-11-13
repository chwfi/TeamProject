using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TriangluarPlane : Reflective //�ﰢ���� �� ��
{
    public override void GetReflectedObjectDataModify(ReflectData reflectedData)
    {
        Color cCol;

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = transform.up;

        var obj = OnShootRaycast<Reflective>(transform.position, raycastDirection); //��, �츮 �� �� ���� �������?

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);

        obj?.GetReflectedObjectDataModify(myReflectData);

        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(transform.position, raycastDirection);

        door?.ColorMatch(myReflectData.color);
    }
}
