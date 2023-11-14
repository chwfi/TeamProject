using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReflectiveObject : Reflective
{
    public override void GetReflectedObjectDataModify(ReflectData reflectedData) //아니! 데이터가 변경됐어????????
    {
        SetStartPos(reflectedData.hitPos);

        Color cCol;

        //========근데 아래 코드 업데이트에서 실행 시키는 것보단 처음 들어올때 데이터 받아와서 하는게 좋을 것 같긴한데 일단 보류 

        cCol = ColorSystem.GetColorCombination(reflectedData.color, defaultColor);
        SetLightColor(cCol);

        //================================================================================================
        var raycastDirection = Vector3.Reflect(reflectedData.direction, reflectedData.normal);

        //방향을! 구해서 넣어줍시다. 근데 그냥 바로 백터를 넣어주면 되는데 왜 굳이굳이 SetDirection에서 리턴을 받냐!
        //바로 나의 데이터를 저장하기 위해서입니다! 모르겠으면 디코로 연락주세욧!
        var obj = OnShootRaycast<Reflective>(reflectedData.hitPos, raycastDirection); //자, 우리 한 번 빛을 쏴볼까요?
        ChangedReflectObject(obj);
        obj?.OnReflectTypeChanged(ReflectState.OnReflect);
        obj?.GetReflectedObjectDataModify(myReflectData);

        TriangluarPlane triPlane = OnShootRaycast<TriangluarPlane>(reflectedData.hitPos, raycastDirection);
        triPlane?.GetReflectedObjectDataModify(myReflectData);


        DoorOpenTrigger door = OnShootRaycast<DoorOpenTrigger>(reflectedData.hitPos, raycastDirection);
        door?.ColorMatch(myReflectData.color);
    }

    public override void OnHandleReflected() //음, 처음에 들어왔군
    {
        base.OnHandleReflected();
    }

    public override void UnHandleReflected() //음, 처음에 나갔군
    {
        base.UnHandleReflected();
    }
}