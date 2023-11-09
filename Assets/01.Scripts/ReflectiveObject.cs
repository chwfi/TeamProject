using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReflectiveObject : Reflective
{
    public override void GetReflectedObjectDataModify(ReflectData reflectedData) //�ƴ�! �����Ͱ� ����ƾ�????????
    {
        _startPos = reflectedData.hitPos;

        Color cCol;

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol); //�츮 �� �� ������� ���� ���� ���������?

        var raycastDirection = Vector3.Reflect(reflectedData.direction, reflectedData.normal);
        //������! ���ؼ� �־��ݽô�. �ٵ� �׳� �ٷ� ���͸� �־��ָ� �Ǵµ� �� ���̱��� SetDirection���� ������ �޳�!
        //�ٷ� ���� �����͸� �����ϱ� ���ؼ��Դϴ�! �𸣰����� ���ڷ� �����ּ���!

        var obj = OnShootRaycast<Reflective>(reflectedData, raycastDirection); //��, �츮 �� �� ���� �������?

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);

        obj?.GetReflectedObjectDataModify(myReflectData);

    }

    public override void OnHandleReflected() //��, ó���� ���Ա�
    {
        base.OnHandleReflected();
        Debug.Log(gameObject.name + " : on");
    }

    public override void UnHandleReflected() //��, ó���� ������
    {
        base.UnHandleReflected();

        Debug.Log(gameObject.name + " : un");
    }
}
