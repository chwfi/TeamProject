using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TriangluarPlane : Reflective //삼각형의 각 면
{
    public override void SetDataModify(ReflectData data)
    {

    }

    public void OnShoot()
    {
        _lr.SetPosition(0, transform.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, 1000))
        {
            if (gameObject == hit.collider.gameObject) return;

            myReflectData.hitPos = hit.point;
            myReflectData.normal = hit.normal;

            _lr.SetPosition(1, hit.point);
        }
        else
        {
            _lr.SetPosition(1, transform.position + transform.up * 1000);
        }
    }
}
