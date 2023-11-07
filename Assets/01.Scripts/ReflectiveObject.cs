using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ReflectiveObject : Reflective
{
    public override void SetDataModify(ReflectData data) //아니! 데이터가 변경됐어????????
    {
        var cCol = ColorSystem.GetColorCombination(data.color, defaultColor); //자, 이게 무슨 코드냐면요 바로바로 색을 더해주는 코드입니다!!
        SetLightColor(cCol); //우리 한 번 만들어진 색을 빛에 입혀볼까요?

        var raycastDirection = SetDirection(Vector3.Reflect(data.direction, data.normal));
        //방향을! 구해서 넣어줍시다. 근데 그냥 바로 백터를 넣어주면 되는데 왜 굳이굳이 SetDirection에서 리턴을 받냐!
        //바로 나의 데이터를 저장하기 위해서입니다! 모르겠으면 디코로 연락주세욧!

        OnShootRaycast(data, raycastDirection); //자, 우리 한 번 빛을 쏴볼까요?
    }

    public override void OnHandleReflected() //음, 처음에 들어왔군
    {
        base.OnHandleReflected();
        Debug.Log(gameObject.name + " : on");
    }

    public override void UnHandleReflected() //음, 처음에 나갔군
    {
        base.UnHandleReflected();

        Debug.Log(gameObject.name + " : un");
    }
}
