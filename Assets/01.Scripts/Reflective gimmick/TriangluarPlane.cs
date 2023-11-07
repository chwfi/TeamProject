using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TriangluarPlane : Reflective //삼각형의 각 면
{
    public override void SetDataModify(ReflectData data)
    {
        var dir = SetDirection(transform.up);
        data.hitPos  = transform.position;
        OnShootRaycast(data, dir);
    }

    public void OnShoot()
    {
        lb.SetPosition(0, transform.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, 1000))
        {
            if (gameObject == hit.collider.gameObject) return;

            myReflectData.hitPos = hit.point;
            myReflectData.normal = hit.normal;

            lb.SetPosition(1, hit.point);
        }
        else
        {
            lb.SetPosition(1, transform.position + transform.up * 1000);
        }
    }
}
