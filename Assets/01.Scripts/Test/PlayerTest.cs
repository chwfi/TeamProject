using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define.Define;


public class PlayerTest : MonoBehaviour
{
    public Color defaultColor;
    private LineRenderer lb;

    private ReflectData mydata;

    private IReflectable reflectObject = null;

    private void Awake()
    {
        mydata.inColor = defaultColor;

        lb = (LineRenderer)GetComponent("LineRenderer");
        lb.positionCount = 2;
        lb.material.color = defaultColor;
    }
    private void Update()
    {
        OnShootLight();
    }
    public void OnShootLight()
    {
        StartShootLight(transform.position, transform.forward);
    }
    public void StartShootLight(Vector3 origin, Vector3 direction)
    {
        mydata.inHitPos = origin;
        mydata.inDirection = direction;

        DataModify(mydata);
    }
    public void DataModify(ReflectData reflectData)
    {
        lb.SetPosition(0, reflectData.inHitPos);
        RaycastHit hit;

        if (Physics.Raycast(reflectData.inHitPos, reflectData.inDirection, out hit, 1000, ReflectionLayer))
        {
            mydata.inHitPos = hit.point;
            mydata.inDirection = reflectData.inDirection;
            mydata.normal = hit.normal;

            lb.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<IReflectable>(out var reflectable)) //반사 오브젝트라면
            {
                ChangedReflectObject(reflectable);

                reflectObject?.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectObject?.DataModify(mydata);
            }
        }
        else
        {
            if (reflectObject != null)
                reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);

            lb.SetPosition(1, reflectData.inDirection * 1000);
        }
    }
    private void ChangedReflectObject(IReflectable reflectable)
    {
        if (reflectObject == reflectable) return;
        reflectObject = reflectable;
    }
}
