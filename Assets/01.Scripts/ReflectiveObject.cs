using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ReflectiveObject : Reflective
{
    public override void SetDataModify(ReflectData data) //�ƴ�! �����Ͱ� ����ƾ�????????
    {
        _startPos = data.hitPos;

        Color cCol;

        cCol = ColorSystem.GetColorCombination(data.color, defaultColor);
        SetLightColor(cCol); //�츮 �� �� ������� ���� ���� ���������?

        var raycastDirection = SetDirection(Vector3.Reflect(data.direction, data.normal));
        //������! ���ؼ� �־��ݽô�. �ٵ� �׳� �ٷ� ���͸� �־��ָ� �Ǵµ� �� ���̱��� SetDirection���� ������ �޳�!
        //�ٷ� ���� �����͸� �����ϱ� ���ؼ��Դϴ�! �𸣰����� ���ڷ� �����ּ���!

        OnShootRaycast(data, raycastDirection); //��, �츮 �� �� ���� �������?
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
