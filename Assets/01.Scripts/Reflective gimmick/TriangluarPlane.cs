using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class TriangluarPlane : Reflective //�ﰢ���� �� ��
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

        Reflective obj = OnShootRaycast<Reflective>(transform.position, raycastDirection); //��, �츮 �� �� ���� �������?

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);

        obj?.GetReflectedObjectDataModify(myReflectData);

        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(transform.position, raycastDirection);

        door?.ColorMatch(myReflectData.color);
    }
}
