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

    private ReflectData myReflectData;

    private Reflective reflectObject = null;

    private void Awake()
    {
        myReflectData.color = defaultColor;

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
        myReflectData.hitPos = origin;
        myReflectData.direction = direction;

        DataModify(myReflectData);
    }
    public void DataModify(ReflectData reflectData)
    {
        lb.SetPosition(0, reflectData.hitPos);
        RaycastHit hit;

        if (Physics.Raycast(reflectData.hitPos, reflectData.direction, out hit, 1000, ReflectionLayer))
        {
            myReflectData.hitPos = hit.point;
            myReflectData.direction = reflectData.direction;
            myReflectData.normal = hit.normal;

            lb.SetPosition(1, hit.point);

            if (hit.collider.TryGetComponent<Reflective>(out var reflectable)) //반사 오브젝트라면
            {
                ChangedReflectObject(reflectable);

                reflectable?.OnReflectTypeChanged(ReflectState.OnReflect);
                reflectable?.SetDataModify(myReflectData);
            }
        }
        else
        {
            if(reflectObject != null)
            {
                reflectObject?.OnReflectTypeChanged(ReflectState.UnReflect);
                reflectObject = null;
            }
            
            lb.SetPosition(1, reflectData.direction * 1000);
        }
    }
    private void ChangedReflectObject(Reflective reflectable)
    {
        if (reflectObject == reflectable) return;
        reflectObject = reflectable;
    }
}
