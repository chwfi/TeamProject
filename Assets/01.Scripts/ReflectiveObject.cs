using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReflectiveObject : Reflective
{
    public override void GetReflectedObjectDataModify(ReflectData reflectedData) //�ƴ�! �����Ͱ� ����ƾ�????????
    {
        SetStartPos(reflectedData.hitPos);

        Color cCol;

        //========�ٵ� �Ʒ� �ڵ� ������Ʈ���� ���� ��Ű�� �ͺ��� ó�� ���ö� ������ �޾ƿͼ� �ϴ°� ���� �� �����ѵ� �ϴ� ���� 

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        //================================================================================================

        var raycastDirection = Vector3.Reflect(reflectedData.direction, reflectedData.normal);
        //������! ���ؼ� �־��ݽô�. �ٵ� �׳� �ٷ� ���͸� �־��ָ� �Ǵµ� �� ���̱��� SetDirection���� ������ �޳�!
        //�ٷ� ���� �����͸� �����ϱ� ���ؼ��Դϴ�! �𸣰����� ���ڷ� �����ּ���!

        var refObj = OnShootRaycast<Reflective>(reflectedData.hitPos, raycastDirection); //��, �츮 �� �� ���� �������?

        ChangedReflectObject(refObj);
        refObj?.OnReflectTypeChanged(ReflectState.OnReflect);
        refObj?.GetReflectedObjectDataModify(myReflectData);

        var triPrism = OnShootRaycast<TriangularPrism>(reflectedData.hitPos, raycastDirection);
        var triPlane = OnShootRaycast<TriangluarPlane>(reflectedData.hitPos, raycastDirection);
        triPrism.SetTriangluarPlane(triPlane);


        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(reflectedData.hitPos, raycastDirection);

        door?.ColorMatch(myReflectData.color);
    }

    public override void OnHandleReflected() //��, ó���� ���Ա�
    {
        base.OnHandleReflected();
    }

    public override void UnHandleReflected() //��, ó���� ������
    {
        base.UnHandleReflected();
    }
}
