using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TriangluarPlane : Reflective //�ﰢ���� �� ��
{
    public override void SetDataModify(ReflectData data)
    {
        //var cCol = ColorSystem.GetColorCombination(data.color, defaultColor);
        //_lr.material.color = cCol;

        //var raycastDirection = Vector3.Reflect(data.direction, data.normal);
        //myReflectData.direction = raycastDirection;

        //OnShootRaycast(data, raycastDirection);
    }

    public void OnShoot(ReflectData inData, Vector3 dir)
    {
        _lr.SetPosition(1, transform.position);
    }
}
