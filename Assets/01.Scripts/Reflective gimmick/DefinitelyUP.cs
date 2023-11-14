using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinitelyUP : Reflective
{
    public override void GetReflectedObjectDataModify(ReflectData inData)
    {
        SetStartPos(inData.hitPos);

        Color cCol;

        //========근데 아래 코드 업데이트에서 실행 시키는 것보단 처음 들어올때 데이터 받아와서 하는게 좋을 것 같긴한데 일단 보류 

        cCol = ColorSystem.GetColorCombination(inData.color, defaultColor);
        SetLightColor(cCol);

        //================================================================================================

        var raycastDirection = transform.up;
        //방향을! 구해서 넣어줍시다. 근데 그냥 바로 백터를 넣어주면 되는데 왜 굳이굳이 SetDirection에서 리턴을 받냐!
        //바로 나의 데이터를 저장하기 위해서입니다! 모르겠으면 디코로 연락주세욧!

        var obj = OnShootRaycast<ReflectiveObject>(inData.hitPos, transform.up); // 무조건 위로만

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
