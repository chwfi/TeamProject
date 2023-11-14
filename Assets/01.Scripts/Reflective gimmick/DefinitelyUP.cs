using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyUP : Reflective
{
    public override void GetReflectedObjectDataModify(ReflectData inData)
    {
        SetStartPos(inData.hitPos);

        Color cCol;

        //========�ٵ� �Ʒ� �ڵ� ������Ʈ���� ���� ��Ű�� �ͺ��� ó�� ���ö� ������ �޾ƿͼ� �ϴ°� ���� �� �����ѵ� �ϴ� ���� 

        cCol = ColorSystem.GetColorCombination(inData.color, defaultColor);
        SetLightColor(cCol);

        //================================================================================================

        var raycastDirection = transform.up;
        //������! ���ؼ� �־��ݽô�. �ٵ� �׳� �ٷ� ���͸� �־��ָ� �Ǵµ� �� ���̱��� SetDirection���� ������ �޳�!
        //�ٷ� ���� �����͸� �����ϱ� ���ؼ��Դϴ�! �𸣰����� ���ڷ� �����ּ���!

        var obj = OnShootRaycast<ReflectiveObject>(inData.hitPos, transform.up); // ������ ���θ�

        ChangedReflectObject(obj);

        obj?.OnReflectTypeChanged(ReflectState.OnReflect);

        obj?.GetReflectedObjectDataModify(myReflectData);

        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(inData.hitPos, raycastDirection);

        door?.ColorMatch(cCol);
    }

    public override void OnHandleReflected()
    {
        base.OnHandleReflected();
    }
    public override void UnHandleReflected()
    {   
        base.UnHandleReflected();
    }
}
