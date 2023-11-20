using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class TriangluarPlane : Reflective //�ﰢ���� �� ��
{
    public List<TriangluarPlane> _planes = new();
    public override void OnHandleReflected()
    {
        foreach (var plane in _planes)
        {
            plane.On();

            plane.SetColor(myReflectData.color);
        }
    }
    public void On()
    {
        StopDrawAndFadeLine();
    }
    public override void UnHandleReflected()
    {
        foreach (var plane in _planes)
        {
            plane.Un();
        };
    }
    public void Un()
    {
        StartDrawAndFadeLine();

    }
    public void SetColor(Color color)
    {
        SetLightColor(color);
    }
    public override void GetReflectedObjectDataModify(ReflectData reflectedData)
    {
        foreach (var plane in _planes)
        {
            plane.SetDataModify(reflectedData);
        }
    }

    private void SetDataModify(ReflectData reflectedData)
    {
        SetStartPos(transform.position);

        Color cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        var raycastDirection = transform.up;

        ReflectiveObject obj = OnShootRaycast<ReflectiveObject>(transform.position, raycastDirection); //��, �츮 �� �� ���� �������?
        ChangedReflectObject(obj);
        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.GetReflectedObjectDataModify(myReflectData);

        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(transform.position, raycastDirection);
        door?.ColorMatch(cCol, this);
    }
}